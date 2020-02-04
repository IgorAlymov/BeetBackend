using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public int LikesCounter { get; set; }
        public DateTime Date { get; set; }

        public  ICollection<LikeComment> LikeComment { get; set; }
        public  Post Post { get; set; }
        public  SocialUser Author { get; set; }
    }
}
