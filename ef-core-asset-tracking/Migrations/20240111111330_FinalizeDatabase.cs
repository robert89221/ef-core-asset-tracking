using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ef_core_asset_tracking.Migrations
{
    /// <inheritdoc />
    public partial class FinalizeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Brand = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Location = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    DateOfPurchase = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "Brand", "DateOfPurchase", "Location", "Model", "Price", "Type" },
                values: new object[,]
                {
                    { 1, "Siemens", new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "WS100", 2500, 0 },
                    { 2, "Nokia", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "6.1", 500, 1 },
                    { 3, "Samsung", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ST-14", 400, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
