using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class FriendRelation
    {
        public int UserIdAdding { get; set; }
        public int UserIdAdded { get; set; }

        public SocialUser UserAdding { get; set; }
        public SocialUser UserAdded { get; set; }
    }
}
