using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealPetApi.Migrations
{
    public partial class DogPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DogPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DogPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DogPhotos_Dogs_DogId",
                        column: x => x.DogId,
                        principalTable: "Dogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DogPhotos_DogId",
                table: "DogPhotos",
                column: "DogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DogPhotos");
        }
    }
}
