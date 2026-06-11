using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_food_categories_Name",
                table: "food_categories");

            migrationBuilder.CreateIndex(
                name: "IX_food_categories_Name",
                table: "food_categories",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_food_categories_Name",
                table: "food_categories");

            migrationBuilder.CreateIndex(
                name: "IX_food_categories_Name",
                table: "food_categories",
                column: "Name",
                unique: true);
        }
    }
}
