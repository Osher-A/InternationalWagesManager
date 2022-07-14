using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternationalWagesManager.Models.Migrations
{
    public partial class SeedingCurrencyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT Currencies (Name, Description)" +
                " VALUES ('CHF', 'Swiss Franc'), ('EUR', 'Euro'), ('GBP', 'Sterling Pound'), ('ILS', 'Israeli Shekel'), ('USD', 'United States Dollar') ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Currencies WHERE Id Between 1 AND 5");
        }
    }
}
