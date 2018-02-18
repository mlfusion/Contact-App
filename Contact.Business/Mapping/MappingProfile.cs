using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact.Business
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.EntityFramework.Contact, Contacts.Model.ContactModel>().ReverseMap();
            CreateMap<Data.EntityFramework.Group, Contacts.Model.GroupModel>().ReverseMap();
        }

    }


}
