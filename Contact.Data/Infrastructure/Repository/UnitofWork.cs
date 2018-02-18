using System;
using System.Data.Entity.Validation;
using System.Linq;
using Contact.Data.EntityFramework;

namespace Contact.Data.Infrastructure.Repository
{
    public class UnitofWork : IUntOfWork
    {
        // Import
        private readonly IDbFactory _dbFactory;
        private Contact_DBEntities dbContext;

        public UnitofWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        public Contact_DBEntities DbContext
        {
            get { return dbContext ?? (dbContext = _dbFactory.Init()); }
        }

        public void Commit()
        {
            try
            {
                DbContext.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                //CommitIntoErorrLog(exception);
            }

            // DbContext.Commit();
        }
    }
}
