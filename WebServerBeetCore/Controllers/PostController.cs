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
        LikePostRepository _dbLikePost;
        CommentRepository _dbComment;

        public PostController(PostRepository repPost,
            SocialUserRepository repUser,
            PhotoRepository repPhoto,
            IHostingEnvironment appEnvironment,
            LikePostRepository likePost,
            CommentRepository comment)
        {
            _dbPost = repPost;
            _dbUser = repUser;
            _dbPhoto = repPhoto;
            _appEnvironment = appEnvironment;
            _dbLikePost = likePost;
            _dbComment = comment;
        }

        [HttpGet("GetActiveUserPosts")]
        public IEnumerable<Post> GetActiveUserPosts()
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            var posts = _dbPost.GetUserPosts(user.SocialUserId).ToList();
            return posts;
        }

        [HttpGet("GetAllPosts")]
        public IEnumerable<Post> GetAllPosts()
        {
            var posts = _dbPost.GetUsersPosts().ToList();
            return posts;
        }

        [Route("GetImagePosts/{id}")]
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
                    photoUrl = Path.Combine("http://localhost:5001", avatar.Path)
                });
            }
            else
                return Ok(new
                {
                    photoUrl = ""
                });
        }

        [Route("PostAddPost/{text}")]
        public async Task<IActionResult> PostAddPost(string text, [FromForm]UploadFileModel model)
        {
            string textPost;
            if (text != null)
                textPost = text.Remove(0, 1);
            else
                textPost = "";
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            try
            {
                if (model.File != null)
                {
                    string path = Path.Combine("Photos", model.File.FileName);
                    using (var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath, path), FileMode.Create))
                    {
                        await model.File.CopyToAsync(fileStream);
                    }

                    Photo file = new Photo { Name = model.File.FileName, Path = path };
                    _dbPhoto.Create(file);
                    _dbPhoto.Save();

                    var post = new Post()
                    {
                        Text = textPost,
                        Date = DateTime.Now,
                        LikesCounter = 0,
                        UserGroupForPost=null
                    };

                    post.AttachedPhotos.Add(file);
                    post.AuthorId = user.SocialUserId;
                    _dbPost.Create(post);
                    _dbPost.Save();
                }
                else
                {
                    var post = new Post()
                    {
                        Text = textPost,
                        Date = DateTime.Now,
                        LikesCounter = 0,
                        UserGroupForPost = null
                    };
                    post.AuthorId = user.SocialUserId;
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

        [Route("AddComment/{text}")]
        public IActionResult AddComment(string text)
        {
            string textComment = text.Remove(0, 1);
            var postAndComment = textComment.Split(" ",2);
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            int idAvatar = (int)user.AvatarPhotoId;
            var avatar = _dbPhoto.GetAvatar(idAvatar);
            if (avatar == null)
            {
                avatar = new Photo();
                avatar.Path = "Photos/noAvatar.png";
            }
            var comment = new Comment()
            {
                AuthorId = user.SocialUserId,
                AuthorName=user.Firstname + " " + user.Lastname,
                Date = DateTime.Now,
                Text = postAndComment[1],
                PostId = Int32.Parse(postAndComment[0]),
                AvatarAuthor = Path.Combine("http://localhost:5001", avatar.Path)
            };
            _dbComment.Create(comment);
            _dbComment.Save();

            var post = _dbPost.Get(Int32.Parse(postAndComment[0]));
            post.AttachedComments.Add(comment);
            _dbPost.Update(post);
            _dbPost.Save();

            return Ok();
        }

        [Route("GetComment/{id}")]
        public IEnumerable<Comment> GetComment(int id)
        {
            var post = _dbPost.Get(id);
            var comments = post.AttachedComments.ToList();
            return comments;
        }

        [Route("GetDeletePost/{id}")]
        public IActionResult GetDeletePost(int id)
        {
            var post = _dbPost.Get(id);
            _dbPost.Delete(post);
            _dbPost.Save();
            return Ok();
        }

        [Route("GetDeleteComment/{id}")]
        public IActionResult GetDeleteComment(int id)
        {
            var comment = _dbComment.Get(id);
            _dbComment.Delete(comment);
            _dbComment.Save();
            return Ok();
        }

        [Route("AddLikePost/{id}")]
        public IActionResult AddLikePost(int id)
        {
            var post = _dbPost.Get(id);
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            var like = new LikePost()
            {
                PostId = post.PostId,
                UserId = user.SocialUserId
            };
            _dbLikePost.Create(like);
            _dbLikePost.Save();

            post.LikePost.Add(like);
            _dbPost.Update(post);
            _dbPost.Save();
            return Ok();
        }

        [Route("GetLikePost/{id}")]
        public IActionResult GetLikePost(int id)
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            var post = _dbPost.Get(id);
            var likes = post.LikePost.ToList();
            string[] iconsLike = new string[] { "favorite_border", "favorite" };
            string icon = "favorite_border";
            for (int i = 0; i < likes.Count; i++)
            {
                if (likes[i].UserId == user.SocialUserId)
                {
                    icon = iconsLike[1];
                    return Ok(new
                    {
                        icon = icon,
                        likesCounter = likes.Count,
                        postId = id
                    });
                }
                else
                {
                    icon = iconsLike[0];
                }
            }
            return Ok(new
            {
                icon = icon,
                likesCounter = likes.Count,
                postId = id
            });
        }

        [Route("RemoveLikePost/{id}")]
        public IActionResult RemoveLikePost(int id)
        {
            string emailUser = User.Identity.Name;
            var user = _dbUser.Get(emailUser);
            var post = _dbPost.Get(id);
            var likes = post.LikePost.ToList();
            for (int i = 0; i < likes.Count; i++)
            {
                if (likes[i].UserId == user.SocialUserId && likes[i].PostId==post.PostId)
                {
                    _dbLikePost.Delete(likes[i]);
                    _dbLikePost.Save();
                }
            }
            return Ok();
        }
    }
}
