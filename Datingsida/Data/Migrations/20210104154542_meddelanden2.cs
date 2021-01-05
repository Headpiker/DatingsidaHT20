using Microsoft.EntityFrameworkCore.Migrations;

namespace Datingsida.Migrations.DatingDb
{
    public partial class meddelanden2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageModel",
                table: "MessageModel");

            migrationBuilder.RenameTable(
                name: "MessageModel",
                newName: "Messages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "MessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "MessageModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageModel",
                table: "MessageModel",
                column: "MessageId");
        }
    }
}
