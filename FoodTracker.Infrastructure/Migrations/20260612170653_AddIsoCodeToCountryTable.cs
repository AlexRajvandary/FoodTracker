using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsoCodeToCountryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsoCode",
                table: "countries",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsoCode",
                table: "countries");
        }
    }
}
