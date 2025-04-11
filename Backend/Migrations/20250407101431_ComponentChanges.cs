using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class ComponentChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentChildren_Components_ChildId",
                table: "ComponentChildren");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentChildren_Components_ParentId",
                table: "ComponentChildren");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentChildren",
                table: "ComponentChildren");

            migrationBuilder.DropColumn(
                name: "Vegan",
                table: "Components");

            migrationBuilder.RenameTable(
                name: "ComponentChildren",
                newName: "ComponentChildPolicies");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentChildren_ParentId",
                table: "ComponentChildPolicies",
                newName: "IX_ComponentChildPolicies_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentChildren_ChildId",
                table: "ComponentChildPolicies",
                newName: "IX_ComponentChildPolicies_ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentChildPolicies",
                table: "ComponentChildPolicies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentChildPolicies_Components_ChildId",
                table: "ComponentChildPolicies",
                column: "ChildId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentChildPolicies_Components_ParentId",
                table: "ComponentChildPolicies",
                column: "ParentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentChildPolicies_Components_ChildId",
                table: "ComponentChildPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentChildPolicies_Components_ParentId",
                table: "ComponentChildPolicies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentChildPolicies",
                table: "ComponentChildPolicies");

            migrationBuilder.RenameTable(
                name: "ComponentChildPolicies",
                newName: "ComponentChildren");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentChildPolicies_ParentId",
                table: "ComponentChildren",
                newName: "IX_ComponentChildren_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentChildPolicies_ChildId",
                table: "ComponentChildren",
                newName: "IX_ComponentChildren_ChildId");

            migrationBuilder.AddColumn<bool>(
                name: "Vegan",
                table: "Components",
                type: "bit",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentChildren",
                table: "ComponentChildren",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentChildren_Components_ChildId",
                table: "ComponentChildren",
                column: "ChildId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentChildren_Components_ParentId",
                table: "ComponentChildren",
                column: "ParentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
