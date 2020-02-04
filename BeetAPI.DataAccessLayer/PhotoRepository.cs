using BeetAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetAPI.DataAccessLayer
{
    public class PhotoRepository
    {
        AppDbContext db;

        public PhotoRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public void Create(Photo photo)
        {
            db.Photos.Add(photo);
        }

        public IEnumerable<Photo> Get()
        {
            return db.Photos;
        }

        public Photo Get(int id)
        {
            return db.Photos.Find(id);
        }

        public Photo Get(string path)
        {
            return db.Photos.FirstOrDefault(p => p.Path == path);
        }

        public IEnumerable<Photo> GetPhotosUser(int id)
        {
            return db.Photos.Where(p => p.PhotoUsers.SocialUserId == id).ToList();
        }

        public Photo GetAvatar(int idAvatar)
        {
            return db.Photos.First(a => a.PhotoId == idAvatar);
        }

        public void Update(Photo photo)
        {
            db.Entry(photo).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Photo photo = db.Photos.Find(id);
            if (photo != null)
                db.Photos.Remove(photo);
        }

        public void Delete(Photo photo)
        {
            db.Photos.Remove(photo);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public int GetAvatarGroup(string idGroup)
        {
            var photo = db.Photos.Where(p=>p.Name==idGroup).First();
            return photo.PhotoId;
        }
    }
}
