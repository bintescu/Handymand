using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class UpdateSkillAddedCreationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreationUserId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CreationUserId",
                table: "Skills",
                column: "CreationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Users_CreationUserId",
                table: "Skills",
                column: "CreationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Users_CreationUserId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_CreationUserId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "CreationUserId",
                table: "Skills");
        }
    }
}
