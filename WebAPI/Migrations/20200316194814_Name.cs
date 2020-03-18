using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Drivers_DrivingLicenseNumber_LicenseCountry",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "LicenseCountry",
                table: "Drivers");

            migrationBuilder.AddColumn<string>(
                name: "LicenseCountryCode",
                table: "Drivers",
                maxLength: 3,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_DrivingLicenseNumber_LicenseCountryCode",
                table: "Drivers",
                columns: new[] { "DrivingLicenseNumber", "LicenseCountryCode" },
                unique: true,
                filter: "[DrivingLicenseNumber] IS NOT NULL AND [LicenseCountryCode] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Drivers_DrivingLicenseNumber_LicenseCountryCode",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "LicenseCountryCode",
                table: "Drivers");

            migrationBuilder.AddColumn<string>(
                name: "LicenseCountry",
                table: "Drivers",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_DrivingLicenseNumber_LicenseCountry",
                table: "Drivers",
                columns: new[] { "DrivingLicenseNumber", "LicenseCountry" },
                unique: true,
                filter: "[DrivingLicenseNumber] IS NOT NULL AND [LicenseCountry] IS NOT NULL");
        }
    }
}
