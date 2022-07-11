using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternationalWagesManager.Models.Migrations
{
    public partial class AddingPayCurrencyIdColumnToWorkConditionsTableAndRemovingItFromSalaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_Currencies_CurrencyId",
                table: "Salaries");

            migrationBuilder.DropIndex(
                name: "IX_Salaries_CurrencyId",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Salaries");

            migrationBuilder.RenameColumn(
                name: "Month",
                table: "SalariesComponents",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "PayCurrencyId",
                table: "WorkConditions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkConditions_PayCurrencyId",
                table: "WorkConditions",
                column: "PayCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkConditions_Currencies_PayCurrencyId",
                table: "WorkConditions",
                column: "PayCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkConditions_Currencies_PayCurrencyId",
                table: "WorkConditions");

            migrationBuilder.DropIndex(
                name: "IX_WorkConditions_PayCurrencyId",
                table: "WorkConditions");

            migrationBuilder.DropColumn(
                name: "PayCurrencyId",
                table: "WorkConditions");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "SalariesComponents",
                newName: "Month");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_CurrencyId",
                table: "Salaries",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_Currencies_CurrencyId",
                table: "Salaries",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
