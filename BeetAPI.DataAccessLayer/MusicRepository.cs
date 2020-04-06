using BeetAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetAPI.DataAccessLayer
{
    public class MusicRepository
    {
        AppDbContext db;

        public MusicRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public void Create(Music music)
        {
            db.Musics.Add(music);
        }

        public IEnumerable<Music> Get()
        {
            return db.Musics;
        }

        public Music Get(int id)
        {
            return db.Musics.Find(id);
        }

        public Music Get(string path)
        {
            return db.Musics.FirstOrDefault(p => p.Path == path);
        }

        public IEnumerable<Music> GetMusicUser(int id)
        {
            return db.Musics.Where(p => p.MusicUser.SocialUserId == id).ToList();
        }

        public void Update(Video music)
        {
            db.Entry(music).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Music music = db.Musics.Find(id);
            if (music != null)
                db.Musics.Remove(music);
        }

        public void Delete(Music music)
        {
            db.Musics.Remove(music);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
