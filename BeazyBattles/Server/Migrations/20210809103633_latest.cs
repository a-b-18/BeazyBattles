using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeazyBattles.Server.Migrations
{
    public partial class latest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    HitPoints = table.Column<int>(type: "int", nullable: false),
                    BananaCost = table.Column<int>(type: "int", nullable: false),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Title", "Attack", "Defense", "HitPoints", "BananaCost", "IconPath" },
                values: new[]
                {
                    "1",
                    "Knight" ,
                    "10" ,
                    "20" ,
                    "100" ,
                    "100" ,
                    "icons/knight.png" ,
                });
            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Title", "Attack", "Defense", "HitPoints", "BananaCost", "IconPath" },
                values: new[]
                {
                        "2",
                        "Archer" ,
                        "15" ,
                        "10" ,
                        "100" ,
                        "150" ,
                        "icons/archer.png" ,
                });
            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Title", "Attack", "Defense", "HitPoints", "BananaCost", "IconPath" },
                values: new[]
                {
                    "3",
                    "Mage" ,
                    "20" ,
                    "5" ,
                    "100" ,
                    "200" ,
                    "icons/mage.png" ,
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Bananas = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
