using BeetAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetAPI.DataAccessLayer
{
    public class UserGroupRepository
    {
        AppDbContext db;

        public UserGroupRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public void Create(Group userGroup)
        {
            db.UserGroups.Add(userGroup);
        }

        public IEnumerable<Group> Get()
        {
            return db.UserGroups;
        }

        public Group Get(int id)
        {
            return db.UserGroups.Where(g=>g.GroupId == id).FirstOrDefault();
        }

        public Group Get(string name)
        {
            return db.UserGroups
                .Include(g=>g.UsersForGroup)
                .SingleOrDefault(a=>a.Name==name);
        }

        public IEnumerable<Group> GetUserGroups(int id)
        {
            var allGroups = db.UserGroups.Include(g => g.UsersForGroup).ToList();
            var groups = new List<Group>();
            foreach (var group in allGroups)
            {
                if (group.UsersForGroup.Any(u => u.SocialUserId == id))
                    groups.Add(group);
            }
            return groups;

            //return db.UserGroups.Where(g=>g.UsersForGroup.Any(user=>user.SocialUserId==id));
        }

        public void Update(Group userGroup)
        {
            db.Entry(userGroup).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Group userGroup = db.UserGroups.Find(id);
            if (userGroup != null)
                db.UserGroups.Remove(userGroup);
        }

        public void Delete(Group userGroup)
        {
            db.UserGroups.Remove(userGroup);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
