using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppAuthorization.Migrations
{
    /// <inheritdoc />
    public partial class SeedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6e07dbd9-a0d7-4714-a093-ad754c9b07b8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("940d5d0d-b327-45a2-a68b-88d92e62516d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a354e16e-b843-48c2-b554-d6d443c2c0d1"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("910ab790-23f1-4fe2-88f4-9212df1988bb"), "04c11f81-333f-4077-b8d4-8f69a19eeab2", "Moderator", "MODERATOR" },
                    { new Guid("c228e90a-e2ca-4e2c-b82c-ba2cf3664ba9"), "50106a3c-1017-47c2-87a3-98d25a3ca18f", "Admin", "ADMIN" },
                    { new Guid("ea8028b8-809c-4283-be40-807981b5f7b7"), "daa06ea5-5eb0-4424-bc3c-c3929db63b8e", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("910ab790-23f1-4fe2-88f4-9212df1988bb"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c228e90a-e2ca-4e2c-b82c-ba2cf3664ba9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ea8028b8-809c-4283-be40-807981b5f7b7"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6e07dbd9-a0d7-4714-a093-ad754c9b07b8"), null, "User", "USER" },
                    { new Guid("940d5d0d-b327-45a2-a68b-88d92e62516d"), null, "Moderator", "MODERATOR" },
                    { new Guid("a354e16e-b843-48c2-b554-d6d443c2c0d1"), null, "Admin", "ADMIN" }
                });
        }
    }
}
