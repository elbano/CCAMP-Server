using Microsoft.EntityFrameworkCore.Migrations;

namespace CCAMPServer.Data.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "SupportUser",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Sponsor",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "campaignId",
                table: "Deal",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "contentCreatorId",
                table: "Deal",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "ContentCreator",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Deal_campaignId",
                table: "Deal",
                column: "campaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Deal_contentCreatorId",
                table: "Deal",
                column: "contentCreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_Campaign_campaignId",
                table: "Deal",
                column: "campaignId",
                principalTable: "Campaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_ContentCreator_contentCreatorId",
                table: "Deal",
                column: "contentCreatorId",
                principalTable: "ContentCreator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deal_Campaign_campaignId",
                table: "Deal");

            migrationBuilder.DropForeignKey(
                name: "FK_Deal_ContentCreator_contentCreatorId",
                table: "Deal");

            migrationBuilder.DropIndex(
                name: "IX_Deal_campaignId",
                table: "Deal");

            migrationBuilder.DropIndex(
                name: "IX_Deal_contentCreatorId",
                table: "Deal");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SupportUser");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sponsor");

            migrationBuilder.DropColumn(
                name: "campaignId",
                table: "Deal");

            migrationBuilder.DropColumn(
                name: "contentCreatorId",
                table: "Deal");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ContentCreator");
        }
    }
}
