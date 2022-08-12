using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class NotificationType_NotificationAffectedList_SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description" },
                values: new object[] { 1, new DateTime(2022, 8, 12, 15, 2, 15, 549, DateTimeKind.Local).AddTicks(7784), null, "Create Offer" });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description" },
                values: new object[] { 2, new DateTime(2022, 8, 12, 15, 2, 15, 552, DateTimeKind.Local).AddTicks(1979), null, "Accept Offer" });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description" },
                values: new object[] { 3, new DateTime(2022, 8, 12, 15, 2, 15, 552, DateTimeKind.Local).AddTicks(2012), null, "Close Contract" });

            migrationBuilder.InsertData(
                table: "NotificationAffectedLists",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name", "NotificationTypeId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 8, 12, 15, 2, 15, 552, DateTimeKind.Local).AddTicks(3519), null, "My Active Offers", 1 },
                    { 3, new DateTime(2022, 8, 12, 15, 2, 15, 552, DateTimeKind.Local).AddTicks(3839), null, "My Active Job Offers", 1 },
                    { 2, new DateTime(2022, 8, 12, 15, 2, 15, 552, DateTimeKind.Local).AddTicks(3827), null, "My Accepted Offers", 2 },
                    { 4, new DateTime(2022, 8, 12, 15, 2, 15, 552, DateTimeKind.Local).AddTicks(3841), null, "Jobs To Pay For", 2 },
                    { 5, new DateTime(2022, 8, 12, 15, 2, 15, 552, DateTimeKind.Local).AddTicks(3845), null, "Closed Job Contracts", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationAffectedLists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NotificationAffectedLists",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NotificationAffectedLists",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "NotificationAffectedLists",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "NotificationAffectedLists",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
