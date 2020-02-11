using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServerBeetCore.Migrations
{
    public partial class ChangeComment2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_SocialUsers_SocialUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LikesCounter",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "SocialUserId",
                table: "Comments",
                newName: "AuthorSocialUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_SocialUserId",
                table: "Comments",
                newName: "IX_Comments_AuthorSocialUserId");

            migrationBuilder.AddColumn<string>(
                name: "AvatarAuthor",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_SocialUsers_AuthorSocialUserId",
                table: "Comments",
                column: "AuthorSocialUserId",
                principalTable: "SocialUsers",
                principalColumn: "SocialUserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_SocialUsers_AuthorSocialUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AvatarAuthor",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "AuthorSocialUserId",
                table: "Comments",
                newName: "SocialUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AuthorSocialUserId",
                table: "Comments",
                newName: "IX_Comments_SocialUserId");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LikesCounter",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_SocialUsers_SocialUserId",
                table: "Comments",
                column: "SocialUserId",
                principalTable: "SocialUsers",
                principalColumn: "SocialUserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
