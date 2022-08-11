using Microsoft.EntityFrameworkCore.Migrations;

namespace Handymand.Migrations
{
    public partial class ContractChangeColumnTypePaymentAmountToFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PaymentAmountPerHour",
                table: "Contracts",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(12)",
                oldPrecision: 12,
                oldScale: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PaymentAmountPerHour",
                table: "Contracts",
                type: "float(12)",
                precision: 12,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
