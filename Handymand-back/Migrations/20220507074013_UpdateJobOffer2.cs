using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class UpdateJobOffer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCreare",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "IdUtilizatorCreare",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "JobOffer");

            migrationBuilder.AddColumn<double>(
                name: "HighPriceRange",
                table: "JobOffer",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LowPriceRange",
                table: "JobOffer",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighPriceRange",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "LowPriceRange",
                table: "JobOffer");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCreare",
                table: "JobOffer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IdUtilizatorCreare",
                table: "JobOffer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "JobOffer",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
