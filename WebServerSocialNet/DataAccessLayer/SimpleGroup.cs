using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServerSocialNet.Domains;

namespace WebServerSocialNet.DataAccessLayer
{
    public class SimpleGroup
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public SimpleGroup()
        {

        }

        public SimpleGroup(UserGroup userGroup)
        {
            GroupId = userGroup.GroupId;
            Name = userGroup.Name;
        }
    }
}