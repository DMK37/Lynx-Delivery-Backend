using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourierAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class TemporaryOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Package_Weight",
                table: "Inquiries",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.CreateTable(
                name: "TemporaryOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InquiryId = table.Column<int>(type: "integer", nullable: false),
                    Company = table.Column<string>(type: "text", nullable: false),
                    InquiryID = table.Column<string>(type: "text", nullable: false),
                    OfferID = table.Column<int>(type: "integer", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    ExpiringAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryOffers_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TemporaryOfferId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceItems_TemporaryOffers_TemporaryOfferId",
                        column: x => x.TemporaryOfferId,
                        principalTable: "TemporaryOffers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceItems_TemporaryOfferId",
                table: "PriceItems",
                column: "TemporaryOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryOffers_InquiryId",
                table: "TemporaryOffers",
                column: "InquiryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceItems");

            migrationBuilder.DropTable(
                name: "TemporaryOffers");

            migrationBuilder.AlterColumn<decimal>(
                name: "Package_Weight",
                table: "Inquiries",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
