using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourierAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class ModelsChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Inquiries_InquiryID",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_InquiryID",
                table: "Offers");

            migrationBuilder.AddColumn<bool>(
                name: "HighPriority",
                table: "Offers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HighPriority",
                table: "Inquiries",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighPriority",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "HighPriority",
                table: "Inquiries");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_InquiryID",
                table: "Offers",
                column: "InquiryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Inquiries_InquiryID",
                table: "Offers",
                column: "InquiryID",
                principalTable: "Inquiries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
