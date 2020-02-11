﻿// <auto-generated />
using System;
using BeetAPI.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WebServerBeetCore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200210181126_ChangeComment")]
    partial class ChangeComment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BeetAPI.Domain.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("LikesCounter");

                    b.Property<int>("PostId");

                    b.Property<int?>("SocialUserId");

                    b.Property<string>("Text");

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("SocialUserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BeetAPI.Domain.FriendRelation", b =>
                {
                    b.Property<int>("UserIdAdding");

                    b.Property<int>("UserIdAdded");

                    b.Property<int?>("UserAddedSocialUserId");

                    b.Property<int?>("UserAddingSocialUserId");

                    b.HasKey("UserIdAdding", "UserIdAdded");

                    b.HasIndex("UserAddedSocialUserId");

                    b.HasIndex("UserAddingSocialUserId");

                    b.ToTable("FriendRelations");
                });

            modelBuilder.Entity("BeetAPI.Domain.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CoverPhotoId");

                    b.Property<string>("Name");

                    b.HasKey("GroupId");

                    b.HasIndex("CoverPhotoId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("BeetAPI.Domain.GroupRelation", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("GroupId");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupRelations");
                });

            modelBuilder.Entity("BeetAPI.Domain.LikeComment", b =>
                {
                    b.Property<int>("LikeCommentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CommentId");

                    b.Property<int?>("UserSocialUserId");

                    b.HasKey("LikeCommentId");

                    b.HasIndex("CommentId");

                    b.HasIndex("UserSocialUserId");

                    b.ToTable("LikeComments");
                });

            modelBuilder.Entity("BeetAPI.Domain.LikePost", b =>
                {
                    b.Property<int>("LikePostId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PostId");

                    b.Property<int?>("SocialUserId");

                    b.Property<int>("UserId");

                    b.HasKey("LikePostId");

                    b.HasIndex("PostId");

                    b.HasIndex("SocialUserId");

                    b.ToTable("LikePosts");
                });

            modelBuilder.Entity("BeetAPI.Domain.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuthorSocialUserId");

                    b.Property<int?>("ReceiverSocialUserId");

                    b.Property<string>("Text");

                    b.Property<bool>("WasReaded");

                    b.HasKey("MessageId");

                    b.HasIndex("AuthorSocialUserId");

                    b.HasIndex("ReceiverSocialUserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("BeetAPI.Domain.Photo", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MessageId");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<int?>("PhotoUsersSocialUserId");

                    b.Property<int?>("PostsWithPhotoPostId");

                    b.HasKey("PhotoId");

                    b.HasIndex("MessageId");

                    b.HasIndex("PhotoUsersSocialUserId");

                    b.HasIndex("PostsWithPhotoPostId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("BeetAPI.Domain.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("LikesCounter");

                    b.Property<int?>("SocialUserId");

                    b.Property<string>("Text");

                    b.Property<int?>("UserGroupForPostGroupId");

                    b.HasKey("PostId");

                    b.HasIndex("SocialUserId");

                    b.HasIndex("UserGroupForPostGroupId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BeetAPI.Domain.SocialUser", b =>
                {
                    b.Property<int>("SocialUserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AboutMe");

                    b.Property<int?>("AvatarPhotoId");

                    b.Property<DateTime?>("Birthday");

                    b.Property<string>("City");

                    b.Property<string>("Email");

                    b.Property<string>("Firstname");

                    b.Property<string>("Gender");

                    b.Property<int?>("GroupId");

                    b.Property<string>("Lastname");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("SocialUserId");

                    b.HasIndex("GroupId");

                    b.ToTable("SocialUsers");
                });

            modelBuilder.Entity("BeetAPI.Domain.Comment", b =>
                {
                    b.HasOne("BeetAPI.Domain.Post")
                        .WithMany("AttachedComments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BeetAPI.Domain.SocialUser")
                        .WithMany("Comments")
                        .HasForeignKey("SocialUserId");
                });

            modelBuilder.Entity("BeetAPI.Domain.FriendRelation", b =>
                {
                    b.HasOne("BeetAPI.Domain.SocialUser", "UserAdded")
                        .WithMany()
                        .HasForeignKey("UserAddedSocialUserId");

                    b.HasOne("BeetAPI.Domain.SocialUser", "UserAdding")
                        .WithMany()
                        .HasForeignKey("UserAddingSocialUserId");
                });

            modelBuilder.Entity("BeetAPI.Domain.Group", b =>
                {
                    b.HasOne("BeetAPI.Domain.Photo", "Cover")
                        .WithMany()
                        .HasForeignKey("CoverPhotoId");
                });

            modelBuilder.Entity("BeetAPI.Domain.GroupRelation", b =>
                {
                    b.HasOne("BeetAPI.Domain.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BeetAPI.Domain.SocialUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeetAPI.Domain.LikeComment", b =>
                {
                    b.HasOne("BeetAPI.Domain.Comment", "Comment")
                        .WithMany("LikeComment")
                        .HasForeignKey("CommentId");

                    b.HasOne("BeetAPI.Domain.SocialUser", "User")
                        .WithMany("LikesComments")
                        .HasForeignKey("UserSocialUserId");
                });

            modelBuilder.Entity("BeetAPI.Domain.LikePost", b =>
                {
                    b.HasOne("BeetAPI.Domain.Post")
                        .WithMany("LikePost")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BeetAPI.Domain.SocialUser")
                        .WithMany("LikesPosts")
                        .HasForeignKey("SocialUserId");
                });

            modelBuilder.Entity("BeetAPI.Domain.Message", b =>
                {
                    b.HasOne("BeetAPI.Domain.SocialUser", "Author")
                        .WithMany("MessageAuthor")
                        .HasForeignKey("AuthorSocialUserId");

                    b.HasOne("BeetAPI.Domain.SocialUser", "Receiver")
                        .WithMany("MessageReceiver")
                        .HasForeignKey("ReceiverSocialUserId");
                });

            modelBuilder.Entity("BeetAPI.Domain.Photo", b =>
                {
                    b.HasOne("BeetAPI.Domain.Message", "Message")
                        .WithMany("AttachedPhoto")
                        .HasForeignKey("MessageId");

                    b.HasOne("BeetAPI.Domain.SocialUser", "PhotoUsers")
                        .WithMany("UserPhotos")
                        .HasForeignKey("PhotoUsersSocialUserId");

                    b.HasOne("BeetAPI.Domain.Post", "PostsWithPhoto")
                        .WithMany("AttachedPhotos")
                        .HasForeignKey("PostsWithPhotoPostId");
                });

            modelBuilder.Entity("BeetAPI.Domain.Post", b =>
                {
                    b.HasOne("BeetAPI.Domain.SocialUser")
                        .WithMany("UserPosts")
                        .HasForeignKey("SocialUserId");

                    b.HasOne("BeetAPI.Domain.Group", "UserGroupForPost")
                        .WithMany("Posts")
                        .HasForeignKey("UserGroupForPostGroupId");
                });

            modelBuilder.Entity("BeetAPI.Domain.SocialUser", b =>
                {
                    b.HasOne("BeetAPI.Domain.Group")
                        .WithMany("UsersForGroup")
                        .HasForeignKey("GroupId");
                });
#pragma warning restore 612, 618
        }
    }
}
