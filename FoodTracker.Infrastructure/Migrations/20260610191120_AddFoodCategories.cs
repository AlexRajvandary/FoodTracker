using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFoodCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_food_entries_food_items_FoodItemId",
                table: "food_entries");

            migrationBuilder.DropIndex(
                name: "IX_food_items_Category",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "PortionGrams",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "PortionHint",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "PortionNote",
                table: "food_entries");

            migrationBuilder.RenameColumn(
                name: "FoodItemId",
                table: "food_entries",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_food_entries_FoodItemId",
                table: "food_entries",
                newName: "IX_food_entries_FoodId");

            migrationBuilder.AlterColumn<double>(
                name: "ProteinsPer100g",
                table: "food_items",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<double>(
                name: "FatsPer100g",
                table: "food_items",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<double>(
                name: "CarbsPer100g",
                table: "food_items",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<double>(
                name: "CaloriesPer100g",
                table: "food_items",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "food_items",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "food_items",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FiberPer100g",
                table: "food_items",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "food_items",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SaltPer100g",
                table: "food_items",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SaturatedFatPer100g",
                table: "food_items",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ServingSizeGrams",
                table: "food_items",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SugarsPer100g",
                table: "food_items",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAtUtc",
                table: "food_items",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Proteins",
                table: "food_entries",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<double>(
                name: "GramsConsumed",
                table: "food_entries",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<double>(
                name: "Fats",
                table: "food_entries",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<double>(
                name: "Carbs",
                table: "food_entries",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<double>(
                name: "Calories",
                table: "food_entries",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<double>(
                name: "CaloriesPerHour",
                table: "activity_types",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CaloriesPer100Reps",
                table: "activity_types",
                type: "double precision",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "food_categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExternalId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "food_item_categories",
                columns: table => new
                {
                    FoodCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    FoodItemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_item_categories", x => new { x.FoodItemId, x.FoodCategoryId });
                    table.ForeignKey(
                        name: "FK_food_item_categories_food_categories_FoodCategoryId",
                        column: x => x.FoodCategoryId,
                        principalTable: "food_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_food_item_categories_food_items_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "food_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_food_items_Barcode",
                table: "food_items",
                column: "Barcode");

            migrationBuilder.CreateIndex(
                name: "IX_food_items_Brand",
                table: "food_items",
                column: "Brand");

            migrationBuilder.CreateIndex(
                name: "IX_food_categories_ExternalId",
                table: "food_categories",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_food_categories_Name",
                table: "food_categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_food_item_categories_FoodCategoryId",
                table: "food_item_categories",
                column: "FoodCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_food_entries_food_items_FoodId",
                table: "food_entries",
                column: "FoodId",
                principalTable: "food_items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_food_entries_food_items_FoodId",
                table: "food_entries");

            migrationBuilder.DropTable(
                name: "food_item_categories");

            migrationBuilder.DropTable(
                name: "food_categories");

            migrationBuilder.DropIndex(
                name: "IX_food_items_Barcode",
                table: "food_items");

            migrationBuilder.DropIndex(
                name: "IX_food_items_Brand",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "FiberPer100g",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "SaltPer100g",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "SaturatedFatPer100g",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "ServingSizeGrams",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "SugarsPer100g",
                table: "food_items");

            migrationBuilder.DropColumn(
                name: "UpdatedAtUtc",
                table: "food_items");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "food_entries",
                newName: "FoodItemId");

            migrationBuilder.RenameIndex(
                name: "IX_food_entries_FoodId",
                table: "food_entries",
                newName: "IX_food_entries_FoodItemId");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProteinsPer100g",
                table: "food_items",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "FatsPer100g",
                table: "food_items",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "CarbsPer100g",
                table: "food_items",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "CaloriesPer100g",
                table: "food_items",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "food_items",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PortionGrams",
                table: "food_items",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortionHint",
                table: "food_items",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Proteins",
                table: "food_entries",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "GramsConsumed",
                table: "food_entries",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Fats",
                table: "food_entries",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Carbs",
                table: "food_entries",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Calories",
                table: "food_entries",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AddColumn<string>(
                name: "PortionNote",
                table: "food_entries",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CaloriesPerHour",
                table: "activity_types",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CaloriesPer100Reps",
                table: "activity_types",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_food_items_Category",
                table: "food_items",
                column: "Category");

            migrationBuilder.AddForeignKey(
                name: "FK_food_entries_food_items_FoodItemId",
                table: "food_entries",
                column: "FoodItemId",
                principalTable: "food_items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
