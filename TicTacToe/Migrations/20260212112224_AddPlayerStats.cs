using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicTacToe.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Draws",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Losses",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalGames",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wins",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Draws",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Losses",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TotalGames",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Wins",
                table: "Players");
        }
    }
}
