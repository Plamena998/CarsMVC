using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataContext.Migrations
{
    /// <inheritdoc />
    public partial class AddFavCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_favoriteCars_CarId",
                table: "favoriteCars");

            migrationBuilder.CreateIndex(
                name: "IX_favoriteCars_CarId_UserId",
                table: "favoriteCars",
                columns: new[] { "CarId", "UserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_favoriteCars_CarId_UserId",
                table: "favoriteCars");

            migrationBuilder.CreateIndex(
                name: "IX_favoriteCars_CarId",
                table: "favoriteCars",
                column: "CarId");
        }
    }
}
