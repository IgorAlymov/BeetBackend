using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServerBeetCore.Migrations
{
    public partial class ChangeLikePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikePosts_Posts_PostId",
                table: "LikePosts");

            migrationBuilder.DropForeignKey(
                name: "FK_LikePosts_SocialUsers_UserSocialUserId",
                table: "LikePosts");

            migrationBuilder.RenameColumn(
                name: "UserSocialUserId",
                table: "LikePosts",
                newName: "SocialUserId");

            migrationBuilder.RenameIndex(
                name: "IX_LikePosts_UserSocialUserId",
                table: "LikePosts",
                newName: "IX_LikePosts_SocialUserId");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "LikePosts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "LikePosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_LikePosts_Posts_PostId",
                table: "LikePosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikePosts_SocialUsers_SocialUserId",
                table: "LikePosts",
                column: "SocialUserId",
                principalTable: "SocialUsers",
                principalColumn: "SocialUserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikePosts_Posts_PostId",
                table: "LikePosts");

            migrationBuilder.DropForeignKey(
                name: "FK_LikePosts_SocialUsers_SocialUserId",
                table: "LikePosts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LikePosts");

            migrationBuilder.RenameColumn(
                name: "SocialUserId",
                table: "LikePosts",
                newName: "UserSocialUserId");

            migrationBuilder.RenameIndex(
                name: "IX_LikePosts_SocialUserId",
                table: "LikePosts",
                newName: "IX_LikePosts_UserSocialUserId");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "LikePosts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_LikePosts_Posts_PostId",
                table: "LikePosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LikePosts_SocialUsers_UserSocialUserId",
                table: "LikePosts",
                column: "UserSocialUserId",
                principalTable: "SocialUsers",
                principalColumn: "SocialUserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
