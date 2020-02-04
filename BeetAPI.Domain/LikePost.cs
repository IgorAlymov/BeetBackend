using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class LikePost
    {
        public int LikePostId { get; set; }

        public  SocialUser User { get; set; }
        public  Post Post { get; set; }
    }
}
