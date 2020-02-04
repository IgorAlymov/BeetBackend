using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeetAPI.DataAccessLayer;
using BeetAPI.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WebServerBeetCore.Models;

namespace WebServerBeetCore.Controllers
{
    [Route("api")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        UserGroupRepository _dbGroup;
        SocialUserRepository _dbUser;
        PhotoRepository _dbPhoto;
        GroupRelationRepository _dbGroupRelation;
        IHostingEnvironment _appEnvironment;

        public GroupController(UserGroupRepository repGroup, SocialUserRepository repUser, PhotoRepository repPhoto, GroupRelationRepository groupRelation, IHostingEnvironment appEnvironment)
        {
            _dbGroup = repGroup;
            _dbUser = repUser;
            _dbPhoto = repPhoto;
            _dbGroupRelation = groupRelation;
            _appEnvironment = appEnvironment;
        }
        //
        [Route("getSubscribers/{name}")]
        public IEnumerable<SocialUser> GetSubscribers(string name)
        {
            var group = _dbGroup.Get(name);
            var users = _dbGroupRelation.GetSubscribers(group.GroupId);
            return users;
        }
        //
        [Route("getAllGroups")]
        public IEnumerable<Group> GetAllGroups()
        {
            var groups = _dbGroup.Get().ToList();
            return groups;
        }

        //
        [Route("PostAddGroup/{name}")]
        public async Task<IActionResult> PostAddGroup([FromForm]UploadFileModel uploadedFile,string name)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                string path = Path.Combine("Files", uploadedFile.File.FileName);
                using (var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath, path), FileMode.Create))
                {
                    await uploadedFile.File.CopyToAsync(fileStream);
                }

                //костыль
                Group group = new Group() { Name = name};
                _dbGroup.Create(group);
                _dbGroup.Save();

                Photo file = new Photo { Name = group.GroupId.ToString(), Path = path };
                _dbPhoto.Create(file);
                _dbGroup.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        //
        [Route("getAddGroupUser/{idU}/{idF}")]
        public IActionResult GetAddGroupUser(int idU, int idF)
        {
            bool found = false;
            var user = _dbUser.Get(idU);
            var group = _dbGroup.Get(idF);
            var followers = _dbGroupRelation.GetSubscribers(group.GroupId);
            if(followers!=null)
            foreach (var item in followers)
            {
                if (item.SocialUserId == user.SocialUserId)
                    found = true;
            }
            if (user != null && group != null && !found)
            {
                _dbGroupRelation.Create(user, group);
                _dbGroup.Save();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //
        [Route("getMyGroups/{id}")]
        public IEnumerable<Group> GetMyGroups(int id)
        {
            return _dbGroupRelation.GetGroup(id);
        }
        //
        [Route("getImageGroup/{id}")]
        public IActionResult GetImageGroup(int id)
        {
            var groups = _dbGroup.Get(id);
            if (groups != null)
            {
                int idAvatar = _dbPhoto.GetAvatarGroup(groups.GroupId.ToString());
                Photo avatar = _dbPhoto.Get(idAvatar);
                if (avatar != null)
                {
                    return Ok(new
                    {
                        avatarUrl = Path.Combine("http://localhost:5001", avatar.Path)
                    });
                }
                else
                    return NotFound();
            }
            else
                return NotFound();
        }
    }
}
