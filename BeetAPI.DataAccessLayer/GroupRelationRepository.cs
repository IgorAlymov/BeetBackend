using BeetAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeetAPI.DataAccessLayer
{
    public class GroupRelationRepository
    {
        AppDbContext db;

        public GroupRelationRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public void Create(SocialUser user, Group group)
        {
            GroupRelation groupRel = new GroupRelation()
            {
                UserId = user.SocialUserId,
                GroupId = group.GroupId,
                User = user,
                Group=group
            };
            db.GroupRelations.Add(groupRel);
        }

        public IEnumerable<Group> GetGroup(int userId)
        {
            List<Group> groups = new List<Group>();
            var groupRelation = db.GroupRelations.Where(f => f.UserId == userId).Include(f=>f.Group).ToList();
            foreach (var group in groupRelation)
            {
                groups.Add(group.Group);
            }
            return groups;
        }

        public IEnumerable<SocialUser> GetSubscribers(int groupId)
        {
            List<SocialUser> users = new List<SocialUser>();
            var groupRelation = db.GroupRelations.Where(f => f.GroupId == groupId).Include(u=>u.User).ToList();
            foreach (var group in groupRelation)
            {
                users.Add(group.User);
            }
            return users;
        }

        public void Update(GroupRelation groupRelation)
        {
            db.Entry(groupRelation).State = EntityState.Modified;
        }

        public void Delete(int idRemoveGroup,int idSub)
        {
            var groupRemove = db.GroupRelations
                .Where(f => f.GroupId == idRemoveGroup && f.UserId==idSub)
                .ToList();
            if (groupRemove[0] != null)
                db.GroupRelations.Remove(groupRemove[0]);
        }

        public void Delete(GroupRelation groupRelation)
        {
            db.GroupRelations.Remove(groupRelation);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
