using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class Requird : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_PlateNumber",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_UserName",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_DrivingLicenseNumber_LicenseCountryCode",
                table: "Drivers");

            migrationBuilder.AlterColumn<string>(
                name: "PlateNumber",
                table: "Vehicles",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "message",
                table: "DriverVehicleMessages",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Drivers",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Drivers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LicenseCountryCode",
                table: "Drivers",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Drivers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DrivingLicenseNumber",
                table: "Drivers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_PlateNumber",
                table: "Vehicles",
                column: "PlateNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_UserName",
                table: "Drivers",
                column: "UserName",
                unique: true)
                .Annotation("SqlServer:Include", new[] { "Password" });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_DrivingLicenseNumber_LicenseCountryCode",
                table: "Drivers",
                columns: new[] { "DrivingLicenseNumber", "LicenseCountryCode" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_PlateNumber",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_UserName",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_DrivingLicenseNumber_LicenseCountryCode",
                table: "Drivers");

            migrationBuilder.AlterColumn<string>(
                name: "PlateNumber",
                table: "Vehicles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "message",
                table: "DriverVehicleMessages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Drivers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LicenseCountryCode",
                table: "Drivers",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Drivers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DrivingLicenseNumber",
                table: "Drivers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_PlateNumber",
                table: "Vehicles",
                column: "PlateNumber",
                unique: true,
                filter: "[PlateNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_UserName",
                table: "Drivers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL")
                .Annotation("SqlServer:Include", new[] { "Password" });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_DrivingLicenseNumber_LicenseCountryCode",
                table: "Drivers",
                columns: new[] { "DrivingLicenseNumber", "LicenseCountryCode" },
                unique: true,
                filter: "[DrivingLicenseNumber] IS NOT NULL AND [LicenseCountryCode] IS NOT NULL");
        }
    }
}
