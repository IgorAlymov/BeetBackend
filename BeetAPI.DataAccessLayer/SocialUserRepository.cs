using BeetAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetAPI.DataAccessLayer
{
    public class SocialUserRepository
    {
        AppDbContext db;

        public SocialUserRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public void Create(SocialUser socialUser)
        {
            db.SocialUsers.Add(socialUser);
        }

        public IEnumerable<SocialUser> Get()
        {
            return db.SocialUsers;
        }

        public SocialUser Get(int id)
        {
            return db.SocialUsers.Find(id);
        }

        public  SocialUser Get(string email)
        {
            var user = db.SocialUsers.Where(a => a.Email == email).FirstOrDefault();
            return user;
        }

        public void Update(SocialUser socialUser)
        {
            db.Entry(socialUser).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            SocialUser socialUser = db.SocialUsers.Find(id);
            if (socialUser != null)
                db.SocialUsers.Remove(socialUser);
        }

        public void Delete(SocialUser socialUser)
        {
            db.SocialUsers.Remove(socialUser);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
