using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppAuthorization.Migrations
{
    /// <inheritdoc />
    public partial class TokenFamilyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2775b38f-d7dc-4322-86bd-4bab220dca5f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("475f9594-0dff-4efc-ba88-f78b7ec59985"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dd5d93fd-3175-471d-99ba-3d1e4c4ef3ba"));

            migrationBuilder.AddColumn<string>(
                name: "TokenRootId",
                table: "RefreshTokens",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("02c93e3a-1494-454c-b02d-eee79f42a553"), "fefeee2f-9f04-41e3-b1f5-2d713214271d", "User", "USER" },
                    { new Guid("75a885e4-bd9b-4b97-ab0b-fabd79f2ecc3"), "88f142c3-e639-4143-9d11-37fd40c048c2", "Moderator", "MODERATOR" },
                    { new Guid("9e42f9af-c1b7-40b6-ba8c-7e9ad89b6aa0"), "dacd6cbc-af11-4a85-8942-d85a70c5af68", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("02c93e3a-1494-454c-b02d-eee79f42a553"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("75a885e4-bd9b-4b97-ab0b-fabd79f2ecc3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e42f9af-c1b7-40b6-ba8c-7e9ad89b6aa0"));

            migrationBuilder.DropColumn(
                name: "TokenRootId",
                table: "RefreshTokens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2775b38f-d7dc-4322-86bd-4bab220dca5f"), "cf0dcd89-73b5-4c5f-8509-d38ad5ddacc6", "Admin", "ADMIN" },
                    { new Guid("475f9594-0dff-4efc-ba88-f78b7ec59985"), "b846eba8-43c2-4729-9a43-114e50ca2bbf", "User", "USER" },
                    { new Guid("dd5d93fd-3175-471d-99ba-3d1e4c4ef3ba"), "42d563b0-90e7-4dcf-8088-fc61a809df30", "Moderator", "MODERATOR" }
                });
        }
    }
}
