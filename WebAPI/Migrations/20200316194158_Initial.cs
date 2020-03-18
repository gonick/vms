using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    ConnectionDetails = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    PlateNumber = table.Column<string>(maxLength: 50, nullable: true),
                    CurrentSpeed = table.Column<float>(nullable: false),
                    CurrentLatitude = table.Column<double>(nullable: false),
                    CurrentLongitude = table.Column<double>(nullable: false),
                    CurrentEngineTemperature = table.Column<float>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    DrivingLicenseNumber = table.Column<string>(maxLength: 50, nullable: true),
                    LicenseCountry = table.Column<string>(maxLength: 3, nullable: true),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverId);
                    table.ForeignKey(
                        name: "FK_Drivers_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverVehicleMessages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(nullable: false),
                    VehicleId = table.Column<int>(nullable: false),
                    message = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverVehicleMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverVehicleMessages_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DriverVehicleMessages_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_UserName",
                table: "Drivers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL")
                .Annotation("SqlServer:Include", new[] { "Password" });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_VehicleId",
                table: "Drivers",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_DrivingLicenseNumber_LicenseCountry",
                table: "Drivers",
                columns: new[] { "DrivingLicenseNumber", "LicenseCountry" },
                unique: true,
                filter: "[DrivingLicenseNumber] IS NOT NULL AND [LicenseCountry] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DriverVehicleMessages_VehicleId",
                table: "DriverVehicleMessages",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverVehicleMessages_DriverId_VehicleId_CreatedAt",
                table: "DriverVehicleMessages",
                columns: new[] { "DriverId", "VehicleId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_PlateNumber",
                table: "Vehicles",
                column: "PlateNumber",
                unique: true,
                filter: "[PlateNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverVehicleMessages");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
