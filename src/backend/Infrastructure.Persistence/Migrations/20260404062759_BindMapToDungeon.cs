using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BindMapToDungeon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_dungeons_map_id",
                table: "dungeons",
                column: "map_id");

            migrationBuilder.AddForeignKey(
                name: "fk_dungeons_dungeon_maps_map_id",
                table: "dungeons",
                column: "map_id",
                principalTable: "dungeon_maps",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_dungeons_dungeon_maps_map_id",
                table: "dungeons");

            migrationBuilder.DropIndex(
                name: "ix_dungeons_map_id",
                table: "dungeons");
        }
    }
}
