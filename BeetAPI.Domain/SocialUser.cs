using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.Domain
{
    public class SocialUser
    {
        public int SocialUserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int? AvatarPhotoId { get; set; }
        public DateTime? Birthday { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string AboutMe { get; set; }
        public string City { get; set; }

        public  ICollection<Photo> UserPhotos { get; set; }
        public  ICollection<Post> UserPosts { get; set; }
        public  ICollection<LikeComment> LikesComments { get; set; }
        public  ICollection<LikePost> LikesPosts { get; set; }
        public  ICollection<Comment> Comments { get; set; }
        public  ICollection<Message> MessageAuthor { get; set; }
        public  ICollection<Message> MessageReceiver { get; set; }
    }
}
