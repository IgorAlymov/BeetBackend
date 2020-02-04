using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebServerSocialNet.DataAccessLayer;
using WebServerSocialNet.Domains;
using WebServerSocialNet.Helpers;
using WebServerSocialNet.Models;

namespace WebServerSocialNet.Controllers
{
    public class ValuesController : ApiController
    {
        private ModelSocialNetContainer _db;
        private ApplicationUserManager _manager;
        public static int idUserActive;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _manager ?? Request
                    .GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _manager = value;
            }
        }

        public ValuesController()
        {
            _db = new ModelSocialNetContainer();
        }
        //
        [Route("api/getallusers/{id}")]
        public IEnumerable<SimpleUser> GetAllUsers(int id)
        {
            var actUser = _db.SocialUsers.Where(b => b.SocialUserId == id).ToList();


            var users = _db
                .SocialUsers
                .Where(a => a.SocialUserId != id)
                .Select(u => new SimpleUser()
                {
                    Id = u.SocialUserId,
                    AboutMe = u.AboutMe,
                    AspId = u.AspUserId,
                    AvatarPhotoId = u.AvatarPhotoId,
                    City = u.City,
                    Email = u.Email,
                    FirstName = u.Firstname,
                    Gender = u.Gender,
                    LastName = u.Lastname,
                    PhoneNumber = u.PhoneNumber,
                    Birthday = u.Birthday
                })
                .ToList();
            List<SimpleUser> endUsers=new List<SimpleUser>();

            for (int i = 0; i < users.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < endUsers.Count; j++)
                {
                    if (users[i].AspId == endUsers[j].AspId || users[i].AspId==actUser[0].AspUserId)
                        found = true;
                }
                if (!found)
                    endUsers.Add(users[i]);
            }
            return endUsers;
        }
        //
        [Route("api/getSubscribers/{name}")]
        public IEnumerable<SimpleUser> GetSubscribers(string name)
        {
            var groups = _db.UserGroups.Where(a=>a.Name == name).ToList();

            List<SimpleUser> subUsers = new List<SimpleUser>();
            foreach (var group in groups)
            {
                var users = group
               .UsersForGroup
               .Select(u => new SimpleUser()
               {
                   Id = u.SocialUserId,
                   AboutMe = u.AboutMe,
                   AspId = u.AspUserId,
                   AvatarPhotoId = u.AvatarPhotoId,
                   City = u.City,
                   Email = u.Email,
                   FirstName = u.Firstname,
                   Gender = u.Gender,
                   LastName = u.Lastname,
                   PhoneNumber = u.PhoneNumber,
                   Birthday = u.Birthday
               }).ToList();
                if(users.Count != 0)
                subUsers.Add(users[0]);
            }
           

            return subUsers;
        }
        //
        [Route("api/getAllGroups")]
        public IEnumerable<UserGroup> GetAllGroups()
        {
            var groups = _db.UserGroups
                .ToList();

            List<UserGroup> userGroup = new List<UserGroup>();
            foreach (var item in groups)
            {
                UserGroup groupsResp = new UserGroup()
                {
                    Name = item.Name,
                    GroupId = item.GroupId
                };
                userGroup.Add(groupsResp);
            }

            List<UserGroup> endGroups = new List<UserGroup>();

            for (int i = 0; i < userGroup.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < endGroups.Count; j++)
                {
                    if (userGroup[i].Name == endGroups[j].Name )
                        found = true;
                }
                if (!found)
                    endGroups.Add(userGroup[i]);
            }


            return endGroups;
        }
        
        /*
        [Route("api/getActiveUser")]
        public IEnumerable<SimpleUser> GetActiveUser()
        {
            var user = _db
                .SocialUsers
                .Where(a => a.SocialUserId == idUserActive)
                .Select(u=> new SimpleUser()
                {
                    Id=u.SocialUserId,
                    AboutMe=u.AboutMe,
                    AspId=u.AspUserId,
                    AvatarPhotoId=u.AvatarPhotoId,
                    City=u.City,
                    Email=u.Email,
                    FirstName=u.Firstname,
                    Gender=u.Gender,
                    LastName=u.Lastname,
                    PhoneNumber=u.PhoneNumber,
                    Birthday=u.Birthday
                })
                .ToList();

            return user;
        }*/
        //
        [Route("api/getCountPhotoUser/{id}")]
        public IEnumerable<Photo> GetCountPhotoUser(int id)
        {
            var user = _db.SocialUsers.Find(id);
            var photo = user
                .UserPhotos
                .Select(a=> new Photo()
                {
                    Name = a.Name,
                    Filename = a.Name,
                    Description = "новая фотка",
                    PhotoId=a.PhotoId
                })
                .ToList();
            return photo;
        }
        //
        [Route("api/getuseravatar/{id}")]
        public HttpResponseMessage GetUserAvatar(int id)
        {
            // содержимое файла с картинкой
         byte[] fileContents = null;
            // находим юзера в базе
            var user = _db.SocialUsers.Find(id);
            if (user != null) // если нашли
            {
                // ищем аватарку:
                int idAvatar = (int)user.AvatarPhotoId;
                var avatar = _db.Photos.First(a=>a.PhotoId == idAvatar);
                if (avatar != null) // если нашли
                {
                    // загрузить соотв. файл
                    string fileName = avatar.Filename;
                    fileContents = FileManager.GetPhoto(fileName);
                }
            }

            // формируем Http ответ на запрос
            var response = new HttpResponseMessage();

            if (fileContents == null)
            {
                response.StatusCode = 
                    HttpStatusCode.BadRequest;
            } else
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ByteArrayContent(fileContents);
                response.Content
                        .Headers
                        .ContentType = new MediaTypeHeaderValue("image/png");
                response.Content
                        .Headers
                        .ContentLength = fileContents.Length;
            }

            return response;
        }
        //
        [Route("api/getImageGroup/{id}")]
        public HttpResponseMessage GetImageGroup(int id)
        {
            byte[] fileContents = null;
            var groups = _db.UserGroups.Find(id);
            if (groups != null && groups.Cover!=null)
            {
                int idAvatar = (int)groups.Cover.PhotoId;
                var avatar = _db.Photos.First(a => a.PhotoId == idAvatar);
                if (avatar != null) 
                {
                    string fileName = avatar.Filename;
                    fileContents = FileManager.GetPhoto(fileName);
                }
            }

            var response = new HttpResponseMessage();
            if (fileContents == null)
            {
                response.StatusCode =
                    HttpStatusCode.BadRequest;
            }
            else
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ByteArrayContent(fileContents);
                response.Content
                        .Headers
                        .ContentType = new MediaTypeHeaderValue("image/png");
                response.Content
                        .Headers
                        .ContentLength = fileContents.Length;
            }
            return response;
        }
        //
        [Route("api/getImagePosts/{id}")]
        public HttpResponseMessage GetImagePosts(int id)
        {
            byte[] fileContents = null;
            var posts = _db.Posts.Find(id);
            var photo = posts.AttachedPhotos.ToList();

            if (posts != null && photo.Count!=0)
            {
                int idAvatar = (int)photo[0].PhotoId;
                var avatar = _db.Photos.First(a => a.PhotoId == idAvatar);
                if (avatar != null)
                {
                    string fileName = avatar.Filename;
                    fileContents = FileManager.GetPhoto(fileName);
                }
            }

            var response = new HttpResponseMessage();
            if (fileContents == null)
            {
                response.StatusCode =
                    HttpStatusCode.BadRequest;
            }
            else
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ByteArrayContent(fileContents);
                response.Content
                        .Headers
                        .ContentType = new MediaTypeHeaderValue("image/png");
                response.Content
                        .Headers
                        .ContentLength = fileContents.Length;
            }
            return response;
        }
        //
        [Route("api/getAllUserPhoto/{id}/{idU}")]
        public HttpResponseMessage GetAllUserPhoto(int id, int idU)
        {
            List<byte[]> listPhoto = new List<byte[]>();
            byte[] fileContents = null;
            var user = _db.SocialUsers.Find(idU);
            if (user != null) // если нашли
            {
                var photos = user.UserPhotos;
                if (photos != null) // если нашли
                {
                    foreach (var item in photos)
                    {
                        if (item.PhotoId == id)
                        {
                            string fileName = item.Filename;
                            fileContents = FileManager.GetPhoto(fileName);
                            listPhoto.Add(fileContents);
                        }
                    }
                }
            }

            // формируем Http ответ на запрос
            var response = new HttpResponseMessage();

            if (fileContents == null)
            {
                response.StatusCode =
                    HttpStatusCode.BadRequest;
            }
            else
            {
                response.StatusCode = HttpStatusCode.OK;
                
                    response.Content = new ByteArrayContent(fileContents);
                    response.Content
                            .Headers
                            .ContentType = new MediaTypeHeaderValue("image/png");
                    response.Content
                            .Headers
                            .ContentLength = fileContents.Length;
            }

            return response;
        }
        //
        [Route("api/PostUserPhoto/{id}")]
        public HttpResponseMessage PostUserPhoto(int id)
        {
            var lastUser = _db.SocialUsers.AsEnumerable().LastOrDefault();
            var response = new HttpResponseMessage();
            try
            {
                // получаем запрос юзера c картинкой
                var request = HttpContext.Current.Request;
                // перебираем циклом все картинки
                foreach (string fname in request.Files)
                {
                    HttpPostedFile f = request.Files[fname];
                    // можно сделать фильтры
                    // 1) по расширениям файлов
                    // 2) по размеру файлов
                    // 3) картинки можно пережимать, конвертировать и резать
                    string photo = FileManager.SavePhoto(f);

                    DBManager.SavePhoto(photo, lastUser.SocialUserId);
                }
                response.StatusCode = HttpStatusCode.OK;
            }
            catch(Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ReasonPhrase = ex.Message;
            }
            return response;
        }
        //
        [Route("api/PostAddPhoto/{id}")]
        public HttpResponseMessage PostAddPhoto(int id)
        {
            var lastUser = _db.SocialUsers.Find(id);
            var response = new HttpResponseMessage();
            try
            {
                var request = HttpContext.Current.Request;
                foreach (string fname in request.Files)
                {
                    HttpPostedFile f = request.Files[fname];
                    string photo = FileManager.SavePhoto(f);

                    DBManager.SaveAddPhoto(photo, lastUser.SocialUserId);
                }
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ReasonPhrase = ex.Message;
            }
            return response;
        }
        //
        [Route("api/PostAddGroup/{name}")]
        public async Task<HttpResponseMessage> PostAddGroup(string name)
        {
            var response = new HttpResponseMessage();
            try
            {
                var request = HttpContext.Current.Request;
                // перебираем циклом все картинки
                foreach (string fname in request.Files)
                {
                    HttpPostedFile f = request.Files[fname];
                    // можно сделать фильтры
                    // 1) по расширениям файлов
                    // 2) по размеру файлов
                    // 3) картинки можно пережимать, конвертировать и резать
                    string photo = FileManager.SavePhoto(f);

                    DBManager.AddGroup(name, photo);
                }
                
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ReasonPhrase = ex.Message;
            }
            return response;
        }
        //
        [Route("api/postAddPost/{text}")]
        public async Task<HttpResponseMessage> PostAddPost(string text)
        {
            var response = new HttpResponseMessage();
            try
            {
                var request = HttpContext.Current.Request;
                if(request.Files.Count == 0 )
                    DBManager.AddPost(text);
                foreach (string fname in request.Files)
                {
                    HttpPostedFile f = request.Files[fname];
                    string photo = FileManager.SavePhoto(f);
                    DBManager.AddPost(text, photo);
                }

                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ReasonPhrase = ex.Message;
            }
            return response;
        }
        //
        [Route("api/addLikePost/{idPost}")]
        public async Task<HttpResponseMessage> AddLikePost(int idPost)
        {
            var response = new HttpResponseMessage();
            var user = _db.SocialUsers.Find(idUserActive);

            var post = _db.Posts.Find(idPost);
            _db.Posts.Remove(post);
            _db.SaveChanges();

            post.LikesCounter++;
            post.Author = user;
            _db.Posts.Add(post);
            _db.SaveChanges();

            return response;
        }
        //
        [Route("api/GetRemoveFriend/{id}")]
        public HttpResponseMessage GetRemoveFriend(int id)
        {
            var response = new HttpResponseMessage();

            var remFriend = _db.SocialUsers.Where(a => a.SocialUserId == id).ToList();
            _db.SocialUsers.Remove(remFriend[0]);
            _db.SaveChanges();
            return response;
        }
        //
        [Route("api/getAddNewFriend/{idU}/{idF}")]
        public HttpResponseMessage GetAddNewFriend(int idU, int idF)
        {
            var response = new HttpResponseMessage();

            var AUser = _db
                .SocialUsers
                .Where(a => a.SocialUserId == idU)
                .ToList();
            var FUser= _db
                .SocialUsers
                .Where(a => a.SocialUserId == idF)
                .ToList();

            var F = new Friend()
            {
                SocialUserId = FUser[0].SocialUserId,
                AboutMe = FUser[0].AboutMe,
                AspUserId = FUser[0].AspUserId,
                AvatarPhotoId = FUser[0].AvatarPhotoId,
                City = FUser[0].City,
                Email = FUser[0].Email,
                Firstname = FUser[0].Firstname,
                Gender = FUser[0].Gender,
                Lastname = FUser[0].Lastname,
                PhoneNumber = FUser[0].PhoneNumber,
                Birthday = FUser[0].Birthday,
                Password=FUser[0].Password
            };

            bool found = false;
            var allFriends = AUser[0].Friends.ToList();
            foreach (var allF in allFriends)
            {
                if(allF.AspUserId == F.AspUserId)
                    found = true;
            }

            if (AUser != null && FUser != null && !found)
            {
                AUser[0].Friends.Add(F);
                _db.SaveChanges();
                response.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.StatusCode = HttpStatusCode.NotAcceptable;
            }

            return response;
        }
        //
        [Route("api/getAddGroupUser/{idU}/{idF}")]
        public HttpResponseMessage GetAddGroupUser(int idU, int idF)
        {
            var response = new HttpResponseMessage();

            var AUser = _db
                .SocialUsers
                .Where(a => a.SocialUserId == idU)
                .ToList();

            var group = _db
                .UserGroups
                .Where(a => a.GroupId == idF)
                .ToList();

            var F = new UserGroup()
            {
                GroupId=group[0].GroupId,
                Name=group[0].Name
            };

            bool found = false;
            var allGroups = AUser[0].Groups.ToList();
            foreach (var allG in allGroups)
            {
                if (allG.Name == F.Name)
                    found = true;
            }

            if (AUser != null && group != null && !found)
            {
                AUser[0].Groups.Add(F);
                _db.SaveChanges();
                response.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.StatusCode = HttpStatusCode.NotAcceptable;
            }

            return response;
        }
        //
        [Route("api/getFriends/{id}")]
        public IEnumerable<SimpleUser> GetFriends(int id)
        {
            var user = _db.SocialUsers.Where(a => a.SocialUserId == id).ToList();
            var friends = user[0]
                .Friends
                .Select(u => new SimpleUser()
                {
                    Id = u.SocialUserId,
                    AboutMe = u.AboutMe,
                    AspId = u.AspUserId,
                    AvatarPhotoId = u.AvatarPhotoId,
                    City = u.City,
                    Email = u.Email,
                    FirstName = u.Firstname,
                    Gender = u.Gender,
                    LastName = u.Lastname,
                    PhoneNumber = u.PhoneNumber,
                    Birthday = u.Birthday
                })
                .ToList();

            return friends;
        }
        //
        [Route("api/getMyGroups/{id}")]
        public IEnumerable<UserGroup> GetMyGroups(int id)
        {
            var user = _db.SocialUsers.Find(id);

            var groups = _db
                .UserGroups
                .ToList();
            List<UserGroup> listGroups = new List<UserGroup>();

            var photos = _db.Photos.Where(b=>b.GroupCover!=null).ToList();

            //group id у групп и group cover u фото
            foreach (var group in groups)
            {
                foreach (var photo in photos)
                {
                    foreach (var uGroup in user.Groups)
                    {
                        if (group.GroupId == photo.GroupCover.GroupId && uGroup.Name == photo.GroupCover.Name)
                        {
                            UserGroup g = new UserGroup()
                            {
                                GroupId = group.GroupId,
                                Name = group.Name
                            };
                            listGroups.Add(g);
                        }
                    }  
                }
            }
            
            return listGroups;
        }
        //
        [Route("api/getActiveUserPosts/{id}")]
        public IEnumerable<Post> GetActiveUserPosts(int id)
        {
            var user = _db.SocialUsers.Find(id);

            var posts = user.UserPosts
                .Select(u=> new Post(){
                    PostId=u.PostId,
                    Date=u.Date,
                    LikesCounter=u.LikesCounter,
                    Text=u.Text
            });
            return posts;
        }
        //
        [Route("api/RegisterUser")]
        public async Task<HttpResponseMessage> RegisterUser(SimpleUser user)
        {
            var response = new HttpResponseMessage();

            var appUser = new ApplicationUser()
            {
                Email = user.Email,
                UserName = user.Email,
                PasswordHash=user.Password
            };
            IdentityResult result = 
                await UserManager.CreateAsync(appUser, 
                                              user.Password);
            var newUserAsp = UserManager.Users.AsEnumerable().Last();
            var idUserAsp = "";
            if (newUserAsp != null)
            {
                idUserAsp = newUserAsp.Id;
                user.AspId = idUserAsp;
            }
            if (result.Succeeded)
            {
                DBManager.AddNewUser(user);
                response.StatusCode = HttpStatusCode.OK;
                var u = _db.SocialUsers.Where(a=>a.AspUserId== idUserAsp).ToList();
                idUserActive = u[0].SocialUserId;

            } else
            {
                response.StatusCode = HttpStatusCode.NotAcceptable;
                response.ReasonPhrase = result.Errors.First();
            }
            //var userId = User.Identity.GetUserId();
            return response;
        }
        //
        [Route("api/EntryUser")]
        public async Task<HttpResponseMessage> EntryUser(SimpleUser user)
        {
            var response = new HttpResponseMessage();
            var entryUser = _db.SocialUsers.AsEnumerable().Where(a => a.Email == user.Email).ToList();
            try
            {
                if (entryUser[0].Password == user.Password && entryUser != null)
                {
                    idUserActive = entryUser[0].SocialUserId;
                    response.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception)
            {
                response.StatusCode = HttpStatusCode.NotAcceptable;
                response.ReasonPhrase = "Incorrect login or password";
                return response;
            }

            return response;
        }
        
    }
}
