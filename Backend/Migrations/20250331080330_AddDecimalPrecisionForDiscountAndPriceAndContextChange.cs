using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddDecimalPrecisionForDiscountAndPriceAndContextChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Sales_SaleId",
                table: "Prices");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "Prices",
                newName: "DiscountId");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_SaleId",
                table: "Prices",
                newName: "IX_Prices_DiscountId");

            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "Prices",
                type: "decimal(8,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Multiplier = table.Column<decimal>(type: "decimal(8,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Discounts_DiscountId",
                table: "Prices",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Discounts_DiscountId",
                table: "Prices");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.RenameColumn(
                name: "DiscountId",
                table: "Prices",
                newName: "SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_DiscountId",
                table: "Prices",
                newName: "IX_Prices_SaleId");

            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "Prices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,4)");

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Multiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Sales_SaleId",
                table: "Prices",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
