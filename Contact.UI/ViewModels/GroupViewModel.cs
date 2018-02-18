using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact.UI.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        //  public virtual ICollection<ContactGroup> ContactGroups { get; set; }
        public virtual ICollection<ContactViewModel> Contacts { get; set; }
    }
}