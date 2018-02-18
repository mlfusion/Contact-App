using Contact.Data.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Data.Repositiories
{
    public class GroupRepository : Repository<Data.EntityFramework.Group>, IGroupRepository
    {
        public GroupRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public string DeleteGroupFromStoreProc(int id)
        {
            using (var context = DbContext)
            {
                var result = context.sp_DeleteGroup(id).FirstOrDefault();

                return result;
            }
        }

        public string DeleteGroupContactFromStoreProc(int contactId, int groupId)
        {
            using (var context = DbContext)
            {
                var result = context.sp_DeleteContactGroup(contactId, groupId).FirstOrDefault();

                return result;
            }
        }

        public string InsertGroupFromStoreProc(Contacts.Model.GroupModel group)
        {
            using (var context = DbContext)
            {
                var result = context.sp_InsertGroup(group.GroupName).FirstOrDefault();

                return result;
            }
        }

        public IEnumerable<Contacts.Model.GroupModel> SelectGroupsFromStoreProc()
        {
            List<Contacts.Model.GroupModel> groupList = new List<Contacts.Model.GroupModel>();

            using (var context = DbContext)
            {
                var groups = context.sp_SelectGroup().ToList();

                groups.ForEach(x =>
                {
                    groupList.Add(new Contacts.Model.GroupModel() { GroupName = x.GroupName, Id = x.Id });
                });

                return groupList;
            }
        }

        public IEnumerable<Contacts.Model.ContactModel> SelectGroupByIdFromStoreProc(int id)
        {
            List<Contacts.Model.ContactModel> groupList = new List<Contacts.Model.ContactModel>();

            using (var context = DbContext)
            {
                var groups = context.sp_SelectGroupById(id).ToList();

                groups.ForEach(x =>
                {
                    groupList.Add(new Contacts.Model.ContactModel() {
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Id = x.Id,
                        Groups = x.Groups.Select(g => new Contacts.Model.GroupModel
                        {
                            Id = g.Id,
                            GroupName = g.GroupName
                        }).ToList()
                    });
                });

                return groupList;
            }
        }

        public string UpdateGroupFromStoreProc(Contacts.Model.GroupModel group)
        {
            using (var context = DbContext)
            {
                var result = context.sp_UpdateGroup(group.Id, group.GroupName).FirstOrDefault();

                return result;
            }
        }
    }
    

    public interface IGroupRepository : IRepository<Data.EntityFramework.Group>
    {
        IEnumerable<Contacts.Model.GroupModel> SelectGroupsFromStoreProc();
        IEnumerable<Contacts.Model.ContactModel> SelectGroupByIdFromStoreProc(int id);
        string InsertGroupFromStoreProc(Contacts.Model.GroupModel group);
        string UpdateGroupFromStoreProc(Contacts.Model.GroupModel group);
        string DeleteGroupFromStoreProc(int id);
        string DeleteGroupContactFromStoreProc(int contactId, int groupId);
    }
}
