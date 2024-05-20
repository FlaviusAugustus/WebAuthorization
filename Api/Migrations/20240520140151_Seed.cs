using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppAuthorization.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
