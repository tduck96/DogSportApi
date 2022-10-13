using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealPetApi.Migrations
{
    public partial class profiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Wallposts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Dogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserProfileId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallposts_UserProfileId",
                table: "Wallposts",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserProfileId",
                table: "Photos",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_UserProfileId",
                table: "Dogs",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserProfileId",
                table: "Comments",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_LocationId",
                table: "UserProfiles",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UserProfiles_UserProfileId",
                table: "Comments",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_UserProfiles_UserProfileId",
                table: "Dogs",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_UserProfiles_UserProfileId",
                table: "Photos",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallposts_UserProfiles_UserProfileId",
                table: "Wallposts",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserProfiles_UserProfileId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_UserProfiles_UserProfileId",
                table: "Dogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_UserProfiles_UserProfileId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallposts_UserProfiles_UserProfileId",
                table: "Wallposts");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Wallposts_UserProfileId",
                table: "Wallposts");

            migrationBuilder.DropIndex(
                name: "IX_Photos_UserProfileId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Dogs_UserProfileId",
                table: "Dogs");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserProfileId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Wallposts");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Comments");
        }
    }
}
