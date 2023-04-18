using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternationalWagesManager.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddingAspNetRolesAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42358d3e-3c22-45e1-be81-6caa7ba865ef",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Employee", "EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1b5952a-2162-46c7-b29e-1a2a68922c14",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Employer", "EMPLOYER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9711a9b-c644-4a22-a88c-64c75384991b", "AQAAAAIAAYagAAAAEArOvQWl/H/qjyhhtZwEhzrCnz9NUZ52vCShvfX+PBDUTRTJNWSGlhuEyB/ZJ1W/dw==", "da067fc7-1640-4ad7-b702-1b4fe2382d8a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac9f3c1e-49fc-437e-a582-e71747ae91de", "AQAAAAIAAYagAAAAEDkub2v7fNN3rCRZoCelwFYNmDi/ZYI2Ea3yIPGd2Z7mF6Q4dCb71auXJc/LSxBu5w==", "461a4257-5add-4a27-8b3f-63b54e8dc3ae" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42358d3e-3c22-45e1-be81-6caa7ba865ef",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1b5952a-2162-46c7-b29e-1a2a68922c14",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Administrator", "ADMINISTRATOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc5b29af-3af2-45dc-b68a-4f73123f0e42", "AQAAAAIAAYagAAAAEL7JneRNT5x8r/lG0Jmo+FNPQsmnTnhhlw9lS/ldgdIr50fsUB++DOuzbevDQJJN/g==", "c17477f9-7f10-4038-8e27-65d477848897" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f5e9eba1-a4c2-458b-935b-c9ebcfc63ad4", "AQAAAAIAAYagAAAAEAOvL7LrjD14LFZ4NPahDN3Ba2u+1l61d5uJGHjaDOFbZ2GdZvs+HsOpsnEAEt/qWg==", "3eba4171-e150-4a29-a81a-5dd442926c5f" });
        }
    }
}
