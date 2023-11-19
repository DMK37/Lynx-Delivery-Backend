using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourierAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class Checkpoint2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Addresses_DestinationAddressID",
                table: "Inquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Addresses_SourceAddressID",
                table: "Inquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Users_UserID",
                table: "Inquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Addresses_DestinationAddressID",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Addresses_SourceAddressID",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Inquiries_UserID",
                table: "Inquiries");

            migrationBuilder.RenameColumn(
                name: "SourceAddressID",
                table: "Offers",
                newName: "SourceAddressId");

            migrationBuilder.RenameColumn(
                name: "DestinationAddressID",
                table: "Offers",
                newName: "DestinationAddressId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Offers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_SourceAddressID",
                table: "Offers",
                newName: "IX_Offers_SourceAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_DestinationAddressID",
                table: "Offers",
                newName: "IX_Offers_DestinationAddressId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Inquiries",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "SourceAddressID",
                table: "Inquiries",
                newName: "SourceAddressId");

            migrationBuilder.RenameColumn(
                name: "DestinationAddressID",
                table: "Inquiries",
                newName: "DestinationAddressId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Inquiries",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Inquiries_SourceAddressID",
                table: "Inquiries",
                newName: "IX_Inquiries_SourceAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Inquiries_DestinationAddressID",
                table: "Inquiries",
                newName: "IX_Inquiries_DestinationAddressId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Addresses",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Inquiries",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourierCompanyName",
                table: "Inquiries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Inquiries",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Inquiries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UsersInfos",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: true),
                    DefaultSourceAddressId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInfos", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UsersInfos_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersInfos_Addresses_DefaultSourceAddressId",
                        column: x => x.DefaultSourceAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersInfos_AddressId",
                table: "UsersInfos",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInfos_DefaultSourceAddressId",
                table: "UsersInfos",
                column: "DefaultSourceAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Addresses_DestinationAddressId",
                table: "Inquiries",
                column: "DestinationAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Addresses_SourceAddressId",
                table: "Inquiries",
                column: "SourceAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Addresses_DestinationAddressId",
                table: "Inquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Addresses_SourceAddressId",
                table: "Inquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Addresses_DestinationAddressId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Addresses_SourceAddressId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "UsersInfos");

            migrationBuilder.DropColumn(
                name: "CourierCompanyName",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Inquiries");

            migrationBuilder.RenameColumn(
                name: "SourceAddressId",
                table: "Offers",
                newName: "SourceAddressID");

            migrationBuilder.RenameColumn(
                name: "DestinationAddressId",
                table: "Offers",
                newName: "DestinationAddressID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Offers",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_SourceAddressId",
                table: "Offers",
                newName: "IX_Offers_SourceAddressID");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_DestinationAddressId",
                table: "Offers",
                newName: "IX_Offers_DestinationAddressID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Inquiries",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "SourceAddressId",
                table: "Inquiries",
                newName: "SourceAddressID");

            migrationBuilder.RenameColumn(
                name: "DestinationAddressId",
                table: "Inquiries",
                newName: "DestinationAddressID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Inquiries",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Inquiries_SourceAddressId",
                table: "Inquiries",
                newName: "IX_Inquiries_SourceAddressID");

            migrationBuilder.RenameIndex(
                name: "IX_Inquiries_DestinationAddressId",
                table: "Inquiries",
                newName: "IX_Inquiries_DestinationAddressID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Addresses",
                newName: "ID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Inquiries",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AddressID = table.Column<int>(type: "integer", nullable: false),
                    DefaultSourceAddressID = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Addresses_DefaultSourceAddressID",
                        column: x => x.DefaultSourceAddressID,
                        principalTable: "Addresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_UserID",
                table: "Inquiries",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressID",
                table: "Users",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DefaultSourceAddressID",
                table: "Users",
                column: "DefaultSourceAddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Addresses_DestinationAddressID",
                table: "Inquiries",
                column: "DestinationAddressID",
                principalTable: "Addresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Addresses_SourceAddressID",
                table: "Inquiries",
                column: "SourceAddressID",
                principalTable: "Addresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Users_UserID",
                table: "Inquiries",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Addresses_DestinationAddressID",
                table: "Offers",
                column: "DestinationAddressID",
                principalTable: "Addresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Addresses_SourceAddressID",
                table: "Offers",
                column: "SourceAddressID",
                principalTable: "Addresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
