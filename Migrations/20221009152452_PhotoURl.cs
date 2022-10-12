using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealPetApi.Migrations
{
    public partial class PhotoURl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HandlerId",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_HandlerId",
                table: "Photos",
                column: "HandlerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Handlers_HandlerId",
                table: "Photos",
                column: "HandlerId",
                principalTable: "Handlers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Handlers_HandlerId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_HandlerId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "HandlerId",
                table: "Photos");
        }
    }
}
