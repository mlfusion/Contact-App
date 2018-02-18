using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Model
{
   public class ContactModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public string Photo { get; set; }
        public string Comments { get; set; }
        public virtual ICollection<GroupModel> Groups { get; set; }
    }
}
