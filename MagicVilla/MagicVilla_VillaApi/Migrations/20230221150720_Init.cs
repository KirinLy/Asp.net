using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_VillaApi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Villas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "CreatedDate", "Description", "ImageUrl", "Location", "Name", "Price", "Rating", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 2, 21, 22, 7, 20, 164, DateTimeKind.Local).AddTicks(8467), "Villa 1 Description", "Villa 1 ImageUrl", "Villa 1 Location", "Villa 1", 100, 1, new DateTime(2023, 2, 21, 22, 7, 20, 164, DateTimeKind.Local).AddTicks(8494) },
                    { 2, new DateTime(2023, 2, 21, 22, 7, 20, 164, DateTimeKind.Local).AddTicks(8500), "Villa 2 Description", "Villa 2 ImageUrl", "Villa 2 Location", "Villa 2", 200, 2, new DateTime(2023, 2, 21, 22, 7, 20, 164, DateTimeKind.Local).AddTicks(8502) },
                    { 3, new DateTime(2023, 2, 21, 22, 7, 20, 164, DateTimeKind.Local).AddTicks(8504), "Villa 3 Description", "Villa 3 ImageUrl", "Villa 3 Location", "Villa 3", 300, 3, new DateTime(2023, 2, 21, 22, 7, 20, 164, DateTimeKind.Local).AddTicks(8505) },
                    { 4, new DateTime(2023, 2, 21, 22, 7, 20, 164, DateTimeKind.Local).AddTicks(8508), "Villa 4 Description", "Villa 4 ImageUrl", "Villa 4 Location", "Villa 4", 400, 4, new DateTime(2023, 2, 21, 22, 7, 20, 164, DateTimeKind.Local).AddTicks(8511) },
                    { 5, new DateTime(2023, 2, 21, 22, 7, 20, 164, DateTimeKind.Local).AddTicks(8512), "Villa 5 Description", "Villa 5 ImageUrl", "Villa 5 Location", "Villa 5", 500, 5, new DateTime(2023, 2, 21, 22, 7, 20, 164, DateTimeKind.Local).AddTicks(8514) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Villas");
        }
    }
}
