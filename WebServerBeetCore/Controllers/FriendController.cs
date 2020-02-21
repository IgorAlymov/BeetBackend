using System;
using System.Collections.Generic;
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
        SocialUserRepository _dbUser;
        PhotoRepository _dbPhoto;
        IHostingEnvironment _appEnvironment;
        FriendRelationRepository _friendRelation;

        public FriendController(SocialUserRepository repUser, PhotoRepository repPhoto,FriendRelationRepository friendRelation, IHostingEnvironment appEnvironment)
        {
            _dbUser = repUser;
            _dbPhoto = repPhoto;
            _appEnvironment = appEnvironment;
            _friendRelation = friendRelation;
        }

        //
        [Route("getallusers/{id}")]
        public IEnumerable<SocialUser> GetAllUsers(int id)
        {
            var users = _dbUser.Get().ToList();
            var allUser = users.Where(a => a.SocialUserId != id);
            return allUser;
        }
        //
        [Route("GetRemoveFriend/{id}")]
        public IActionResult GetRemoveFriend(int id)
        {
            _friendRelation.Delete(id);
            _friendRelation.Save();
            return Ok();
        }
        //
        [Route("getAddNewFriend/{idU}/{idF}")]
        public IActionResult GetAddNewFriend(int idU, int idF)
        {
            var AUser = _dbUser.Get(idU);
            var FUser = _dbUser.Get(idF);
            bool foundet = false;
            var AFriends = _friendRelation.Get(idU);
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
        //
        [HttpGet("getFriends/{id}")]
        public IEnumerable<SocialUser> GetFriends(int id)
        {
            var friends = _friendRelation.Get(id);
            return friends;
        }
    }
}
