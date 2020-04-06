using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using BeetAPI.DataAccessLayer;
using BeetAPI.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServerBeetCore.Models;

namespace WebServerBeetCore.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        PostRepository _dbPost;
        SocialUserRepository _dbUser;
        PhotoRepository _dbPhoto;
        IHostingEnvironment _appEnvironment;
        CommentRepository _dbComment;
        LikePostRepository _dbLikePost;

        public UserController(LikePostRepository likePost, CommentRepository comment, PostRepository repPost, SocialUserRepository rep, PhotoRepository repPhoto, IHostingEnvironment appEnvironment)
        {
            _dbUser = rep;
            _dbPhoto = repPhoto;
            _appEnvironment = appEnvironment;
            _dbPost = repPost;
            _dbComment = comment;
            _dbLikePost = likePost;
        }
        
        [Route("GetUserAvatar")]
        public IActionResult GetUserAvatar()
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            if (user == null) return NotFound();
            Photo avatar = new Photo();
            if (user.AvatarPhotoId == null)
            {
                avatar.Path = "Photos/noAvatar.png";
            }
            else
            {
                avatar = _dbPhoto.GetAvatar((int)user.AvatarPhotoId);
                if(avatar==null)
                {
                    avatar = new Photo();
                    avatar.Path = "Photos/noAvatar.png";
                }
                    
            }
                
            return Ok(new {
                avatarUrl = Path.Combine("http://localhost:5001", avatar.Path)
            });
        }

        [Route("GetUserAvatar/{id}")]
        public IActionResult GetUserAvatar(int id)
        {
            var user = _dbUser.Get(id);
            if (user == null) return NotFound();

            Photo avatar = new Photo();
            if (user.AvatarPhotoId == null)
            {
                avatar.Path = "Photos/noAvatar.png";
            }
            else
            {
                avatar = _dbPhoto.GetAvatar((int)user.AvatarPhotoId);
                if (avatar == null)
                {
                    avatar = new Photo();
                    avatar.Path = "Photos/noAvatar.png";
                }
            }
            return Ok(new
            {
                avatarUrl = Path.Combine("http://localhost:5001", avatar.Path)
            });
        }

        [Route("GetAllUserPhoto")]
        public IActionResult GetAllUserPhoto()
        {
            List<string> listPhoto =new List<string>();
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            var photos = _dbPhoto.GetPhotosUser(user.SocialUserId);
            if (photos != null)
            {
                foreach (var item in photos)
                {
                    listPhoto.Add(Path.Combine("http://localhost:5001", item.Path));
                }
                return Ok(new
                {
                    listPhoto
                });
            }
            else
                return NotFound();
        }
        
        [HttpPost("PostUserPhoto")]
        public async Task<IActionResult> PostUserPhoto([FromForm] UploadFileModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            string emailUser = User.Identity.Name;
            var lastUser = _dbUser.Get(emailUser);
            try
            {
                string path = Path.Combine("Photos", model.File.FileName);
                using (var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath, path), FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
                }

                Photo file = new Photo { Name = model.File.FileName, Path = path };
                _dbPhoto.Create(file);
                _dbPhoto.Save();

                lastUser.AvatarPhotoId = file.PhotoId;
                _dbUser.Save();

                file.PhotoUsers = lastUser;
                _dbPhoto.Save();
                    
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        [HttpPost("PostAddPhoto")]
        public async Task<IActionResult> PostAddPhoto([FromForm]UploadFileModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            string emailUser = User.Identity.Name;
            var lastUser = _dbUser.Get(emailUser);
            try
            {
                string path = Path.Combine("Photos", model.File.FileName);
                using (var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath, path), FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
                    fileStream.Close();
                }

                Photo file = new Photo { Name = model.File.FileName, Path = path };
                _dbPhoto.Create(file);
                _dbPhoto.Save();

                file.PhotoUsers = lastUser;
                _dbPhoto.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        [HttpGet("GetActiveUser")]
        public SocialUser GetActiveUser()
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            return user;
        }

        [HttpGet("GetUser/{id}")]
        public SocialUser GetUser(int id)
        {
            var user = _dbUser.Get(id);
            return user;
        }

        [Route("GetRemovePhoto/{text}")]
        public IActionResult GetRemovePhoto(string text)
        {
            string path = text.Remove(0,1);
            Photo photoRemove = _dbPhoto.Get(path);
            _dbPhoto.Delete(photoRemove);
            _dbPhoto.Save();
            return Ok();
        }

        [HttpPost("ChangeSave")]
        public async Task<IActionResult> ChangeSave(SocialUser user)
        {
            var userExist = _dbUser.Get(user.SocialUserId);
            if (userExist.Firstname != user.Firstname)
                userExist.Firstname = user.Firstname ; 
            if (userExist.Lastname != user.Lastname)
                userExist.Lastname = user.Lastname;
            if (userExist.Birthday != user.Birthday)
                userExist.Birthday = user.Birthday;
            if (userExist.City != user.City)
                userExist.City = user.City;
            if (userExist.PhoneNumber != user.PhoneNumber)
                userExist.PhoneNumber = user.PhoneNumber;
            if (userExist.Gender != user.Gender)
                userExist.Gender = user.Gender;
            try
            {
                if (userExist != null)
                {
                    _dbUser.Update(userExist);
                    _dbUser.Save();
                    return Ok();
                }
                else
                    return BadRequest("User not exist");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [Route("DeletePage")]
        public async Task<ActionResult> DeletePageAsync()
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            var photos = _dbPhoto.GetPhotosUser(user.SocialUserId);
            foreach (var item in photos)
            {
                _dbPhoto.Delete(item);
            }
            var posts = _dbPost.GetUserPosts(user.SocialUserId).ToList();
            foreach (var item in posts)
            {
                _dbPost.Delete(item);
            }
            var comments = _dbComment.GetUserComments(user.SocialUserId).ToList();
            foreach (var item in comments)
            {
                _dbComment.Delete(item);
            }
            var likes = _dbLikePost.GetLikeSUser(user.SocialUserId).ToList();
            foreach (var item in likes)
            {
                _dbLikePost.Delete(item);
            }
            _dbUser.Delete(user.SocialUserId);
            _dbUser.Save();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok();
        }
    }
}
