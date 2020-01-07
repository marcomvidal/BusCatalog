using Microsoft.EntityFrameworkCore.Migrations;

namespace SantoAndreOnBus.Migrations
{
    public partial class DeparturesToNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Departures",
                table: "Lines",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Departures",
                table: "Lines",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
