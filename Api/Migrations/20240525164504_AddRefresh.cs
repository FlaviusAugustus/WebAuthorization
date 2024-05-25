using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppAuthorization.Migrations
{
    /// <inheritdoc />
    public partial class AddRefresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Revoked = table.Column<bool>(type: "INTEGER", nullable: false),
                    TokenHash = table.Column<string>(type: "TEXT", nullable: false),
                    AccessTokenHash = table.Column<string>(type: "TEXT", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

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
    }
}
