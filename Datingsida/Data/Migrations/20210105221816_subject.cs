using Microsoft.EntityFrameworkCore.Migrations;

namespace Datingsida.Migrations.DatingDb
{
    public partial class subject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subjekt",
                table: "Messages",
                newName: "Subject");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Messages",
                newName: "Subjekt");
        }
    }
}
