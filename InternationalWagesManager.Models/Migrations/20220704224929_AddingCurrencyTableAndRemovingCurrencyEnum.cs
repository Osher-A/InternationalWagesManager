using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternationalWagesManager.Models.Migrations
{
    public partial class AddingCurrencyTableAndRemovingCurrencyEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculatorCurrency",
                table: "WorkConditions");

            migrationBuilder.DropColumn(
                name: "ExpensesCurrency",
                table: "WorkConditions");

            migrationBuilder.DropColumn(
                name: "WageCurrency",
                table: "WorkConditions");

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.AddColumn<int>(
                name: "CalculatorCurrency",
                table: "WorkConditions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExpensesCurrency",
                table: "WorkConditions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WageCurrency",
                table: "WorkConditions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
