using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class OneToManyJobOfferAndCities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "JobOffer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOffer_CityId",
                table: "JobOffer",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffer_Cities_CityId",
                table: "JobOffer",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffer_Cities_CityId",
                table: "JobOffer");

            migrationBuilder.DropIndex(
                name: "IX_JobOffer_CityId",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "JobOffer");
        }
    }
}
