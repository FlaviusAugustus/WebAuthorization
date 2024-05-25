using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppAuthorization.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("3a176cd2-4dfc-4cb7-8848-16187c2ca8bd"), "438a2999-3a99-49ee-b129-3b2f0aef6434", "Moderator", "MODERATOR" },
                    { new Guid("4ba9137a-5225-48ad-a0d6-9c2b0c519f3e"), "e23cf5b5-52fd-4a31-988c-367f6ede2f12", "Admin", "ADMIN" },
                    { new Guid("dadfc0f6-3ebe-4956-9550-cfa85afdeb7c"), "09731215-d612-4339-9509-0ded10c1a364", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3a176cd2-4dfc-4cb7-8848-16187c2ca8bd"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4ba9137a-5225-48ad-a0d6-9c2b0c519f3e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dadfc0f6-3ebe-4956-9550-cfa85afdeb7c"));

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
    }
}
