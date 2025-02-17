using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportiverseApi.Migrations
{
    public partial class AddUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_news",
                table: "news");

            migrationBuilder.RenameTable(
                name: "news",
                newName: "News");

            migrationBuilder.AddPrimaryKey(
                name: "PK_News",
                table: "News",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_News",
                table: "News");

            migrationBuilder.RenameTable(
                name: "News",
                newName: "news");

            migrationBuilder.AddPrimaryKey(
                name: "PK_news",
                table: "news",
                column: "Id");
        }
    }
}
