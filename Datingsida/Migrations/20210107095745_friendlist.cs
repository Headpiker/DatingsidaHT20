using Microsoft.EntityFrameworkCore.Migrations;

namespace Datingsida.Migrations
{
    public partial class friendlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Friendlists",
                columns: table => new
                {
                    FriendRequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserReceiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserSender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendlists", x => x.FriendRequestID);
                });

            migrationBuilder.CreateTable(
                name: "ProfileModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexuality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFilepath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Presentation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProfileModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileModel_ProfileModel_ProfileModelId",
                        column: x => x.ProfileModelId,
                        principalTable: "ProfileModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileModel_ProfileModelId",
                table: "ProfileModel",
                column: "ProfileModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendlists");

            migrationBuilder.DropTable(
                name: "ProfileModel");
        }
    }
}
