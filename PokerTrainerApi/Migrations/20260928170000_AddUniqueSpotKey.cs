using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokerTrainerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueSpotKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SpotKey",
                table: "PokerRanges",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PokerRanges_SpotKey",
                table: "PokerRanges",
                column: "SpotKey",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PokerRanges_SpotKey",
                table: "PokerRanges");

            migrationBuilder.AlterColumn<string>(
                name: "SpotKey",
                table: "PokerRanges",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}