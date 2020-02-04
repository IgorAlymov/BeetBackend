using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class GroupRelation
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public SocialUser User { get; set; }
        public Group Group { get; set; }
    }
}
