using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternationalWagesManager.Models.Migrations
{
    public partial class AddingWageCurrencyIdColumnInWorkConditionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkConditions_Currencies_ExpensesCurrencyId",
                table: "WorkConditions");

            migrationBuilder.AlterColumn<int>(
                name: "ExpensesCurrencyId",
                table: "WorkConditions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "WageCurrencyId",
                table: "WorkConditions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkConditions_WageCurrencyId",
                table: "WorkConditions",
                column: "WageCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkConditions_Currencies_ExpensesCurrencyId",
                table: "WorkConditions",
                column: "ExpensesCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkConditions_Currencies_WageCurrencyId",
                table: "WorkConditions",
                column: "WageCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkConditions_Currencies_ExpensesCurrencyId",
                table: "WorkConditions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkConditions_Currencies_WageCurrencyId",
                table: "WorkConditions");

            migrationBuilder.DropIndex(
                name: "IX_WorkConditions_WageCurrencyId",
                table: "WorkConditions");

            migrationBuilder.DropColumn(
                name: "WageCurrencyId",
                table: "WorkConditions");

            migrationBuilder.AlterColumn<int>(
                name: "ExpensesCurrencyId",
                table: "WorkConditions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkConditions_Currencies_ExpensesCurrencyId",
                table: "WorkConditions",
                column: "ExpensesCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
