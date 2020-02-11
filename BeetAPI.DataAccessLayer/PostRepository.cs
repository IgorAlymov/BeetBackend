using BeetAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetAPI.DataAccessLayer
{
    public class PostRepository
    {
        AppDbContext db;

        public PostRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public void Create(Post post)
        {
            db.Posts.Add(post);
        }

        public IEnumerable<Post> Get()
        {
            return db.Posts;
        }

        public Post Get(int id)
        {
            return db.Posts
                .Include(p => p.AttachedPhotos)
                .Include(l=>l.LikePost)
                .Include(c => c.AttachedComments)
                .Include(g => g.UserGroupForPost)
                .SingleOrDefault(a => a.PostId == id);
        }

        public IEnumerable<Post> GetUserPosts(int id)
        {
            var allPostUser = db.Posts.Where(p => p.AuthorId == id).ToList();
            List<Post> posts = new List<Post>();
            for (int i = allPostUser.Count()-1; i >= 0; i--)
            {
                posts.Add(allPostUser[i]);
            }
            return posts;
        }

        public void Update(Post post)
        {
            db.Entry(post).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Post postRemove = db.Posts.Find(id);
            if (postRemove != null)
                db.Posts.Remove(postRemove);
        }

        public void Delete(Post post)
        {
            db.Posts.Remove(post);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
