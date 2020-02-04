using BeetAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.DataAccessLayer
{
    public class MessageRepository
    {
        AppDbContext db;

        public MessageRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public void Create(Message message)
        {
            db.Messages.Add(message);
        }

        public IEnumerable<Message> Get()
        {
            return db.Messages;
        }

        public Message Get(int id)
        {
            return db.Messages.Find(id);
        }

        public void Update(Message message)
        {
            db.Entry(message).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Message message = db.Messages.Find(id);
            if (message != null)
                db.Messages.Remove(message);
        }

        public void Delete(Message message)
        {
            db.Messages.Remove(message);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
