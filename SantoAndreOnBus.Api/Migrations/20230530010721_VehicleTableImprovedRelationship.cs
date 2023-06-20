using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SantoAndreOnBus.Api.Migrations
{
    public partial class VehicleTableImprovedRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Vehicles",
                newName: "Type");

            migrationBuilder.CreateTable(
                name: "LineVehicle",
                columns: table => new
                {
                    LinesId = table.Column<int>(type: "INTEGER", nullable: false),
                    VehiclesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineVehicle", x => new { x.LinesId, x.VehiclesId });
                    table.ForeignKey(
                        name: "FK_LineVehicle_Lines_LinesId",
                        column: x => x.LinesId,
                        principalTable: "Lines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineVehicle_Vehicles_VehiclesId",
                        column: x => x.VehiclesId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineVehicle_VehiclesId",
                table: "LineVehicle",
                column: "VehiclesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineVehicle");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Vehicles",
                newName: "Name");
        }
    }
}
