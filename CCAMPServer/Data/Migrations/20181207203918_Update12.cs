using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CCAMPServer.Data.Migrations
{
    public partial class Update12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from deal; delete from campaign; delete from channel;" +
                " delete from sponsor; delete from contentcreator;");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaign_Sponsor_SponsorId",
                table: "Campaign");

            migrationBuilder.DropForeignKey(
                name: "FK_Channel_ContentCreator_ContentCreatorId",
                table: "Channel");

            migrationBuilder.DropTable(
                name: "ContentCreator");

            migrationBuilder.DropTable(
                name: "Sponsor");

            migrationBuilder.DropTable(
                name: "SupportUser");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "TokenRequest");

            migrationBuilder.RenameColumn(
                name: "ContentCreatorId",
                table: "Channel",
                newName: "ContentCreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Channel_ContentCreatorId",
                table: "Channel",
                newName: "IX_Channel_ContentCreatorUserId");

            migrationBuilder.RenameColumn(
                name: "SponsorId",
                table: "Campaign",
                newName: "SponsorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Campaign_SponsorId",
                table: "Campaign",
                newName: "IX_Campaign_SponsorUserId");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    AuthUserId = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Campaign_User_SponsorUserId",
                table: "Campaign",
                column: "SponsorUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Channel_User_ContentCreatorUserId",
                table: "Channel",
                column: "ContentCreatorUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaign_User_SponsorUserId",
                table: "Campaign");

            migrationBuilder.DropForeignKey(
                name: "FK_Channel_User_ContentCreatorUserId",
                table: "Channel");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.RenameColumn(
                name: "ContentCreatorUserId",
                table: "Channel",
                newName: "ContentCreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Channel_ContentCreatorUserId",
                table: "Channel",
                newName: "IX_Channel_ContentCreatorId");

            migrationBuilder.RenameColumn(
                name: "SponsorUserId",
                table: "Campaign",
                newName: "SponsorId");

            migrationBuilder.RenameIndex(
                name: "IX_Campaign_SponsorUserId",
                table: "Campaign",
                newName: "IX_Campaign_SponsorId");

            migrationBuilder.CreateTable(
                name: "ContentCreator",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthUserId = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentCreator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sponsor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthUserId = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupportUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthUserId = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessToken = table.Column<string>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TokenRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    RequestDate = table.Column<string>(nullable: false),
                    ResponseUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenRequest", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Campaign_Sponsor_SponsorId",
                table: "Campaign",
                column: "SponsorId",
                principalTable: "Sponsor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Channel_ContentCreator_ContentCreatorId",
                table: "Channel",
                column: "ContentCreatorId",
                principalTable: "ContentCreator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
