using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationsBetweenIngredientJunctionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedOrderProductIngredients_OrderProducts_OrderProductId",
                table: "ModifiedOrderProductIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Discounts_DiscountId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Prices_DiscountId",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Prices");

            migrationBuilder.AddColumn<int>(
                name: "IngredientId",
                table: "ProductIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OrderProductId",
                table: "ModifiedOrderProductIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredients_IngredientId",
                table: "ProductIngredients",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedOrderProductIngredients_OrderProducts_OrderProductId",
                table: "ModifiedOrderProductIngredients",
                column: "OrderProductId",
                principalTable: "OrderProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredients_Ingredients_IngredientId",
                table: "ProductIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModifiedOrderProductIngredients_OrderProducts_OrderProductId",
                table: "ModifiedOrderProductIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredients_Ingredients_IngredientId",
                table: "ProductIngredients");

            migrationBuilder.DropIndex(
                name: "IX_ProductIngredients_IngredientId",
                table: "ProductIngredients");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "ProductIngredients");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Prices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OrderProductId",
                table: "ModifiedOrderProductIngredients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_DiscountId",
                table: "Prices",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModifiedOrderProductIngredients_OrderProducts_OrderProductId",
                table: "ModifiedOrderProductIngredients",
                column: "OrderProductId",
                principalTable: "OrderProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Discounts_DiscountId",
                table: "Prices",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
