using Microsoft.EntityFrameworkCore.Migrations;

namespace SantoAndreOnBus.Migrations
{
    public partial class RenamingBackwardToTowardsOnLineModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Backwards",
                table: "Lines",
                newName: "Towards");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Towards",
                table: "Lines",
                newName: "Backwards");
        }
    }
}
