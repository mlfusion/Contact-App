using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Contact.Data.EntityFramework;

namespace Contact.Data.Infrastructure.Repository
{
    public class Repository<T> where T : class
    {
        private Contact_DBEntities context;
        private readonly IDbSet<T> dbSet;
        
        protected IDbFactory DbFactory { get; private set; }

        protected Repository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        protected Contact_DBEntities DbContext
        {
            get { return context ?? (context = DbFactory.Init()); }
        }

        public virtual T Select(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> Select()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<T> Select(Expression<Func<T, bool>> where, int status = 0)
        {
            return dbSet.Where(where).ToList();//.FirstOrDefault<T>();
        }

        public virtual T Select(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault();
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);

            if (context != null)
            {
                context.Entry(entity).State = EntityState.Modified;
            }        
        }

        public virtual void Update(Expression<Func<T, bool>> where)
        {
             dbSet.Where(where).FirstOrDefault<T>();
        }

        // Delete entity
        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        // Delete entity by delegate
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }
    }
}
