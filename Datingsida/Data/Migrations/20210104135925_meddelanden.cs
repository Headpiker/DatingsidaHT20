using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Datingsida.Migrations.DatingDb
{
    public partial class meddelanden : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageModel",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subjekt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfPost = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageModel", x => x.MessageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageModel");
        }
    }
}
