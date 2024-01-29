using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherParser.Migrations
{
    /// <inheritdoc />
    public partial class FixBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_CurrentLocationId",
                table: "Users",
                column: "CurrentLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locations_CurrentLocationId",
                table: "Users",
                column: "CurrentLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locations_CurrentLocationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CurrentLocationId",
                table: "Users");
        }
    }
}
