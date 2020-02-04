using BeetAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BeetAPI.DataAccessLayer
{
    public class CommentRepository
    {
        AppDbContext db;

        public CommentRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public void Create(Comment comment)
        {
            db.Comments.Add(comment);
        }

        public IEnumerable<Comment> Get()
        {
            return db.Comments;
        }

        public Comment Get(int id)
        {
            return db.Comments.Find(id);
        }

        public void Update(Comment comment)
        {
            db.Entry(comment).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment != null)
                db.Comments.Remove(comment);
        }

        public void Delete(Comment comment)
        {
            db.Comments.Remove(comment);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
