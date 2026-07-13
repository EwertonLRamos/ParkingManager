using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    EntryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OccupancyRate = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SetorName = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: false),
                    SectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spots_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSessions_EntryTime",
                table: "ParkingSessions",
                column: "EntryTime");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSessions_LicensePlate",
                table: "ParkingSessions",
                column: "LicensePlate");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSessions_SetorName_EntryTime",
                table: "ParkingSessions",
                columns: new[] { "SetorName", "EntryTime" });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSessions_Status",
                table: "ParkingSessions",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_Name",
                table: "Sectors",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Spots_Latitude_Longitude",
                table: "Spots",
                columns: new[] { "Latitude", "Longitude" });

            migrationBuilder.CreateIndex(
                name: "IX_Spots_SectorId",
                table: "Spots",
                column: "SectorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSessions");

            migrationBuilder.DropTable(
                name: "Spots");

            migrationBuilder.DropTable(
                name: "Sectors");
        }
    }
}
