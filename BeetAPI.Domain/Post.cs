using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class Post
    {
        public int PostId { get; set; }
        public string Text { get; set; }
        public int LikesCounter { get; set; }
        public DateTime Date { get; set; }
        public int AuthorId { get; set; }

        public  ICollection<Photo> AttachedPhotos { get; set; }
        public  ICollection<LikePost> LikePost { get; set; }
        public  ICollection<Comment> AttachedComments { get; set; }
        public  Group UserGroupForPost { get; set; }

        public Post()
        {
            AttachedPhotos = new List<Photo>();
            LikePost = new List<LikePost>();
            AttachedComments = new List<Comment>();
            UserGroupForPost = new Group();
        }
    }
}
