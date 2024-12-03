using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace J104895.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "434b053f-e452-4f22-8db1-bff04df24b09", null, "seller", "seller" },
                    { "4435c6a2-29cd-4fc9-baaa-aceca966d880", null, "admin", "admin" },
                    { "6e2f78a4-23b6-4e57-a59e-82c84c22f9f8", null, "client", "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "434b053f-e452-4f22-8db1-bff04df24b09");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4435c6a2-29cd-4fc9-baaa-aceca966d880");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e2f78a4-23b6-4e57-a59e-82c84c22f9f8");
        }
    }
}
