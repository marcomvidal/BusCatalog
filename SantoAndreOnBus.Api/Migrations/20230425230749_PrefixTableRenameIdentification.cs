using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SantoAndreOnBus.Api.Migrations
{
    public partial class PrefixTableRenameIdentification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Prefixes",
                newName: "Identification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Identification",
                table: "Prefixes",
                newName: "Number");
        }
    }
}
