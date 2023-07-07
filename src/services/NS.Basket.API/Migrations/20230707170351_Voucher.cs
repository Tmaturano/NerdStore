using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NS.Basket.API.Migrations
{
    /// <inheritdoc />
    public partial class Voucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "BasketClients",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DiscountType",
                table: "BasketClients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountValue",
                table: "BasketClients",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                table: "BasketClients",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VoucherAlreadyUsed",
                table: "BasketClients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VoucherCode",
                table: "BasketClients",
                type: "varchar(50)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "BasketClients");

            migrationBuilder.DropColumn(
                name: "DiscountType",
                table: "BasketClients");

            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "BasketClients");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "BasketClients");

            migrationBuilder.DropColumn(
                name: "VoucherAlreadyUsed",
                table: "BasketClients");

            migrationBuilder.DropColumn(
                name: "VoucherCode",
                table: "BasketClients");
        }
    }
}
