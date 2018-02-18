using System.Xml.Serialization;
using Contact.Data.EntityFramework;
using Contact.Data.Infrastructure.Repository;

namespace Contact.Data.Infrastructure.Repository
{
   public  class DbFactory : Disposable, IDbFactory
   {
       private Contact_DBEntities _dbContext;

       public Contact_DBEntities Init()
       {
           return _dbContext ?? (_dbContext = new Contact_DBEntities());
       }

       protected override void DisposeCore()
       {
           if (_dbContext != null)
               _dbContext.Dispose();
       }
    }
}
