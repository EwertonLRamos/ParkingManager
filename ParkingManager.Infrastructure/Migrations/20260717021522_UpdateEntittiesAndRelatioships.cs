using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntittiesAndRelatioships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sectors_Name",
                table: "Sectors");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSessions_SetorName_EntryTime",
                table: "ParkingSessions");

            migrationBuilder.DropColumn(
                name: "SetorName",
                table: "ParkingSessions");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Spots",
                type: "decimal(12,8)",
                precision: 12,
                scale: 8,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)",
                oldPrecision: 9,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Spots",
                type: "decimal(12,8)",
                precision: 12,
                scale: 8,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)",
                oldPrecision: 9,
                oldScale: 6);

            migrationBuilder.AddColumn<bool>(
                name: "IsOccupied",
                table: "Spots",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "SectorId",
                table: "ParkingSessions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpotId",
                table: "ParkingSessions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_Name",
                table: "Sectors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSessions_SectorId",
                table: "ParkingSessions",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSessions_SpotId",
                table: "ParkingSessions",
                column: "SpotId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSessions_Status_ExitTime",
                table: "ParkingSessions",
                columns: new[] { "Status", "ExitTime" });

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSessions_Sectors_SectorId",
                table: "ParkingSessions",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSessions_Spots_SpotId",
                table: "ParkingSessions",
                column: "SpotId",
                principalTable: "Spots",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSessions_Sectors_SectorId",
                table: "ParkingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSessions_Spots_SpotId",
                table: "ParkingSessions");

            migrationBuilder.DropIndex(
                name: "IX_Sectors_Name",
                table: "Sectors");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSessions_SectorId",
                table: "ParkingSessions");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSessions_SpotId",
                table: "ParkingSessions");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSessions_Status_ExitTime",
                table: "ParkingSessions");

            migrationBuilder.DropColumn(
                name: "IsOccupied",
                table: "Spots");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "ParkingSessions");

            migrationBuilder.DropColumn(
                name: "SpotId",
                table: "ParkingSessions");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Spots",
                type: "decimal(9,6)",
                precision: 9,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,8)",
                oldPrecision: 12,
                oldScale: 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Spots",
                type: "decimal(9,6)",
                precision: 9,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,8)",
                oldPrecision: 12,
                oldScale: 8);

            migrationBuilder.AddColumn<string>(
                name: "SetorName",
                table: "ParkingSessions",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_Name",
                table: "Sectors",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSessions_SetorName_EntryTime",
                table: "ParkingSessions",
                columns: new[] { "SetorName", "EntryTime" });
        }
    }
}
