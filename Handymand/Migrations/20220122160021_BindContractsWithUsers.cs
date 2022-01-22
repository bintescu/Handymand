using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class BindContractsWithUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Clients_IdCreationUser",
                table: "Contracts");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Users_IdCreationUser",
                table: "Contracts",
                column: "IdCreationUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Users_IdCreationUser",
                table: "Contracts");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Clients_IdCreationUser",
                table: "Contracts",
                column: "IdCreationUser",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
