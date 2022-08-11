using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class Feedback_UpdateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationUserId = table.Column<int>(type: "int", nullable: false),
                    RefferedUserId = table.Column<int>(type: "int", nullable: false),
                    JobOfferId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    FromClientToFreelancer = table.Column<bool>(type: "bit", nullable: false),
                    FromFreelancerToClient = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_JobOffer_JobOfferId",
                        column: x => x.JobOfferId,
                        principalTable: "JobOffer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_CreationUserId",
                        column: x => x.CreationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_RefferedUserId",
                        column: x => x.RefferedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_CreationUserId",
                table: "Feedbacks",
                column: "CreationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_JobOfferId",
                table: "Feedbacks",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_RefferedUserId",
                table: "Feedbacks",
                column: "RefferedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");
        }
    }
}
