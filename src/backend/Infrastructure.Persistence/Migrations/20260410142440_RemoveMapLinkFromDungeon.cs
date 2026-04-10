using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMapLinkFromDungeon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_dungeons_dungeon_maps_map_id",
                table: "dungeons");

            migrationBuilder.DropIndex(
                name: "ix_dungeons_map_id",
                table: "dungeons");

            migrationBuilder.DropColumn(
                name: "map_id",
                table: "dungeons");

            migrationBuilder.AddColumn<string>(
                name: "map",
                table: "dungeons",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "dungeon_maps",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_dungeons_map",
                table: "dungeons",
                column: "map");

            migrationBuilder.CreateIndex(
                name: "ix_dungeon_maps_name",
                table: "dungeon_maps",
                column: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_dungeons_map",
                table: "dungeons");

            migrationBuilder.DropIndex(
                name: "ix_dungeon_maps_name",
                table: "dungeon_maps");

            migrationBuilder.DropColumn(
                name: "map",
                table: "dungeons");

            migrationBuilder.DropColumn(
                name: "name",
                table: "dungeon_maps");

            migrationBuilder.AddColumn<int>(
                name: "map_id",
                table: "dungeons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
    }
}
