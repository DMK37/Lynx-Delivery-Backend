using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourierAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class CustomerInfoChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_CustomerInfos_CustomerInfoId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "CustomerInfos");

            migrationBuilder.RenameColumn(
                name: "CustomerInfoId",
                table: "Offers",
                newName: "CustomerInfo_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_CustomerInfoId",
                table: "Offers",
                newName: "IX_Offers_CustomerInfo_AddressId");

            migrationBuilder.AddColumn<string>(
                name: "CustomerInfo_CompanyName",
                table: "Offers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerInfo_Email",
                table: "Offers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerInfo_FirstName",
                table: "Offers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerInfo_LastName",
                table: "Offers",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Addresses_CustomerInfo_AddressId",
                table: "Offers",
                column: "CustomerInfo_AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Addresses_CustomerInfo_AddressId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "CustomerInfo_CompanyName",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "CustomerInfo_Email",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "CustomerInfo_FirstName",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "CustomerInfo_LastName",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "CustomerInfo_AddressId",
                table: "Offers",
                newName: "CustomerInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_CustomerInfo_AddressId",
                table: "Offers",
                newName: "IX_Offers_CustomerInfoId");

            migrationBuilder.CreateTable(
                name: "CustomerInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AddressId = table.Column<int>(type: "integer", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerInfos_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInfos_AddressId",
                table: "CustomerInfos",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_CustomerInfos_CustomerInfoId",
                table: "Offers",
                column: "CustomerInfoId",
                principalTable: "CustomerInfos",
                principalColumn: "Id");
        }
    }
}
