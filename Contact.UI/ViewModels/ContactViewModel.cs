using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contact.UI.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public string Photo { get; set; }
        public string Comments { get; set; }

        public virtual ICollection<GroupViewModel> Groups { get; set; }

        // public virtual ICollection<ContactGroupViewModel> ContactGroups { get; set; }
    }
}