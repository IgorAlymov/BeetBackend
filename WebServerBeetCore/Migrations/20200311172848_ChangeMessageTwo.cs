using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServerBeetCore.Migrations
{
    public partial class ChangeMessageTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "text",
                table: "Messages",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "reciver",
                table: "Messages",
                newName: "Reciver");

            migrationBuilder.RenameColumn(
                name: "author",
                table: "Messages",
                newName: "Author");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Messages",
                newName: "text");

            migrationBuilder.RenameColumn(
                name: "Reciver",
                table: "Messages",
                newName: "reciver");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Messages",
                newName: "author");
        }
    }
}
