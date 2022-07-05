using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternationalWagesManager.Models.Migrations
{
    public partial class AddingExpensesCurrencyIdColumnInWorkConditionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpensesCurrencyId",
                table: "WorkConditions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkConditions_ExpensesCurrencyId",
                table: "WorkConditions",
                column: "ExpensesCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkConditions_Currencies_ExpensesCurrencyId",
                table: "WorkConditions",
                column: "ExpensesCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkConditions_Currencies_ExpensesCurrencyId",
                table: "WorkConditions");

            migrationBuilder.DropIndex(
                name: "IX_WorkConditions_ExpensesCurrencyId",
                table: "WorkConditions");

            migrationBuilder.DropColumn(
                name: "ExpensesCurrencyId",
                table: "WorkConditions");
        }
    }
}
