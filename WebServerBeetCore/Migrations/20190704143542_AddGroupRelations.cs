 using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServerBeetCore.Migrations
{
    public partial class AddGroupRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupRelations",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRelations", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GroupRelations_UserGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "UserGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupRelations_SocialUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SocialUsers",
                        principalColumn: "SocialUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupRelations_GroupId",
                table: "GroupRelations",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupRelations");
        }
    }
}
