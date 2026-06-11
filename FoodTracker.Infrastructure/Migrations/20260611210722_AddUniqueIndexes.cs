using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_food_items_Barcode",
                table: "food_items");

            migrationBuilder.DropIndex(
                name: "IX_food_categories_ExternalId",
                table: "food_categories");

            migrationBuilder.CreateIndex(
                name: "IX_food_items_Barcode",
                table: "food_items",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_food_categories_ExternalId",
                table: "food_categories",
                column: "ExternalId",
                unique: true,
                filter: "\"ExternalId\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_food_items_Barcode",
                table: "food_items");

            migrationBuilder.DropIndex(
                name: "IX_food_categories_ExternalId",
                table: "food_categories");

            migrationBuilder.CreateIndex(
                name: "IX_food_items_Barcode",
                table: "food_items",
                column: "Barcode");

            migrationBuilder.CreateIndex(
                name: "IX_food_categories_ExternalId",
                table: "food_categories",
                column: "ExternalId");
        }
    }
}
