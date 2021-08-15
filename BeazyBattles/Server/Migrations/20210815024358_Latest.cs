using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeazyBattles.Server.Migrations
{
    public partial class Latest : Migration
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
                    "Dog" ,
                    "100" ,
                    "1" ,
                    "5" ,
                    "10" ,
                    "icons/dog.png" ,
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Title", "Attack", "Defense", "HitPoints", "BananaCost", "IconPath" },
                values: new[]
                {
                    "2",
                    "Honey Badger" ,
                    "100" ,
                    "1" ,
                    "5" ,
                    "10" ,
                    "icons/honey_badger.png" ,
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Title", "Attack", "Defense", "HitPoints", "BananaCost", "IconPath" },
                values: new[]
                {
                    "3",
                    "Monkey" ,
                    "100" ,
                    "1" ,
                    "5" ,
                    "10" ,
                    "icons/monkey.png" ,
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Title", "Attack", "Defense", "HitPoints", "BananaCost", "IconPath" },
                values: new[]
                {
                    "4",
                    "Knight" ,
                    "10" ,
                    "20" ,
                    "50" ,
                    "100" ,
                    "icons/knight.png" ,
            });
            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Title", "Attack", "Defense", "HitPoints", "BananaCost", "IconPath" },
                values: new[]
                {
                    "5",
                    "Archer" ,
                    "15" ,
                    "10" ,
                    "100" ,
                    "1000" ,
                    "icons/archer.png" ,
            });
            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Title", "Attack", "Defense", "HitPoints", "BananaCost", "IconPath" },
                values: new[]
                {
                    "6",
                    "Mage" ,
                    "50" ,
                    "5" ,
                    "100" ,
                    "2000" ,
                    "icons/mage.png" ,
            });
            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Title", "Attack", "Defense", "HitPoints", "BananaCost", "IconPath" },
                values: new[]
                {
                    "7",
                    "Barbarian" ,
                    "30" ,
                    "20" ,
                    "150" ,
                    "5000" ,
                    "icons/barbarian.png" ,
                });
            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Title", "Attack", "Defense", "HitPoints", "BananaCost", "IconPath" },
                values: new[]
                {
                    "8",
                    "Ogre" ,
                    "50" ,
                    "0" ,
                    "500" ,
                    "100000" ,
                    "icons/ogre.png" ,
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Title", "Attack", "Defense", "HitPoints", "BananaCost", "IconPath" },
                values: new[]
                {
                    "9",
                    "Ninja" ,
                    "200" ,
                    "70" ,
                    "350" ,
                    "150000" ,
                    "icons/ninja.png" ,
            });
            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Title", "Attack", "Defense", "HitPoints", "BananaCost", "IconPath" },
                values: new[]
                {
                    "10",
                    "Giant" ,
                    "100" ,
                    "50" ,
                    "1000" ,
                    "200000" ,
                    "icons/giant.png" ,
            });
            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Title", "Attack", "Defense", "HitPoints", "BananaCost", "IconPath" },
                values: new[]
                {
                    "11",
                    "Fallen Hero" ,
                    "350" ,
                    "100" ,
                    "3500" ,
                    "800000" ,
                    "icons/fallen_hero.png" ,
            });
            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Title", "Attack", "Defense", "HitPoints", "BananaCost", "IconPath" },
                values: new[]
                {
                    "12",
                    "Dragon" ,
                    "500" ,
                    "0" ,
                    "5000" ,
                    "1000000" ,
                    "icons/dragon.png" ,
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Battles = table.Column<int>(type: "int", nullable: false),
                    Victories = table.Column<int>(type: "int", nullable: false),
                    Defeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Battles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttackerId = table.Column<int>(type: "int", nullable: false),
                    OpponentId = table.Column<int>(type: "int", nullable: false),
                    WinnerId = table.Column<int>(type: "int", nullable: false),
                    WinnerDamage = table.Column<int>(type: "int", nullable: false),
                    RoundsFought = table.Column<int>(type: "int", nullable: false),
                    BattleDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Battles_Users_AttackerId",
                        column: x => x.AttackerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Battles_Users_OpponentId",
                        column: x => x.OpponentId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Battles_Users_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    HitPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUnits_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battles_AttackerId",
                table: "Battles",
                column: "AttackerId");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_OpponentId",
                table: "Battles",
                column: "OpponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_WinnerId",
                table: "Battles",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUnits_UnitId",
                table: "UserUnits",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUnits_UserId",
                table: "UserUnits",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Battles");

            migrationBuilder.DropTable(
                name: "UserUnits");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
