using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class UpdateSkillAddModificationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModificationUserId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ModificationUserId",
                table: "Skills",
                column: "ModificationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Users_ModificationUserId",
                table: "Skills",
                column: "ModificationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Users_ModificationUserId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_ModificationUserId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "ModificationUserId",
                table: "Skills");
        }
    }
}
