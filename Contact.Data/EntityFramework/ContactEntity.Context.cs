﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Contact.Data.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Contact_DBEntities : DbContext
    {
        public Contact_DBEntities()
            : base("name=Contact_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    
        public virtual ObjectResult<string> sp_DeleteGroup(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_DeleteGroup", idParameter);
        }
    
        public virtual ObjectResult<string> sp_InsertGroup(string groupName)
        {
            var groupNameParameter = groupName != null ?
                new ObjectParameter("groupName", groupName) :
                new ObjectParameter("groupName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_InsertGroup", groupNameParameter);
        }
    
        public virtual ObjectResult<string> sp_UpdateGroup(Nullable<int> id, string groupName)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var groupNameParameter = groupName != null ?
                new ObjectParameter("groupName", groupName) :
                new ObjectParameter("groupName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_UpdateGroup", idParameter, groupNameParameter);
        }
    
        public virtual ObjectResult<Group> sp_SelectGroup()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Group>("sp_SelectGroup");
        }
    
        public virtual ObjectResult<Group> sp_SelectGroup(MergeOption mergeOption)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Group>("sp_SelectGroup", mergeOption);
        }
    
        public virtual ObjectResult<Contact> sp_SelectGroupById(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Contact>("sp_SelectGroupById", idParameter);
        }
    
        public virtual ObjectResult<Contact> sp_SelectGroupById(Nullable<int> id, MergeOption mergeOption)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Contact>("sp_SelectGroupById", mergeOption, idParameter);
        }
    
        public virtual ObjectResult<string> sp_DeleteContactGroup(Nullable<int> contactId, Nullable<int> groupid)
        {
            var contactIdParameter = contactId.HasValue ?
                new ObjectParameter("contactId", contactId) :
                new ObjectParameter("contactId", typeof(int));
    
            var groupidParameter = groupid.HasValue ?
                new ObjectParameter("groupid", groupid) :
                new ObjectParameter("groupid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("sp_DeleteContactGroup", contactIdParameter, groupidParameter);
        }
    }
}