using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SantoAndreOnBus.Migrations
{
    public partial class PrefixesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrefixId",
                table: "Lines",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Prefixes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Number = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefixes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prefixes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lines_PrefixId",
                table: "Lines",
                column: "PrefixId");

            migrationBuilder.CreateIndex(
                name: "IX_Prefixes_CompanyId",
                table: "Prefixes",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Prefixes_PrefixId",
                table: "Lines",
                column: "PrefixId",
                principalTable: "Prefixes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Prefixes_PrefixId",
                table: "Lines");

            migrationBuilder.DropTable(
                name: "Prefixes");

            migrationBuilder.DropIndex(
                name: "IX_Lines_PrefixId",
                table: "Lines");

            migrationBuilder.DropColumn(
                name: "PrefixId",
                table: "Lines");
        }
    }
}
