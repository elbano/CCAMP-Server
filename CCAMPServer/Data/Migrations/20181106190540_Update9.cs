using Microsoft.EntityFrameworkCore.Migrations;

namespace CCAMPServer.Data.Migrations
{
    public partial class Update9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthToken",
                table: "SupportUser",
                newName: "AuthUserId");

            migrationBuilder.RenameColumn(
                name: "AuthToken",
                table: "Sponsor",
                newName: "AuthUserId");

            migrationBuilder.RenameColumn(
                name: "AuthToken",
                table: "ContentCreator",
                newName: "AuthUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthUserId",
                table: "SupportUser",
                newName: "AuthToken");

            migrationBuilder.RenameColumn(
                name: "AuthUserId",
                table: "Sponsor",
                newName: "AuthToken");

            migrationBuilder.RenameColumn(
                name: "AuthUserId",
                table: "ContentCreator",
                newName: "AuthToken");
        }
    }
}
