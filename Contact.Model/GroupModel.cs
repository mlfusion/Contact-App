using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Model
{
    public class GroupModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<ContactModel> Contacts { get; set; }
    }
}
