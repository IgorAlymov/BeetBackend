using BeetAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.DataAccessLayer
{
    public class LikeCommentRepository
    {
        AppDbContext db;

        public LikeCommentRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public void Create(LikeComment likeComment)
        {
            db.LikeComments.Add(likeComment);
        }

        public IEnumerable<LikeComment> Get()
        {
            return db.LikeComments;
        }

        public LikeComment Get(int id)
        {
            return db.LikeComments.Find(id);
        }

        public void Update(LikeComment likeComment)
        {
            db.Entry(likeComment).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            LikeComment likeComment = db.LikeComments.Find(id);
            if (likeComment != null)
                db.LikeComments.Remove(likeComment);
        }

        public void Delete(LikeComment likeComment)
        {
            db.LikeComments.Remove(likeComment);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
