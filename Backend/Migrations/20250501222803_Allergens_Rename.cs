using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Allergens_Rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllergenComponent_Allergen_AllergensId",
                table: "AllergenComponent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Allergen",
                table: "Allergen");

            migrationBuilder.RenameTable(
                name: "Allergen",
                newName: "Allergens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allergens",
                table: "Allergens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllergenComponent_Allergens_AllergensId",
                table: "AllergenComponent",
                column: "AllergensId",
                principalTable: "Allergens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllergenComponent_Allergens_AllergensId",
                table: "AllergenComponent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Allergens",
                table: "Allergens");

            migrationBuilder.RenameTable(
                name: "Allergens",
                newName: "Allergen");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allergen",
                table: "Allergen",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllergenComponent_Allergen_AllergensId",
                table: "AllergenComponent",
                column: "AllergensId",
                principalTable: "Allergen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
