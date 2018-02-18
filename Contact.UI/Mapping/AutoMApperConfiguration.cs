using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact.UI.Mapping
{
    public class AutoMApperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            AutoMapper.Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}