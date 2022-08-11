using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class ModifyContractAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Users_IdCreationUser",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "IdRefferedUser",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "PaymentAmount",
                table: "Contracts",
                newName: "PaymentAmountPerHour");

            migrationBuilder.RenameColumn(
                name: "IdCreationUser",
                table: "Contracts",
                newName: "RefferedUserId");

            migrationBuilder.RenameColumn(
                name: "ExpectedDurationInHours",
                table: "Contracts",
                newName: "JobOfferId");

            migrationBuilder.RenameColumn(
                name: "ComplexityGrade",
                table: "Contracts",
                newName: "CreationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_IdCreationUser",
                table: "Contracts",
                newName: "IX_Contracts_RefferedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CreationUserId",
                table: "Contracts",
                column: "CreationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_JobOfferId",
                table: "Contracts",
                column: "JobOfferId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_JobOffer_JobOfferId",
                table: "Contracts",
                column: "JobOfferId",
                principalTable: "JobOffer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Users_CreationUserId",
                table: "Contracts",
                column: "CreationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Users_RefferedUserId",
                table: "Contracts",
                column: "RefferedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_JobOffer_JobOfferId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Users_CreationUserId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Users_RefferedUserId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_CreationUserId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_JobOfferId",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "RefferedUserId",
                table: "Contracts",
                newName: "IdCreationUser");

            migrationBuilder.RenameColumn(
                name: "PaymentAmountPerHour",
                table: "Contracts",
                newName: "PaymentAmount");

            migrationBuilder.RenameColumn(
                name: "JobOfferId",
                table: "Contracts",
                newName: "ExpectedDurationInHours");

            migrationBuilder.RenameColumn(
                name: "CreationUserId",
                table: "Contracts",
                newName: "ComplexityGrade");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_RefferedUserId",
                table: "Contracts",
                newName: "IX_Contracts_IdCreationUser");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdRefferedUser",
                table: "Contracts",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Users_IdCreationUser",
                table: "Contracts",
                column: "IdCreationUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
