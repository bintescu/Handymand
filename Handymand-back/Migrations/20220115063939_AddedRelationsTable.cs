using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class AddedRelationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Model1MMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model1MMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Model1OMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model1OMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Model1OOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model1OOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Model2MMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model2MMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Model2OMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model1OMId = table.Column<int>(type: "int", nullable: true),
                    Model1Id = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model2OMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Model2OMs_Model1OMs_Model1OMId",
                        column: x => x.Model1OMId,
                        principalTable: "Model1OMs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Model2OOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model1Id = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model2OOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Model2OOs_Model1OOs_Model1Id",
                        column: x => x.Model1Id,
                        principalTable: "Model1OOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelsRelations",
                columns: table => new
                {
                    Model1MMId = table.Column<int>(type: "int", nullable: false),
                    Model2MMId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelsRelations", x => new { x.Model1MMId, x.Model2MMId });
                    table.ForeignKey(
                        name: "FK_ModelsRelations_Model1MMs_Model1MMId",
                        column: x => x.Model1MMId,
                        principalTable: "Model1MMs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModelsRelations_Model2MMs_Model2MMId",
                        column: x => x.Model2MMId,
                        principalTable: "Model2MMs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Model2OMs_Model1OMId",
                table: "Model2OMs",
                column: "Model1OMId");

            migrationBuilder.CreateIndex(
                name: "IX_Model2OOs_Model1Id",
                table: "Model2OOs",
                column: "Model1Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModelsRelations_Model2MMId",
                table: "ModelsRelations",
                column: "Model2MMId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Model2OMs");

            migrationBuilder.DropTable(
                name: "Model2OOs");

            migrationBuilder.DropTable(
                name: "ModelsRelations");

            migrationBuilder.DropTable(
                name: "Model1OMs");

            migrationBuilder.DropTable(
                name: "Model1OOs");

            migrationBuilder.DropTable(
                name: "Model1MMs");

            migrationBuilder.DropTable(
                name: "Model2MMs");
        }
    }
}
