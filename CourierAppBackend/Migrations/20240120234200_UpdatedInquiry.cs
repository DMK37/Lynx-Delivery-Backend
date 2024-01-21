using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourierAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedInquiry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveringCompany",
                table: "Inquiries",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveringCompany",
                table: "Inquiries");
        }
    }
}
