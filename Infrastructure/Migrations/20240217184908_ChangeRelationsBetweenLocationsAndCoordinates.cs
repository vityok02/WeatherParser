using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationsBetweenLocationsAndCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinates_Locations_LocationId",
                table: "Coordinates");

            migrationBuilder.DropIndex(
                name: "IX_Coordinates_LocationId",
                table: "Coordinates");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Coordinates");

            migrationBuilder.AddColumn<long>(
                name: "CoordinatesId",
                table: "Locations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CoordinatesId",
                table: "Locations",
                column: "CoordinatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Coordinates_CoordinatesId",
                table: "Locations",
                column: "CoordinatesId",
                principalTable: "Coordinates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Coordinates_CoordinatesId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CoordinatesId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CoordinatesId",
                table: "Locations");

            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                table: "Coordinates",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Coordinates_LocationId",
                table: "Coordinates",
                column: "LocationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinates_Locations_LocationId",
                table: "Coordinates",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
