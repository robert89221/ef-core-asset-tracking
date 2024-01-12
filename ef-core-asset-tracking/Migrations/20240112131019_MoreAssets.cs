using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ef_core_asset_tracking.Migrations
{
    /// <inheritdoc />
    public partial class MoreAssets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfPurchase",
                value: new DateTime(2021, 1, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Brand", "DateOfPurchase", "Location", "Model", "Price", "Type" },
                values: new object[] { "Asus", new DateTime(2023, 11, 23, 0, 0, 0, 0, DateTimeKind.Local), 0, "Zenbook", 745, 0 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Brand", "DateOfPurchase", "Location", "Model", "Price", "Type" },
                values: new object[] { "Nokia", new DateTime(2021, 4, 17, 0, 0, 0, 0, DateTimeKind.Local), 0, "4.2", 180, 1 });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "Brand", "DateOfPurchase", "Location", "Model", "Price", "Type" },
                values: new object[,]
                {
                    { 4, "Xiaomi", new DateTime(2020, 9, 29, 0, 0, 0, 0, DateTimeKind.Local), 2, "Redmi 5", 320, 1 },
                    { 5, "LG", new DateTime(2022, 2, 11, 0, 0, 0, 0, DateTimeKind.Local), 2, "Max", 545, 1 },
                    { 6, "Vivo", new DateTime(2021, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), 2, "V-10Micro", 125, 1 },
                    { 7, "Samsung", new DateTime(2021, 5, 27, 0, 0, 0, 0, DateTimeKind.Local), 1, "Pad 5", 855, 2 },
                    { 8, "Asus", new DateTime(2022, 8, 30, 0, 0, 0, 0, DateTimeKind.Local), 1, "Matepad", 395, 2 },
                    { 9, "Huawei", new DateTime(2023, 6, 26, 0, 0, 0, 0, DateTimeKind.Local), 1, "Ultra", 680, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfPurchase",
                value: new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Brand", "DateOfPurchase", "Location", "Model", "Price", "Type" },
                values: new object[] { "Nokia", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "6.1", 500, 1 });

            migrationBuilder.UpdateData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Brand", "DateOfPurchase", "Location", "Model", "Price", "Type" },
                values: new object[] { "Samsung", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ST-14", 400, 2 });
        }
    }
}
