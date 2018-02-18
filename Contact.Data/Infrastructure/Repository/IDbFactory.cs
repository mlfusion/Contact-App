using System;
using Contact.Data.EntityFramework;

namespace Contact.Data.Infrastructure.Repository
{
   public interface IDbFactory : IDisposable
   {
        Contact_DBEntities Init();
   }
}
