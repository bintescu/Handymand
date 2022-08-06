using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class ManyToManySkillsJobOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobOffersSkills",
                columns: table => new
                {
                    IdSkill = table.Column<int>(type: "int", nullable: false),
                    IdJobOffer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOffersSkills", x => new { x.IdSkill, x.IdJobOffer });
                    table.ForeignKey(
                        name: "FK_JobOffersSkills_JobOffer_IdJobOffer",
                        column: x => x.IdJobOffer,
                        principalTable: "JobOffer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOffersSkills_Skills_IdSkill",
                        column: x => x.IdSkill,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobOffersSkills_IdJobOffer",
                table: "JobOffersSkills",
                column: "IdJobOffer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobOffersSkills");
        }
    }
}
