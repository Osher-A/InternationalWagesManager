using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternationalWagesManager.Models.Migrations
{
    public partial class AddWagesPayRateAndExpensesPayRateColumnsInSalaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ExpensesRate",
                table: "Salaries",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "WageRate",
                table: "Salaries",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpensesRate",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "WageRate",
                table: "Salaries");
        }
    }
}
