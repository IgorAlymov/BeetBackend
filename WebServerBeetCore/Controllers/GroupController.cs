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
        PostRepository _dbPost;
        SocialUserRepository _dbUser;
        PhotoRepository _dbPhoto;
        GroupRelationRepository _dbGroupRelation;
        IHostingEnvironment _appEnvironment;

        public GroupController(PostRepository repPost, 
            UserGroupRepository repGroup, 
            SocialUserRepository repUser, 
            PhotoRepository repPhoto, 
            GroupRelationRepository groupRelation,
            IHostingEnvironment appEnvironment)
        {
            _dbPost = repPost;
            _dbGroup = repGroup;
            _dbUser = repUser;
            _dbPhoto = repPhoto;
            _dbGroupRelation = groupRelation;
            _appEnvironment = appEnvironment;
        }
        
        [Route("GetSubscribers/{id}")]
        public IEnumerable<SocialUser> GetSubscribers(int id)
        {
            var users = _dbGroupRelation.GetSubscribers(id);
            return users;
        }

        [Route("GetSubscription/{id}")]
        public SocialUser GetSubscription(int id)
        {
            var users = _dbGroupRelation.GetSubscribers(id);
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            foreach (var item in users)
            {
                if (item.SocialUserId == user.SocialUserId)
                    return item;
            }
            return null;
        }

        [Route("GetAllGroups")]
        public IEnumerable<Group> GetAllGroups()
        {
            var groups = _dbGroup.Get().ToList();
            return groups;
        }

        [Route("GetCommunity/{id}")]
        public Group GetCommunity(int id)
        {
            var group = _dbGroup.Get(id);

            return group;
        }

        [Route("PostAddGroup/{name}")]
        public async Task<IActionResult> PostAddGroup([FromForm]UploadFileModel uploadedFile,string name)
        {
            string textCom =name.Remove(0, 1);
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                string path = Path.Combine("Files", uploadedFile.File.FileName);
                using (var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath, path), FileMode.Create))
                {
                    await uploadedFile.File.CopyToAsync(fileStream);
                }

                Group group = new Group() { Name = textCom ,AuthorId=user.SocialUserId};
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
        
        [Route("AddGroupSubscriber/{id}")]
        public IActionResult GetAddGroupUser(int id)
        {
            bool found = false;
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            var group = _dbGroup.Get(id);
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

        [Route("RemoveGroupSubscriber/{id}")]
        public IActionResult GetRemoveFriend(int id)
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            _dbGroupRelation.Delete(id,user.SocialUserId);
            _dbGroupRelation.Save();
            return Ok();
        }

        [Route("GetMyGroups/{id}")]
        public IEnumerable<Group> GetMyGroups(int id)
        {
            return _dbGroupRelation.GetGroup(id);
        }

        [Route("GetMyGroup")]
        public Group GetMyGroup()
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            return _dbGroup.GetMyGroup(user.SocialUserId);
        }

        [Route("GetImageGroup/{id}")]
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

        [Route("AddPostGroup/{id}/{text}")]
        public async Task<IActionResult> AddPostGroup(int id,string text, [FromForm]UploadFileModel model)
        {
            string textPost;
            if (text != null)
                textPost = text.Remove(0, 1);
            else
                textPost = "";
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            Group group = _dbGroup.Get(id);
            try
            {
                if (model.File != null)
                {
                    string path = Path.Combine("Files", model.File.FileName);
                    using (var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath, path), FileMode.Create))
                    {
                        await model.File.CopyToAsync(fileStream);
                    }

                    Photo file = new Photo { Name = model.File.FileName, Path = path };
                    _dbPhoto.Create(file);
                    var post = new Post()
                    {
                        Text = textPost,
                        Date = DateTime.Now,
                        LikesCounter = 0,
                        UserGroupForPost = group
                    };
                    post.AttachedPhotos.Add(file);
                    _dbPost.Create(post);
                    group.Posts.Add(post);
                    _dbGroup.Update(group);
                    _dbPost.Save();
                }
                else
                {
                    var post = new Post()
                    {
                        Text = textPost,
                        Date = DateTime.Now,
                        LikesCounter = 0,
                        UserGroupForPost=group
                    };
                    _dbPost.Create(post);

                    group.Posts.Add(post);
                    _dbGroup.Update(group);
                    _dbPost.Save();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("GetGroupPost/{id}")]
        public IEnumerable<Post> GetGroupPost(int id)
        {
            var group = _dbGroup.GetPosts(id);
            var posts = group.Posts.ToList();
            foreach (var post in posts)
            {
                post.UserGroupForPost = null;
            }
            return posts;
        }

        [HttpGet("GetAllGroupPosts")]
        public IEnumerable<Post> GetAllGroupPosts()
        {
            var posts = _dbPost.GetAllGroupPosts();
            foreach (var item in posts)
            {
                item.AuthorId = item.UserGroupForPost.GroupId;
                item.UserGroupForPost = null;
            }
            return posts;
        }
    }
}
