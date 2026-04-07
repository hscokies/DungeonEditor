using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDungeonMapEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dungeon_maps",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    area = table.Column<byte>(type: "smallint", nullable: false),
                    layout_seed = table.Column<byte>(type: "smallint", nullable: false),
                    instance = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dungeon_maps", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_dungeon_maps_area_layout_seed_instance",
                table: "dungeon_maps",
                columns: new[] { "area", "layout_seed", "instance" });
            
            migrationBuilder.AddColumn<int>(
                name: "map_id",
                table: "dungeons",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            
            
            migrationBuilder.DropColumn(
                name: "map",
                table: "dungeons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dungeon_maps");

            // Skip dungeon map restore
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
        }
    }
}
