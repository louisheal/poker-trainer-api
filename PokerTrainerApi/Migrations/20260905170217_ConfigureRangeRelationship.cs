using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokerTrainerApi.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureRangeRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RangeEntries_PokerRanges_PokerRangeDaoId",
                table: "RangeEntries");

            migrationBuilder.DropIndex(
                name: "IX_RangeEntries_PokerRangeDaoId",
                table: "RangeEntries");

            migrationBuilder.DropColumn(
                name: "PokerRangeDaoId",
                table: "RangeEntries");

            migrationBuilder.AddForeignKey(
                name: "FK_RangeEntries_PokerRanges_RangeId",
                table: "RangeEntries",
                column: "RangeId",
                principalTable: "PokerRanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RangeEntries_PokerRanges_RangeId",
                table: "RangeEntries");

            migrationBuilder.AddColumn<int>(
                name: "PokerRangeDaoId",
                table: "RangeEntries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RangeEntries_PokerRangeDaoId",
                table: "RangeEntries",
                column: "PokerRangeDaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_RangeEntries_PokerRanges_PokerRangeDaoId",
                table: "RangeEntries",
                column: "PokerRangeDaoId",
                principalTable: "PokerRanges",
                principalColumn: "Id");
        }
    }
}
