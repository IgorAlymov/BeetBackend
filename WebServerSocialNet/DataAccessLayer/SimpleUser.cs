using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServerSocialNet.Domains;

namespace WebServerSocialNet.DataAccessLayer
{
    public class SimpleUser
    {
        internal string ConnectId;

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string AboutMe { get; set; }

        public string AspId { get; set; }

        public Nullable<int> AvatarPhotoId { get; set; }

        public DateTime Birthday { get; set; }


        public SimpleUser()
        {

        }

        public SimpleUser(SocialUser user)
        {
            FirstName = user.Firstname;
            LastName = user.Lastname;
            Id = user.SocialUserId;
            Email = user.Email;
            Password = user.Password;
            Gender = user.Gender;
            PhoneNumber = user.PhoneNumber;
            City = user.City;
            AboutMe = user.AboutMe;
            AvatarPhotoId = user.AvatarPhotoId;
            AspId = user.AspUserId;
            Birthday = user.Birthday;
        }
    }
}