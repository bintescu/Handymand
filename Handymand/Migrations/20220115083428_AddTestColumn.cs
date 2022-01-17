using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class AddTestColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nume",
                table: "ModelsRelations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nume",
                table: "ModelsRelations");
        }
    }
}
