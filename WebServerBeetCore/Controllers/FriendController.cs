using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BeetAPI.DataAccessLayer;
using BeetAPI.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebServerBeetCore.Controllers
{
    [Route("api")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        PostRepository _dbPost;
        SocialUserRepository _dbUser;
        PhotoRepository _dbPhoto;
        IHostingEnvironment _appEnvironment;
        FriendRelationRepository _friendRelation;

        public FriendController(PostRepository repPost, SocialUserRepository repUser, PhotoRepository repPhoto,FriendRelationRepository friendRelation, IHostingEnvironment appEnvironment)
        {
            _dbPost = repPost;
            _dbUser = repUser;
            _dbPhoto = repPhoto;
            _appEnvironment = appEnvironment;
            _friendRelation = friendRelation;
        }

        [Route("GetAllUsers")]
        public IEnumerable<SocialUser> GetAllUsers()
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            var users = _dbUser.Get().ToList();
            var allUsers = users.Where(a => a.SocialUserId != user.SocialUserId).ToList();
            return allUsers;
        }

        [Route("GetRemoveFriend/{id}")]
        public IActionResult GetRemoveFriend(int id)
        {
            _friendRelation.Delete(id);
            _friendRelation.Save();
            return Ok();
        }
        
        [Route("GetAddNewFriend/{idF}")]
        public IActionResult GetAddNewFriend(int idF)
        {
            string emailUser = User.Identity.Name;
            var AUser = _dbUser.Get(emailUser);
            var FUser = _dbUser.Get(idF);
            bool foundet = false;
            var AFriends = _friendRelation.GetSubscription(AUser.SocialUserId);
            foreach (var friend in AFriends)
            {
                if (friend.SocialUserId == idF)
                   foundet = true;
            }
            if (!foundet)
            {
                _friendRelation.Create(AUser, FUser);
                _friendRelation.Save();
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpGet("GetFriend/{id}")]
        public SocialUser GetFriend(int id)
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            var friends = _friendRelation.GetSubscribersFriend(id);
            foreach (var friend in friends)
            {
                if (friend.SocialUserId == user.SocialUserId)
                    return friend;
                else
                    return null;
            }
            return null;
        }

        [HttpGet("GetFriends")]
        public IEnumerable<SocialUser> GetFriends()
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            var friends = _friendRelation.GetSubscription(user.SocialUserId).ToList();
            return friends;
        }

        [HttpGet("GetSubscruptionFriends/{id}")]
        public IEnumerable<SocialUser> GetSubscruptionFriends(int id)
        {
            var friends = _friendRelation.GetSubscription(id);
            return friends;
        }

        [Route("GetFriendPhoto/{id}")]
        public IActionResult GetAllUserPhoto(int id)
        {
            List<string> listPhoto = new List<string>();
            var photos = _dbPhoto.GetPhotosUser(id);
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

        [HttpGet("GetFriendPost/{id}")]
        public IEnumerable<Post> GetActiveUserPosts(int id)
        {
            var posts = _dbPost.GetUserPosts(id).ToList();
            return posts;
        }
    }
}
