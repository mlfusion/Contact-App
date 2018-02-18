using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Contact.Data.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        // Select entity by Id
        T Select(int id);

        // Select entity by IEnumerable
        IEnumerable<T> Select();

        // Select entity by delegate 
        T Select(Expression<Func<T, bool>> where);

        IEnumerable<T> Select(Expression<Func<T, bool>> where, int status = 0);

        // Add new entity
        void Add(T entity);

        // Update entity
        void Update(T entity);

        // Update entity be delegate
        void Update(Expression<Func<T, bool>> where);

        // Delete entity
        void Delete(T entity);

        // Delete entity by delegate
        void Delete(Expression<Func<T, bool>> where);
    }
}
