using Microsoft.EntityFrameworkCore.Migrations;

namespace SantoAndreOnBus.Migrations
{
    public partial class RemovalOfUnnecessaryNamePropertyAtLineVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "LineVehicles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LineVehicles",
                nullable: true);
        }
    }
}
