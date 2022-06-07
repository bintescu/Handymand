using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class UpdateJobOffer3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffer_Users_UtilizatorCreareId",
                table: "JobOffer");

            migrationBuilder.RenameColumn(
                name: "UtilizatorCreareId",
                table: "JobOffer",
                newName: "CreationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_JobOffer_UtilizatorCreareId",
                table: "JobOffer",
                newName: "IX_JobOffer_CreationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffer_Users_CreationUserId",
                table: "JobOffer",
                column: "CreationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffer_Users_CreationUserId",
                table: "JobOffer");

            migrationBuilder.RenameColumn(
                name: "CreationUserId",
                table: "JobOffer",
                newName: "UtilizatorCreareId");

            migrationBuilder.RenameIndex(
                name: "IX_JobOffer_CreationUserId",
                table: "JobOffer",
                newName: "IX_JobOffer_UtilizatorCreareId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffer_Users_UtilizatorCreareId",
                table: "JobOffer",
                column: "UtilizatorCreareId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
