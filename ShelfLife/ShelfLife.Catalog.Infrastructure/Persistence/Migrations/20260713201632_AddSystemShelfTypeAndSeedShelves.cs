using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShelfLife.Catalog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemShelfTypeAndSeedShelves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SystemShelfType",
                table: "shelves",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "shelves",
                columns: new[] { "Id", "IsSystemShelf", "Name", "SystemShelfType" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), true, "To Read", "ToRead" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), true, "Reading", "Reading" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), true, "Finished", "Finished" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), true, "DNF", "Dnfs" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "shelves",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "shelves",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "shelves",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "shelves",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DropColumn(
                name: "SystemShelfType",
                table: "shelves");
        }
    }
}
