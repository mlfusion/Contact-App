using Contact.Data.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.Model;
using System.Data.SqlClient;

namespace Contact.Data.Repositiories
{
    public class ContactRepository : Repository<Data.EntityFramework.Contact>, IContactRepository
    {
        public ContactRepository(IDbFactory dbFactory): base(dbFactory) { }

        public void AddContactAndGroups(EntityFramework.Contact _contact)
        {
            using (var context = DbContext)
            {
                EntityFramework.Contact obj = new EntityFramework.Contact();
                obj.FirstName = _contact.FirstName;
                obj.LastName = _contact.FirstName;
                obj.Email = _contact.Email;
                obj.Phone = _contact.Phone;
                obj.Birthdate = _contact.Birthdate;
                obj.Photo = _contact.Photo;
                obj.Comments = _contact.Comments;

                context.Contacts.Add(obj);
                context.SaveChanges();

                // insert into ContactGroups table
                if (_contact.Groups != null)
                {
                    foreach (var g in _contact.Groups)
                    {
                        SqlParameter[] parameter = new SqlParameter[2];
                        parameter[0] = new SqlParameter("@p1", obj.Id);
                        parameter[1] = new SqlParameter("@p2", g.Id);
                        context.Database.ExecuteSqlCommand("insert into dbo.ContactGroups values(@p1, @p2)", parameter);
                    }
                }
            }

           // return contact;
        }

        public Contacts.Model.ContactModel UpdateContactAndGroups(EntityFramework.Contact contact)
        {
            // update ContactGroups relation table
            using (var context = DbContext)
            {
                contact.Birthdate = contact.Birthdate == null ? new DateTime(1900, 1, 1) : contact.Birthdate;

                // update contact table
                base.Update(contact);

                var deletedRow = context.Database.ExecuteSqlCommand("delete from dbo.ContactGroups where ContactId = " + contact.Id);

                if (contact.Groups != null)
                {
                    foreach(var g in contact.Groups)
                    {
                        SqlParameter[] parameter = new SqlParameter[2];
                        parameter[0] = new SqlParameter("@p1", contact.Id);
                        parameter[1] = new SqlParameter("@p2", g.Id);
                        context.Database.ExecuteSqlCommand("insert into dbo.ContactGroups values(@p1, @p2)", parameter);
                    }
                }

                context.SaveChanges();
            }

            return null;
        }
    }

    public interface IContactRepository: IRepository<Data.EntityFramework.Contact>
    {
        Contacts.Model.ContactModel UpdateContactAndGroups(EntityFramework.Contact contact);

        void AddContactAndGroups(EntityFramework.Contact contact);
    }
}
