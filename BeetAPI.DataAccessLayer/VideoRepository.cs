using BeetAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetAPI.DataAccessLayer
{
    public class VideoRepository
    {
        AppDbContext db;

        public VideoRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public void Create(Video video)
        {
            db.Videos.Add(video);
        }

        public IEnumerable<Video> Get()
        {
            return db.Videos;
        }

        public Video Get(int id)
        {
            return db.Videos.Find(id);
        }

        public Video Get(string path)
        {
            return db.Videos.FirstOrDefault(p => p.Path == path);
        }

        public IEnumerable<Video> GetVideoUser(int id)
        {
            return db.Videos.Where(p => p.VideoUser.SocialUserId == id).ToList();
        }

        public void Update(Video video)
        {
            db.Entry(video).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Video video = db.Videos.Find(id);
            if (video != null)
                db.Videos.Remove(video);
        }

        public void Delete(Video video)
        {
            db.Videos.Remove(video);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
