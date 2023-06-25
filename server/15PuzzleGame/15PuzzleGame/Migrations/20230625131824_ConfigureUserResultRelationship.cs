using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _15PuzzleGame.Migrations
{
    public partial class ConfigureUserResultRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Results_MyUserId",
                table: "Results",
                column: "MyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Users_MyUserId",
                table: "Results",
                column: "MyUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Users_MyUserId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_MyUserId",
                table: "Results");
        }
    }
}
