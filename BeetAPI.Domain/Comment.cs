using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int PostId { get; set; }
        public int AuthorId  { get; set; }
        public string AuthorName { get; set; }
        public string AvatarAuthor { get; set; }

    }
}
