using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourierAppBackend.Migrations
{
    /// <inheritdoc />
    public partial class ModelChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price_Value",
                table: "Offers",
                newName: "Price_WeightFee");

            migrationBuilder.RenameColumn(
                name: "Price_Taxes",
                table: "Offers",
                newName: "Price_SizeFee");

            migrationBuilder.RenameColumn(
                name: "Price_Fees",
                table: "Offers",
                newName: "Price_PriorityFee");

            migrationBuilder.AddColumn<decimal>(
                name: "Price_BaseDeliveryPrice",
                table: "Offers",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Price_Currency",
                table: "Offers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price_DeliveryAtWeekendFee",
                table: "Offers",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Package_Weight",
                table: "Inquiries",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price_BaseDeliveryPrice",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Price_Currency",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Price_DeliveryAtWeekendFee",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "Price_WeightFee",
                table: "Offers",
                newName: "Price_Value");

            migrationBuilder.RenameColumn(
                name: "Price_SizeFee",
                table: "Offers",
                newName: "Price_Taxes");

            migrationBuilder.RenameColumn(
                name: "Price_PriorityFee",
                table: "Offers",
                newName: "Price_Fees");

            migrationBuilder.AlterColumn<float>(
                name: "Package_Weight",
                table: "Inquiries",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
