using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServerSocialNet.Controllers;
using WebServerSocialNet.DataAccessLayer;
using WebServerSocialNet.Domains;

namespace WebServerSocialNet.Helpers
{
    public class DBManager
    {
        private static ModelSocialNetContainer _db;

        static DBManager()
        {
            _db = new ModelSocialNetContainer();
        }

        static public void SavePhoto(string fname, int userId)
        {
            var user = _db.SocialUsers.Find(userId);
            if (user != null)
            {
                var photo = new Photo()
                {
                    Name = fname,
                    Filename = fname,
                    Description = "новая фотка"
                };
                _db.Photos.Add(photo);

                // photo.PhotoUsers.Add(user);
                user.UserPhotos.Add(photo);               
                _db.SaveChanges();

                user.AvatarPhotoId = photo.PhotoId;
                _db.SaveChanges();
            }
        }

        static public void SaveAddPhoto(string fname, int userId)
        {
            var user = _db.SocialUsers.Find(userId);
            if (user != null)
            {
                var photo = new Photo()
                {
                    Name = fname,
                    Filename = fname,
                    Description = "новая фотка"
                };
                _db.Photos.Add(photo);
                user.UserPhotos.Add(photo);
                _db.SaveChanges();
            }
        }

        public static void AddGroup(string name,string photoName)
        {
            var photo = new Photo()
            {
                Name = photoName,
                Filename = photoName,
                Description = "новая фотка"
            };

            var group = new UserGroup()
            {
                Name = name,
                Cover=photo
            };
            
            _db.Photos.Add(photo);
            _db.SaveChanges();

            _db.UserGroups.Add(group);
            _db.SaveChanges();
        }

        public static void AddPost(string text, string photoName)
        {
            var actUser = _db.SocialUsers.Find(ValuesController.idUserActive);

            var photo = new Photo()
            {
                Name = photoName,
                Filename = photoName,
                Description = "новая фотка"
            };

            var post = new Post()
            {
                Text = text,
                Date = DateTime.Now,
                LikesCounter = 0
            };

            _db.Photos.Add(photo);
            _db.SaveChanges();

            post.AttachedPhotos.Add(photo);
            post.Author = actUser;

            _db.Posts.Add(post);
            _db.SaveChanges();
        }

        public static void AddPost(string text)
        {
            var actUser = _db.SocialUsers.Find(ValuesController.idUserActive);
            
            var post = new Post()
            {
                Text = text,
                Date = DateTime.Now,
                LikesCounter = 0
            };
            post.Author = actUser;
            _db.Posts.Add(post);
            _db.SaveChanges();
        }

        public static void AddNewUser(SimpleUser user)
        {
            var socialUser = new SocialUser()
            {
                Email = user.Email,
                Firstname = user.FirstName,
                Lastname = user.LastName,
                Birthday = user.Birthday,
                Password = user.Password,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                City = user.City,
                AboutMe = user.AboutMe,
                AvatarPhotoId = user.AvatarPhotoId,
                AspUserId = user.AspId
                
            };
            _db.SocialUsers.Add(socialUser);
            try
            {
                _db.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}