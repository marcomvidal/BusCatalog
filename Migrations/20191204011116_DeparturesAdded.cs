using Microsoft.EntityFrameworkCore.Migrations;

namespace SantoAndreOnBus.Migrations
{
    public partial class DeparturesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Departures",
                table: "Lines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Departures",
                table: "Lines");
        }
    }
}
