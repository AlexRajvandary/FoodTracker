using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalIdToFoodItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "food_items",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Fiber",
                table: "food_entries",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Salt",
                table: "food_entries",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SaturatedFat",
                table: "food_entries",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Sugars",
                table: "food_entries",
                type: "double precision",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_food_items_ExternalId",
                table: "food_items",
                column: "ExternalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_food_items_ExternalId",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "Fiber",
                table: "food_entries");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "food_entries");

            migrationBuilder.DropColumn(
                name: "SaturatedFat",
                table: "food_entries");

            migrationBuilder.DropColumn(
                name: "Sugars",
                table: "food_entries");
        }
    }
}
