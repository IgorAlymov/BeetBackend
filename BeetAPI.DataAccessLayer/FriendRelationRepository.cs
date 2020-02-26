using BeetAPI.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BeetAPI.DataAccessLayer
{
    public class FriendRelationRepository
    {
        AppDbContext db;

        public FriendRelationRepository(AppDbContext appDbContext)
        {
            db = appDbContext;
        }

        public void Create(SocialUser friendAdding, SocialUser friendAdded)
        {
            FriendRelation friends = new FriendRelation()
            {
                UserIdAdding=friendAdding.SocialUserId,
                UserIdAdded=friendAdded.SocialUserId,
                UserAdding=friendAdding,
                UserAdded=friendAdded
            };
            db.FriendRelations.Add(friends);
        }

        public IEnumerable<SocialUser> GetSubscribersFriend(int id)
        {
            List<SocialUser> friends = new List<SocialUser>();
            var friendRelation = db.FriendRelations.Where(f => f.UserIdAdded == id).ToList();
            foreach (var item in friendRelation)
            {
                if (id != item.UserIdAdded)
                {
                    var friend = db.SocialUsers.Find(item.UserIdAdded);
                    friends.Add(friend);
                }
                else
                {
                    var friend = db.SocialUsers.Find(item.UserIdAdding);
                    friends.Add(friend);
                }
            }
            return friends;
        }

        public IEnumerable<SocialUser> GetSubscription(int id)
        {
            List<SocialUser> friends = new List<SocialUser>();
            var friendRelation = db.FriendRelations.Where(f => f.UserIdAdding == id).ToList();
            foreach (var item in friendRelation)
            {
                if (id != item.UserIdAdded)
                {
                    var friend = db.SocialUsers.Find(item.UserIdAdded);
                    friends.Add(friend);
                }
                else
                {
                    var friend = db.SocialUsers.Find(item.UserIdAdding);
                    friends.Add(friend);
                }
            }
            return friends;
        }

        public void Update(FriendRelation friendRelation)
        {
            db.Entry(friendRelation).State = EntityState.Modified;
        }

        public void Delete(int idRemoveFriend)
        {
            var friendRemove = db.FriendRelations
                .Where(f => f.UserIdAdded == idRemoveFriend)
                .ToList();
            if (friendRemove != null)
                db.FriendRelations.Remove(friendRemove[0]);
        }

        public void Delete(FriendRelation friendRelation)
        {
            db.FriendRelations.Remove(friendRelation);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
