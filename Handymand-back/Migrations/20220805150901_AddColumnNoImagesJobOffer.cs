using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class AddColumnNoImagesJobOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoImages",
                table: "JobOffer",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoImages",
                table: "JobOffer");
        }
    }
}
