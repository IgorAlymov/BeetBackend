using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServerBeetCore.Migrations
{
    public partial class AddMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_SocialUsers_AuthorSocialUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_SocialUsers_ReceiverSocialUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "WasReaded",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Messages",
                newName: "text");

            migrationBuilder.RenameColumn(
                name: "ReceiverSocialUserId",
                table: "Messages",
                newName: "SocialUserId1");

            migrationBuilder.RenameColumn(
                name: "AuthorSocialUserId",
                table: "Messages",
                newName: "SocialUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ReceiverSocialUserId",
                table: "Messages",
                newName: "IX_Messages_SocialUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_AuthorSocialUserId",
                table: "Messages",
                newName: "IX_Messages_SocialUserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DialogId",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "author",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "reciver",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Dialogs",
                columns: table => new
                {
                    DialogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Avatar = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LastMessage = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Author = table.Column<int>(nullable: false),
                    Reciver = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dialogs", x => x.DialogId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_DialogId",
                table: "Messages",
                column: "DialogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Dialogs_DialogId",
                table: "Messages",
                column: "DialogId",
                principalTable: "Dialogs",
                principalColumn: "DialogId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_SocialUsers_SocialUserId",
                table: "Messages",
                column: "SocialUserId",
                principalTable: "SocialUsers",
                principalColumn: "SocialUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_SocialUsers_SocialUserId1",
                table: "Messages",
                column: "SocialUserId1",
                principalTable: "SocialUsers",
                principalColumn: "SocialUserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Dialogs_DialogId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_SocialUsers_SocialUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_SocialUsers_SocialUserId1",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Dialogs");

            migrationBuilder.DropIndex(
                name: "IX_Messages_DialogId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "DialogId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "author",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "reciver",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "text",
                table: "Messages",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "SocialUserId1",
                table: "Messages",
                newName: "ReceiverSocialUserId");

            migrationBuilder.RenameColumn(
                name: "SocialUserId",
                table: "Messages",
                newName: "AuthorSocialUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SocialUserId1",
                table: "Messages",
                newName: "IX_Messages_ReceiverSocialUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SocialUserId",
                table: "Messages",
                newName: "IX_Messages_AuthorSocialUserId");

            migrationBuilder.AddColumn<bool>(
                name: "WasReaded",
                table: "Messages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_SocialUsers_AuthorSocialUserId",
                table: "Messages",
                column: "AuthorSocialUserId",
                principalTable: "SocialUsers",
                principalColumn: "SocialUserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_SocialUsers_ReceiverSocialUserId",
                table: "Messages",
                column: "ReceiverSocialUserId",
                principalTable: "SocialUsers",
                principalColumn: "SocialUserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
