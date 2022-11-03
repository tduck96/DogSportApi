using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealPetApi.Migrations
{
    public partial class commentName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WriterName",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WriterName",
                table: "Comments");
        }
    }
}
