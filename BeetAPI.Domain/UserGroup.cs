using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }

        public  Photo Cover { get; set; }
        public  ICollection<SocialUser> UsersForGroup { get; set; }
        public  ICollection<Post> Posts { get; set; }

        public Group()
        {
            UsersForGroup = new List<SocialUser>();
        }
    }
}
