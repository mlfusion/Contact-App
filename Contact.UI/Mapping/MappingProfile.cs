using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact.UI.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<ViewModels.ContactViewModel, Contacts.Model.ContactModel>().ReverseMap();
            CreateMap<ViewModels.GroupViewModel, Contacts.Model.GroupModel>().ReverseMap();
        }

    }


}
