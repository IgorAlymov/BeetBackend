using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServerBeetCore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikeComments",
                columns: table => new
                {
                    LikeCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserSocialUserId = table.Column<int>(nullable: true),
                    CommentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeComments", x => x.LikeCommentId);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    PhotoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    PhotoUsersSocialUserId = table.Column<int>(nullable: true),
                    PostsWithPhotoPostId = table.Column<int>(nullable: true),
                    MessageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.PhotoId);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CoverPhotoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_UserGroups_Photos_CoverPhotoId",
                        column: x => x.CoverPhotoId,
                        principalTable: "Photos",
                        principalColumn: "PhotoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocialUsers",
                columns: table => new
                {
                    SocialUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    AvatarPhotoId = table.Column<int>(nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    AboutMe = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    GroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialUsers", x => x.SocialUserId);
                    table.ForeignKey(
                        name: "FK_SocialUsers_UserGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "UserGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FriendRelations",
                columns: table => new
                {
                    UserIdAdding = table.Column<int>(nullable: false),
                    UserIdAdded = table.Column<int>(nullable: false),
                    UserAddingSocialUserId = table.Column<int>(nullable: true),
                    UserAddedSocialUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRelations", x => new { x.UserIdAdding, x.UserIdAdded });
                    table.ForeignKey(
                        name: "FK_FriendRelations_SocialUsers_UserAddedSocialUserId",
                        column: x => x.UserAddedSocialUserId,
                        principalTable: "SocialUsers",
                        principalColumn: "SocialUserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FriendRelations_SocialUsers_UserAddingSocialUserId",
                        column: x => x.UserAddingSocialUserId,
                        principalTable: "SocialUsers",
                        principalColumn: "SocialUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    WasReaded = table.Column<bool>(nullable: false),
                    AuthorSocialUserId = table.Column<int>(nullable: true),
                    ReceiverSocialUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_SocialUsers_AuthorSocialUserId",
                        column: x => x.AuthorSocialUserId,
                        principalTable: "SocialUsers",
                        principalColumn: "SocialUserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_SocialUsers_ReceiverSocialUserId",
                        column: x => x.ReceiverSocialUserId,
                        principalTable: "SocialUsers",
                        principalColumn: "SocialUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    LikesCounter = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    AuthorSocialUserId = table.Column<int>(nullable: true),
                    UserGroupForPostGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_SocialUsers_AuthorSocialUserId",
                        column: x => x.AuthorSocialUserId,
                        principalTable: "SocialUsers",
                        principalColumn: "SocialUserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_UserGroups_UserGroupForPostGroupId",
                        column: x => x.UserGroupForPostGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    LikesCounter = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<int>(nullable: true),
                    AuthorSocialUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_SocialUsers_AuthorSocialUserId",
                        column: x => x.AuthorSocialUserId,
                        principalTable: "SocialUsers",
                        principalColumn: "SocialUserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LikePosts",
                columns: table => new
                {
                    LikePostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserSocialUserId = table.Column<int>(nullable: true),
                    PostId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikePosts", x => x.LikePostId);
                    table.ForeignKey(
                        name: "FK_LikePosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LikePosts_SocialUsers_UserSocialUserId",
                        column: x => x.UserSocialUserId,
                        principalTable: "SocialUsers",
                        principalColumn: "SocialUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorSocialUserId",
                table: "Comments",
                column: "AuthorSocialUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRelations_UserAddedSocialUserId",
                table: "FriendRelations",
                column: "UserAddedSocialUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRelations_UserAddingSocialUserId",
                table: "FriendRelations",
                column: "UserAddingSocialUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeComments_CommentId",
                table: "LikeComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeComments_UserSocialUserId",
                table: "LikeComments",
                column: "UserSocialUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikePosts_PostId",
                table: "LikePosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_LikePosts_UserSocialUserId",
                table: "LikePosts",
                column: "UserSocialUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AuthorSocialUserId",
                table: "Messages",
                column: "AuthorSocialUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverSocialUserId",
                table: "Messages",
                column: "ReceiverSocialUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_MessageId",
                table: "Photos",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PhotoUsersSocialUserId",
                table: "Photos",
                column: "PhotoUsersSocialUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PostsWithPhotoPostId",
                table: "Photos",
                column: "PostsWithPhotoPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorSocialUserId",
                table: "Posts",
                column: "AuthorSocialUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserGroupForPostGroupId",
                table: "Posts",
                column: "UserGroupForPostGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialUsers_GroupId",
                table: "SocialUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_CoverPhotoId",
                table: "UserGroups",
                column: "CoverPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeComments_SocialUsers_UserSocialUserId",
                table: "LikeComments",
                column: "UserSocialUserId",
                principalTable: "SocialUsers",
                principalColumn: "SocialUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LikeComments_Comments_CommentId",
                table: "LikeComments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_SocialUsers_PhotoUsersSocialUserId",
                table: "Photos",
                column: "PhotoUsersSocialUserId",
                principalTable: "SocialUsers",
                principalColumn: "SocialUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Posts_PostsWithPhotoPostId",
                table: "Photos",
                column: "PostsWithPhotoPostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Messages_MessageId",
                table: "Photos",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_SocialUsers_AuthorSocialUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_SocialUsers_ReceiverSocialUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_SocialUsers_PhotoUsersSocialUserId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_SocialUsers_AuthorSocialUserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Posts_PostsWithPhotoPostId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "FriendRelations");

            migrationBuilder.DropTable(
                name: "LikeComments");

            migrationBuilder.DropTable(
                name: "LikePosts");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "SocialUsers");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
