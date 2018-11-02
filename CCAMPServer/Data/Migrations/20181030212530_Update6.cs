using Microsoft.EntityFrameworkCore.Migrations;

namespace CCAMPServer.Data.Migrations
{
    public partial class Update6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Deal",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Modality",
                table: "Deal",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Deal");

            migrationBuilder.DropColumn(
                name: "Modality",
                table: "Deal");
        }
    }
}
