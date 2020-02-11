using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BeetAPI.DataAccessLayer;
using BeetAPI.Domain;
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
        SocialUserRepository _dbUser;
        PhotoRepository _dbPhoto;
        IHostingEnvironment _appEnvironment;

        public UserController(SocialUserRepository rep, PhotoRepository repPhoto, IHostingEnvironment appEnvironment)
        {
            _dbUser = rep;
            _dbPhoto = repPhoto;
            _appEnvironment = appEnvironment;
        }
        //
        [Route("getallusers/{id}")]
        public IEnumerable<SocialUser> GetAllUsers(int id)
        {
            var users = _dbUser.Get().ToList();
            var allUser = users.Where(a => a.SocialUserId != id);
            return allUser;
        }
        
        [Route("GetUserAvatar")]
        public IActionResult GetUserAvatar()
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            if (user == null) return NotFound();

            int idAvatar = (int)user.AvatarPhotoId;
            var avatar = _dbPhoto.GetAvatar(idAvatar);

            return Ok(new {
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
                string path = Path.Combine("Files", model.File.FileName);
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
                string path = Path.Combine("Files", model.File.FileName);
                using (var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath, path), FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
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
    }
}
