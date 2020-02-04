using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class LikeComment
    {
        public int LikeCommentId { get; set; }

        public  SocialUser User { get; set; }
        public  Comment Comment { get; set; }
    }
}
