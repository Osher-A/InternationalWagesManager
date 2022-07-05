using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternationalWagesManager.Models.Migrations
{
    public partial class AddCurrencyIdColumnInSalaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
