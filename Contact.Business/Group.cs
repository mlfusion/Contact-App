using Contact.Data.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Business
{
     public class Group
    {
        public readonly Data.Repositiories.IGroupRepository groupRepository;
        public readonly Data.Infrastructure.Repository.IDbFactory dbFactory;
        public readonly Data.Infrastructure.Repository.IUntOfWork unitOfWork;

        public Group()
        {
            this.dbFactory = new DbFactory();
            this.unitOfWork = new UnitofWork(dbFactory);
            this.groupRepository = new Data.Repositiories.GroupRepository(dbFactory);
        }

        public IEnumerable<Contacts.Model.GroupModel> SelectGroups()
        {
            return groupRepository.SelectGroupsFromStoreProc();
        }


        public IEnumerable<Contacts.Model.ContactModel> SelectGroup(int id)
        {
            return groupRepository.SelectGroupByIdFromStoreProc(id);
        }

        public string AddGroup(Contacts.Model.GroupModel group)
        {
            group.GroupName = Helper.ContentHelper.Uppercase(group.GroupName);
           return groupRepository.InsertGroupFromStoreProc(group);
        }

        public string UpdateGroup(Contacts.Model.GroupModel group)
        {
            group.GroupName = Helper.ContentHelper.Uppercase(group.GroupName);
            return  groupRepository.UpdateGroupFromStoreProc(group);
        }

        public string DeleteGroup(int id)
        {
           return groupRepository.DeleteGroupFromStoreProc(id);
        }

        public string DeleteGroupContact(int contactId, int groupId)
        {
            return groupRepository.DeleteGroupContactFromStoreProc(contactId, groupId);
        }

    }
}
