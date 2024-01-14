using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourierAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class ModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Addresses_DestinationAddressId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Addresses_SourceAddressId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInfos_Addresses_AddressId",
                table: "UsersInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInfos_Addresses_DefaultSourceAddressId",
                table: "UsersInfos");

            migrationBuilder.DropIndex(
                name: "IX_Offers_DestinationAddressId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_SourceAddressId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "DeliveryAtWeekend",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "DestinationAddressId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "HighPriority",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "InquiryID",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Package_Height",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Package_Length",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Package_Weight",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Package_Width",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "SourceAddressId",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "PickupDate",
                table: "Offers",
                newName: "ExpireDate");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "Inquiries",
                newName: "OfferID");

            migrationBuilder.AlterColumn<int>(
                name: "DefaultSourceAddressId",
                table: "UsersInfos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "UsersInfos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price_Value",
                table: "Offers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price_Taxes",
                table: "Offers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price_FullPrice",
                table: "Offers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price_Fees",
                table: "Offers",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "CustomerInfoId",
                table: "Offers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "Offers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonOfRejection",
                table: "Offers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerInfo_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CustomerInfoId",
                table: "Offers",
                column: "CustomerInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_OfferID",
                table: "Inquiries",
                column: "OfferID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInfo_AddressId",
                table: "CustomerInfo",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Offers_OfferID",
                table: "Inquiries",
                column: "OfferID",
                principalTable: "Offers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_CustomerInfo_CustomerInfoId",
                table: "Offers",
                column: "CustomerInfoId",
                principalTable: "CustomerInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInfos_Addresses_AddressId",
                table: "UsersInfos",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInfos_Addresses_DefaultSourceAddressId",
                table: "UsersInfos",
                column: "DefaultSourceAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Offers_OfferID",
                table: "Inquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_CustomerInfo_CustomerInfoId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInfos_Addresses_AddressId",
                table: "UsersInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInfos_Addresses_DefaultSourceAddressId",
                table: "UsersInfos");

            migrationBuilder.DropTable(
                name: "CustomerInfo");

            migrationBuilder.DropIndex(
                name: "IX_Offers_CustomerInfoId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Inquiries_OfferID",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "CustomerInfoId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ReasonOfRejection",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "ExpireDate",
                table: "Offers",
                newName: "PickupDate");

            migrationBuilder.RenameColumn(
                name: "OfferID",
                table: "Inquiries",
                newName: "OfferId");

            migrationBuilder.AlterColumn<int>(
                name: "DefaultSourceAddressId",
                table: "UsersInfos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "UsersInfos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<float>(
                name: "Price_Value",
                table: "Offers",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<float>(
                name: "Price_Taxes",
                table: "Offers",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<float>(
                name: "Price_FullPrice",
                table: "Offers",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<float>(
                name: "Price_Fees",
                table: "Offers",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<bool>(
                name: "DeliveryAtWeekend",
                table: "Offers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Offers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DestinationAddressId",
                table: "Offers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HighPriority",
                table: "Offers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "InquiryID",
                table: "Offers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Package_Height",
                table: "Offers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Package_Length",
                table: "Offers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Package_Weight",
                table: "Offers",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Package_Width",
                table: "Offers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceAddressId",
                table: "Offers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_DestinationAddressId",
                table: "Offers",
                column: "DestinationAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_SourceAddressId",
                table: "Offers",
                column: "SourceAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Addresses_DestinationAddressId",
                table: "Offers",
                column: "DestinationAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Addresses_SourceAddressId",
                table: "Offers",
                column: "SourceAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInfos_Addresses_AddressId",
                table: "UsersInfos",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInfos_Addresses_DefaultSourceAddressId",
                table: "UsersInfos",
                column: "DefaultSourceAddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
