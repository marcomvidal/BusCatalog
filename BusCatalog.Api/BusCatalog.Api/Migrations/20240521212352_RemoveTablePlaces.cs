using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusCatalog.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTablePlaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinePlace");

            migrationBuilder.DropTable(
                name: "Places");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Identification = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinePlace",
                columns: table => new
                {
                    LinesId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlacesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinePlace", x => new { x.LinesId, x.PlacesId });
                    table.ForeignKey(
                        name: "FK_LinePlace_Lines_LinesId",
                        column: x => x.LinesId,
                        principalTable: "Lines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinePlace_Places_PlacesId",
                        column: x => x.PlacesId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinePlace_PlacesId",
                table: "LinePlace",
                column: "PlacesId");
        }
    }
}
