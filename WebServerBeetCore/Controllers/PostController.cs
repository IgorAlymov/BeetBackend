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
using WebServerBeetCore.Models;

namespace WebServerBeetCore.Controllers
{
    [Route("api")]
    [ApiController]
    public class PostController : ControllerBase
    {
        PostRepository _dbPost;
        SocialUserRepository _dbUser;
        PhotoRepository _dbPhoto;
        IHostingEnvironment _appEnvironment;

        public PostController(PostRepository repPost, SocialUserRepository repUser, PhotoRepository repPhoto, IHostingEnvironment appEnvironment)
        {
            _dbPost = repPost;
            _dbUser = repUser;
            _dbPhoto = repPhoto;
            _appEnvironment = appEnvironment;
        }
        //
        [Route("getActiveUserPosts/{id}")]
        public IEnumerable<Post> GetActiveUserPosts(int id)
        {
            var posts = _dbPost.GetUserPosts(id);
            return posts;
        }
        //
        [Route("getImagePosts/{id}")]
        public IActionResult GetImagePosts(int id)
        {
            var post = _dbPost.Get(id);
            var photo = post.AttachedPhotos.ToList();

            if (post != null && photo.Count != 0)
            {
                int idAvatar = photo[0].PhotoId;
                var avatar = _dbPhoto.Get(idAvatar);

                return Ok(new
                {
                    avatarUrl = Path.Combine("http://localhost:5001", avatar.Path)
                });
            }
            else
                return NotFound();
        }
        //
        [Route("postAddPost/{text}")]
        public async Task<IActionResult> PostAddPost(string text, [FromForm]UploadFileModel model)
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
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
                    _dbPhoto.Save();
                    
                    var post = new Post()
                    {
                        Text = text,
                        Date = DateTime.Now,
                        LikesCounter = 0
                    };

                    post.AttachedPhotos.Add(file);
                    post.Author = user;
                    _dbPost.Create(post);
                    _dbPost.Save();
                }
                else
                {
                    var post = new Post()
                    {
                        Text = text,
                        Date = DateTime.Now,
                        LikesCounter = 0
                    };
                    post.Author = user;
                    _dbPost.Create(post);
                    _dbPost.Save();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok();
        }
        //
        [Route("api/addLikePost/{idPost}")]
        public async Task<IActionResult> AddLikePost(int idPost)
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);

            var post = _dbPost.Get(idPost);
            post.LikesCounter++;
            _dbPost.Update(post);
            _dbPost.Save();

            return Ok();
        }
    }
}
