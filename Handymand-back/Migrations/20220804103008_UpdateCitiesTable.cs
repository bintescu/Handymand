using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class UpdateCitiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Bucuresti" },
                    { 16, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Drobeta-Turnu Severin" },
                    { 15, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Suceava" },
                    { 14, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Botosani" },
                    { 13, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Buzau" },
                    { 12, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Arad" },
                    { 11, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Braila" },
                    { 10, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Oradea" },
                    { 9, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Ploiesti" },
                    { 8, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Galati" },
                    { 7, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Brasov" },
                    { 6, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Craiova" },
                    { 5, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Constanta" },
                    { 4, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Timisoara" },
                    { 3, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cluj" },
                    { 2, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Iasi" },
                    { 17, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Slatina" },
                    { 18, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Deva" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cities");
        }
    }
}
