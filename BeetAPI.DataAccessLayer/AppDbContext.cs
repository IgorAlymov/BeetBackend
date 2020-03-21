using BeetAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeetAPI.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LikeComment> LikeComments { get; set; }
        public DbSet<LikePost> LikePosts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<SocialUser> SocialUsers { get; set; }
        public DbSet<Group> UserGroups { get; set; }
        public DbSet<FriendRelation> FriendRelations { get; set; }
        public DbSet<GroupRelation> GroupRelations { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<SocialUser>()
                .HasMany(e => e.MessageAuthor)
                .WithOne(f => f.Author);
            modelBuilder.Entity<SocialUser>()
                .HasMany(e => e.MessageReceiver)
                .WithOne(f => f.Receiver);*/
            modelBuilder.Entity<FriendRelation>()
                .HasKey(k=> new { k.UserIdAdding , k.UserIdAdded});
            modelBuilder.Entity<GroupRelation>()
                .HasKey(k => new {k.UserId,k.GroupId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
