using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact.Data;
using Contacts.Model;
using Contact.Data.Infrastructure.Repository;

namespace Contact.Business.Objects
{
     public class Contact
    {
        private readonly Data.Repositiories.IContactRepository contactRepository;
        private readonly Data.Infrastructure.Repository.IDbFactory dbFactory;
        private readonly Data.Infrastructure.Repository.IUntOfWork unitOfWork;

        public Contact()
        {
            this.dbFactory = new DbFactory();
            this.unitOfWork = new UnitofWork(dbFactory);
            this.contactRepository = new Data.Repositiories.ContactRepository(dbFactory);

            // set automapper
           // Business.AutoMApperConfiguration.Configure();
        }

        public IEnumerable<Contacts.Model.ContactModel> SelectContacts()
        {
            var contacts = contactRepository.Select();
            //var contactMapper = AutoMapper.Mapper.Map< IEnumerable<Data.EntityFramework.Contact>, IEnumerable<ContactModel>>(contacts);

            List<ContactModel> gc = new List<ContactModel>();

            contacts.ToList().ForEach(x =>
            {
                // x.Groups.ToList().ForEach(g => g.Contacts = null);

                gc.Add(new ContactModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Birthdate = x.Birthdate.Value.Date,
                    Phone = x.Phone,
                    Photo = x.Photo,
                    Comments = x.Comments,
                    Groups = x.Groups.Select(c => new GroupModel
                    {
                        Id = c.Id,
                        GroupName = c.GroupName
                    }).ToList()
                });
            });

            return gc;
        }

        public Contacts.Model.ContactModel SelectContact(int id)
        {
            var contact =  contactRepository.Select(id);

            Contacts.Model.ContactModel contactModel = new ContactModel()
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Birthdate = contact.Birthdate.Value.Date,
                Phone = contact.Phone,
                Photo = contact.Photo,
                Comments = contact.Comments,
                Groups = contact.Groups.Select(x => new GroupModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName
                }).ToList()
            };

            return contactModel;
        }
            
        public Contacts.Model.ContactModel AddContact(Contacts.Model.ContactModel contact)
        {
            contact.FirstName = Helper.ContentHelper.Uppercase(contact.FirstName);
            contact.LastName = Helper.ContentHelper.Uppercase(contact.LastName);
            contact.Email = Helper.ContentHelper.Lowercase(contact.Email);
            contact.Comments = Helper.ContentHelper.UppercaseFirstWord(contact.Comments);
            contactRepository.AddContactAndGroups(ConvertContactModelToEntityModel(contact));
            return contact;
        }

        public Contacts.Model.ContactModel UpdateContact(Contacts.Model.ContactModel contact)
        {
            contact.FirstName = Helper.ContentHelper.Uppercase(contact.FirstName);
            contact.LastName = Helper.ContentHelper.Uppercase(contact.LastName);
            contact.Email = Helper.ContentHelper.Lowercase(contact.Email);
            contact.Comments = Helper.ContentHelper.UppercaseFirstWord(contact.Comments);
            contactRepository.UpdateContactAndGroups(ConvertContactModelToEntityModel(contact));
            return contact;
        }

        public int DeleteContact(int id)
        {
            contactRepository.Delete(x => x.Id == id);

            Save();

            return id;
        }

        private void Save()
        {
            unitOfWork.Commit();
        }

        private Data.EntityFramework.Contact ConvertContactModelToEntityModel(ContactModel contact)
        {
            var _contact = new Data.EntityFramework.Contact()
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Phone = contact.Phone,
                Birthdate = contact.Birthdate == null ? new DateTime(1900, 1, 1) : contact.Birthdate,
                Photo = contact.Photo,
                Comments = contact.Comments,
                Groups = contact.Groups.Select(x => new Data.EntityFramework.Group
                {
                    Id = x.Id,
                    GroupName = x.GroupName
                }).ToList()
            };

            return _contact;
        }
    }
}
