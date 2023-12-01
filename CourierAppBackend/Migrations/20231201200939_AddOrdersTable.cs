using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourierAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInfo_Addresses_AddressId",
                table: "CustomerInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_CustomerInfo_CustomerInfoId",
                table: "Offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerInfo",
                table: "CustomerInfo");

            migrationBuilder.RenameTable(
                name: "CustomerInfo",
                newName: "CustomerInfos");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerInfo_AddressId",
                table: "CustomerInfos",
                newName: "IX_CustomerInfos_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerInfos",
                table: "CustomerInfos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OfferID = table.Column<int>(type: "integer", nullable: false),
                    OfferId = table.Column<int>(type: "integer", nullable: false),
                    OrderStatus = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CourierName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OfferId",
                table: "Orders",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInfos_Addresses_AddressId",
                table: "CustomerInfos",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_CustomerInfos_CustomerInfoId",
                table: "Offers",
                column: "CustomerInfoId",
                principalTable: "CustomerInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInfos_Addresses_AddressId",
                table: "CustomerInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_CustomerInfos_CustomerInfoId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerInfos",
                table: "CustomerInfos");

            migrationBuilder.RenameTable(
                name: "CustomerInfos",
                newName: "CustomerInfo");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerInfos_AddressId",
                table: "CustomerInfo",
                newName: "IX_CustomerInfo_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerInfo",
                table: "CustomerInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInfo_Addresses_AddressId",
                table: "CustomerInfo",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_CustomerInfo_CustomerInfoId",
                table: "Offers",
                column: "CustomerInfoId",
                principalTable: "CustomerInfo",
                principalColumn: "Id");
        }
    }
}
