using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserAuthProviders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_auth_providers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProviderKind = table.Column<int>(type: "integer", nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_auth_providers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_auth_providers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_auth_providers_ProviderKind_ProviderKey",
                table: "user_auth_providers",
                columns: new[] { "ProviderKind", "ProviderKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_auth_providers_UserId",
                table: "user_auth_providers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_auth_providers");
        }
    }
}
