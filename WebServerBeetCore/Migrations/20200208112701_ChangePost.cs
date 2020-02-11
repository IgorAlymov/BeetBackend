using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServerBeetCore.Migrations
{
    public partial class ChangePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_SocialUsers_AuthorSocialUserId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "AuthorSocialUserId",
                table: "Posts",
                newName: "SocialUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_AuthorSocialUserId",
                table: "Posts",
                newName: "IX_Posts_SocialUserId");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_SocialUsers_SocialUserId",
                table: "Posts",
                column: "SocialUserId",
                principalTable: "SocialUsers",
                principalColumn: "SocialUserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_SocialUsers_SocialUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "SocialUserId",
                table: "Posts",
                newName: "AuthorSocialUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_SocialUserId",
                table: "Posts",
                newName: "IX_Posts_AuthorSocialUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_SocialUsers_AuthorSocialUserId",
                table: "Posts",
                column: "AuthorSocialUserId",
                principalTable: "SocialUsers",
                principalColumn: "SocialUserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
