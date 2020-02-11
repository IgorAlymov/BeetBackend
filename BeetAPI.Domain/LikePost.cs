using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class LikePost
    {
        public int LikePostId { get; set; }

        public  int UserId { get; set; }
        public  int PostId { get; set; }
    }
}
