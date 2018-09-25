using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CCAMPServer.Data.Migrations
{
    public partial class Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_ContentCreator_ContentCreatorId",
                table: "Content");

            migrationBuilder.DropForeignKey(
                name: "FK_Deal_ContentCreator_ContentCreatorId",
                table: "Deal");

            migrationBuilder.RenameColumn(
                name: "ContentCreatorId",
                table: "Deal",
                newName: "ChannelId");

            migrationBuilder.RenameIndex(
                name: "IX_Deal_ContentCreatorId",
                table: "Deal",
                newName: "IX_Deal_ChannelId");

            migrationBuilder.RenameColumn(
                name: "ContentCreatorId",
                table: "Content",
                newName: "ChannelId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_ContentCreatorId",
                table: "Content",
                newName: "IX_Content_ChannelId");

            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ContentCreatorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Channel_ContentCreator_ContentCreatorId",
                        column: x => x.ContentCreatorId,
                        principalTable: "ContentCreator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channel_ContentCreatorId",
                table: "Channel",
                column: "ContentCreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Channel_ChannelId",
                table: "Content",
                column: "ChannelId",
                principalTable: "Channel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_Channel_ChannelId",
                table: "Deal",
                column: "ChannelId",
                principalTable: "Channel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Channel_ChannelId",
                table: "Content");

            migrationBuilder.DropForeignKey(
                name: "FK_Deal_Channel_ChannelId",
                table: "Deal");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.RenameColumn(
                name: "ChannelId",
                table: "Deal",
                newName: "ContentCreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Deal_ChannelId",
                table: "Deal",
                newName: "IX_Deal_ContentCreatorId");

            migrationBuilder.RenameColumn(
                name: "ChannelId",
                table: "Content",
                newName: "ContentCreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_ChannelId",
                table: "Content",
                newName: "IX_Content_ContentCreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_ContentCreator_ContentCreatorId",
                table: "Content",
                column: "ContentCreatorId",
                principalTable: "ContentCreator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_ContentCreator_ContentCreatorId",
                table: "Deal",
                column: "ContentCreatorId",
                principalTable: "ContentCreator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
