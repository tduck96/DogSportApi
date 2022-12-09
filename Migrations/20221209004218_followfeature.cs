using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealPetApi.Migrations
{
    public partial class followfeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFollowing",
                columns: table => new
                {
                    UserProfileId = table.Column<int>(type: "int", nullable: false),
                    UserFollowsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollowing", x => new { x.UserProfileId, x.UserFollowsId });
                    table.ForeignKey(
                        name: "FK_UserFollowing_UserProfiles_UserFollowsId",
                        column: x => x.UserFollowsId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserFollowing_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowing_UserFollowsId",
                table: "UserFollowing",
                column: "UserFollowsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFollowing");
        }
    }
}
