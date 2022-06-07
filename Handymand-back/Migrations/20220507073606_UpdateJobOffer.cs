using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class UpdateJobOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffer_Users_UserId",
                table: "JobOffer");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "JobOffer",
                newName: "UtilizatorCreareId");

            migrationBuilder.RenameIndex(
                name: "IX_JobOffer_UserId",
                table: "JobOffer",
                newName: "IX_JobOffer_UtilizatorCreareId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCreare",
                table: "JobOffer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "JobOffer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdUtilizatorCreare",
                table: "JobOffer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "JobOffer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "JobOffer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffer_Users_UtilizatorCreareId",
                table: "JobOffer",
                column: "UtilizatorCreareId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffer_Users_UtilizatorCreareId",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "DataCreare",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "IdUtilizatorCreare",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "JobOffer");

            migrationBuilder.RenameColumn(
                name: "UtilizatorCreareId",
                table: "JobOffer",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_JobOffer_UtilizatorCreareId",
                table: "JobOffer",
                newName: "IX_JobOffer_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffer_Users_UserId",
                table: "JobOffer",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
