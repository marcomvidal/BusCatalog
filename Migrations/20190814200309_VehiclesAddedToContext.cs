using Microsoft.EntityFrameworkCore.Migrations;

namespace SantoAndreOnBus.Migrations
{
    public partial class VehiclesAddedToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineVehicle_Lines_LineId",
                table: "LineVehicle");

            migrationBuilder.DropForeignKey(
                name: "FK_LineVehicle_Vehicle_VehicleId",
                table: "LineVehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LineVehicle",
                table: "LineVehicle");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Vehicles");

            migrationBuilder.RenameTable(
                name: "LineVehicle",
                newName: "LineVehicles");

            migrationBuilder.RenameIndex(
                name: "IX_LineVehicle_VehicleId",
                table: "LineVehicles",
                newName: "IX_LineVehicles_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_LineVehicle_LineId",
                table: "LineVehicles",
                newName: "IX_LineVehicles_LineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LineVehicles",
                table: "LineVehicles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LineVehicles_Lines_LineId",
                table: "LineVehicles",
                column: "LineId",
                principalTable: "Lines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LineVehicles_Vehicles_VehicleId",
                table: "LineVehicles",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineVehicles_Lines_LineId",
                table: "LineVehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_LineVehicles_Vehicles_VehicleId",
                table: "LineVehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LineVehicles",
                table: "LineVehicles");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehicle");

            migrationBuilder.RenameTable(
                name: "LineVehicles",
                newName: "LineVehicle");

            migrationBuilder.RenameIndex(
                name: "IX_LineVehicles_VehicleId",
                table: "LineVehicle",
                newName: "IX_LineVehicle_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_LineVehicles_LineId",
                table: "LineVehicle",
                newName: "IX_LineVehicle_LineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LineVehicle",
                table: "LineVehicle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LineVehicle_Lines_LineId",
                table: "LineVehicle",
                column: "LineId",
                principalTable: "Lines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LineVehicle_Vehicle_VehicleId",
                table: "LineVehicle",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
