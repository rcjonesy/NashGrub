using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NashGrub.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hashtags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BusinessName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hashtags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HashtagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Hashtags_HashtagId",
                        column: x => x.HashtagId,
                        principalTable: "Hashtags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Hashtags",
                columns: new[] { "Id", "BusinessName" },
                values: new object[,]
                {
                    { 1, "3rd and Lindsley" },
                    { 2, "Hattie B's Hot Chicken" },
                    { 3, "Loveless Cafe" },
                    { 4, "Pancake Pantry" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "DateCreated", "HashtagId", "Message" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 25, 10, 54, 40, 101, DateTimeKind.Local).AddTicks(2777), 1, "The service was attentive and friendly, adding to the overall wonderful dining experience." },
                    { 2, new DateTime(2024, 4, 25, 10, 54, 40, 101, DateTimeKind.Local).AddTicks(2833), 2, "he casual atmosphere makes it a great spot for a laid-back meal with family or friends. Just be prepared to leave with a satisfied belly and a newfound love for barbecue!" },
                    { 3, new DateTime(2024, 4, 25, 10, 54, 40, 101, DateTimeKind.Local).AddTicks(2835), 3, "The tranquil ambiance adds to the overall dining experience, making it a great place to unwind after a long day." },
                    { 4, new DateTime(2024, 4, 25, 10, 54, 40, 101, DateTimeKind.Local).AddTicks(2837), 4, "The staff is always friendly and accommodating, adding to the welcoming atmosphere of the restaurant." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HashtagId",
                table: "Reviews",
                column: "HashtagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Hashtags");
        }
    }
}
