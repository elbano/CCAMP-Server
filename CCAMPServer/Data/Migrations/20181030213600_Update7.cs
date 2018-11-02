using Microsoft.EntityFrameworkCore.Migrations;

namespace CCAMPServer.Data.Migrations
{
    public partial class Update7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Deal");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Deal",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Deal");

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "Deal",
                nullable: false,
                defaultValue: 0);
        }
    }
}
