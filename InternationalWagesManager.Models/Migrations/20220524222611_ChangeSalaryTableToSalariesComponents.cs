using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternationalWagesManager.Models.Migrations
{
    public partial class ChangeSalaryTableToSalariesComponents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.CreateTable(
                name: "SalariesComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalHours = table.Column<float>(type: "real", nullable: false),
                    BonusHours = table.Column<float>(type: "real", nullable: false),
                    BonusWage = table.Column<float>(type: "real", nullable: false),
                    Expenses = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalariesComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalariesComponents_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalariesComponents_EmployeeId",
                table: "SalariesComponents",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalariesComponents");

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    BonusHours = table.Column<float>(type: "real", nullable: false),
                    BonusWage = table.Column<float>(type: "real", nullable: false),
                    Expenses = table.Column<float>(type: "real", nullable: false),
                    Month = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalHours = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salaries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmployeeId",
                table: "Salaries",
                column: "EmployeeId");
        }
    }
}
