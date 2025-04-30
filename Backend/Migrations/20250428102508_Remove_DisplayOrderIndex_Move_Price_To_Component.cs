using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Remove_DisplayOrderIndex_Move_Price_To_Component : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Prices_PriceId",
                table: "Components");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Components_PriceId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "DisplayOrderIndex",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "Components");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Components",
                type: "decimal(8,4)",
                precision: 8,
                scale: 4,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Components");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrderIndex",
                table: "Components",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "Components",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasePrice = table.Column<decimal>(type: "decimal(8,4)", precision: 8, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_PriceId",
                table: "Components",
                column: "PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Prices_PriceId",
                table: "Components",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
