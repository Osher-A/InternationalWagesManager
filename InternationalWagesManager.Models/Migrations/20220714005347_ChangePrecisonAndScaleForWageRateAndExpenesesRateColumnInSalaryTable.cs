using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternationalWagesManager.Models.Migrations
{
    public partial class ChangePrecisonAndScaleForWageRateAndExpenesesRateColumnInSalaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "WageRate",
                table: "Salaries",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExpensesRate",
                table: "Salaries",
                type: "decimal(9,5)",
                precision: 9,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "WageRate",
                table: "Salaries",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExpensesRate",
                table: "Salaries",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,5)",
                oldPrecision: 9,
                oldScale: 5);
        }
    }
}
