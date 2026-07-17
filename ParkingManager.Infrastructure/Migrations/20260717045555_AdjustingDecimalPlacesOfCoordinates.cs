using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustingDecimalPlacesOfCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Spots",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,8)",
                oldPrecision: 12,
                oldScale: 8);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Spots",
                type: "decimal(12,6)",
                precision: 12,
                scale: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,8)",
                oldPrecision: 12,
                oldScale: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Spots",
                type: "decimal(12,8)",
                precision: 12,
                scale: 8,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Spots",
                type: "decimal(12,8)",
                precision: 12,
                scale: 8,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,6)",
                oldPrecision: 12,
                oldScale: 6);
        }
    }
}
