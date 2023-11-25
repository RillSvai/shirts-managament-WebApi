using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShirtsManagament.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shirts",
                columns: new[] { "Id", "Brand", "Color", "Gender", "Price", "Size" },
                values: new object[,]
                {
                    { 1, "My brand", "Black", "M", 200m, 10.0 },
                    { 2, "My brand", "Yellow", "M", 250m, 12.0 },
                    { 3, "Another brand", "Blue", "F", 180m, 8.0 },
                    { 4, "Another brand", null, "F", 200m, 9.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shirts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shirts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shirts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Shirts",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
