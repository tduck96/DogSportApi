using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealPetApi.Migrations
{
    public partial class commentphotourl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WriterPhotoUrl",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WriterPhotoUrl",
                table: "Comments");
        }
    }
}
