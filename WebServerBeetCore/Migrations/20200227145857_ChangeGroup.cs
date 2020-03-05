using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServerBeetCore.Migrations
{
    public partial class ChangeGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "UserGroups",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "UserGroups");
        }
    }
}
