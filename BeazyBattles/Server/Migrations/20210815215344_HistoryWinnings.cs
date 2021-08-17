using Microsoft.EntityFrameworkCore.Migrations;

namespace BeazyBattles.Server.Migrations
{
    public partial class HistoryWinnings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttackerWinnings",
                table: "Battles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OpponentWinnings",
                table: "Battles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttackerWinnings",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "OpponentWinnings",
                table: "Battles");
        }
    }
}
