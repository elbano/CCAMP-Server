using Microsoft.EntityFrameworkCore.Migrations;

namespace CCAMPServer.Data.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deal_Campaign_campaignId",
                table: "Deal");

            migrationBuilder.DropForeignKey(
                name: "FK_Deal_ContentCreator_contentCreatorId",
                table: "Deal");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Advertisement_AdvertisementId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Content_ContentId",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "contentCreatorId",
                table: "Deal",
                newName: "ContentCreatorId");

            migrationBuilder.RenameColumn(
                name: "campaignId",
                table: "Deal",
                newName: "CampaignId");

            migrationBuilder.RenameIndex(
                name: "IX_Deal_contentCreatorId",
                table: "Deal",
                newName: "IX_Deal_ContentCreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Deal_campaignId",
                table: "Deal",
                newName: "IX_Deal_CampaignId");

            migrationBuilder.AlterColumn<int>(
                name: "ContentId",
                table: "Transaction",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementId",
                table: "Transaction",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ContentCreatorId",
                table: "Deal",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CampaignId",
                table: "Deal",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_Campaign_CampaignId",
                table: "Deal",
                column: "CampaignId",
                principalTable: "Campaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_ContentCreator_ContentCreatorId",
                table: "Deal",
                column: "ContentCreatorId",
                principalTable: "ContentCreator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Advertisement_AdvertisementId",
                table: "Transaction",
                column: "AdvertisementId",
                principalTable: "Advertisement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Content_ContentId",
                table: "Transaction",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deal_Campaign_CampaignId",
                table: "Deal");

            migrationBuilder.DropForeignKey(
                name: "FK_Deal_ContentCreator_ContentCreatorId",
                table: "Deal");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Advertisement_AdvertisementId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Content_ContentId",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "ContentCreatorId",
                table: "Deal",
                newName: "contentCreatorId");

            migrationBuilder.RenameColumn(
                name: "CampaignId",
                table: "Deal",
                newName: "campaignId");

            migrationBuilder.RenameIndex(
                name: "IX_Deal_ContentCreatorId",
                table: "Deal",
                newName: "IX_Deal_contentCreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Deal_CampaignId",
                table: "Deal",
                newName: "IX_Deal_campaignId");

            migrationBuilder.AlterColumn<int>(
                name: "ContentId",
                table: "Transaction",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementId",
                table: "Transaction",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "contentCreatorId",
                table: "Deal",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "campaignId",
                table: "Deal",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Advertisement_AdvertisementId",
                table: "Transaction",
                column: "AdvertisementId",
                principalTable: "Advertisement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Content_ContentId",
                table: "Transaction",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
