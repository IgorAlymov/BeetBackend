using BeetAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetAPI.DataAccessLayer
{
    public class LikePostRepository
    {
        AppDbContext db;

        public LikePostRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public void Create(LikePost likePost)
        {
            db.LikePosts.Add(likePost);
        }

        public IEnumerable<LikePost> Get()
        {
            return db.LikePosts;
        }

        public IEnumerable<LikePost> GetLikeSUser(int idUser)
        {
            var likes = db.LikePosts.Where(a => a.UserId == idUser);
            return likes;
        }

        public LikePost Get(int id)
        {
            return db.LikePosts.Find(id);
        }

        public void Update(LikePost likePost)
        {
            db.Entry(likePost).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            LikePost likePost = db.LikePosts.Find(id);
            if (likePost != null)
                db.LikePosts.Remove(likePost);
        }

        public void Delete(LikePost likePost)
        {
            db.LikePosts.Remove(likePost);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
