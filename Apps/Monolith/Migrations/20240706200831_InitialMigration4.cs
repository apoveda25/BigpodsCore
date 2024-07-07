using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bigpods.Monolith.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "inventory_outputs",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "utf8mb4_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "inventory_inputs",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "utf8mb4_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "inventories",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_outputs_ProductId",
                table: "inventory_outputs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_inputs_ProductId",
                table: "inventory_inputs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_ProductId",
                table: "inventories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_products_ProductId",
                table: "inventories",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_inputs_products_ProductId",
                table: "inventory_inputs",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_outputs_products_ProductId",
                table: "inventory_outputs",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_products_ProductId",
                table: "inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_inputs_products_ProductId",
                table: "inventory_inputs");

            migrationBuilder.DropForeignKey(
                name: "FK_inventory_outputs_products_ProductId",
                table: "inventory_outputs");

            migrationBuilder.DropIndex(
                name: "IX_inventory_outputs_ProductId",
                table: "inventory_outputs");

            migrationBuilder.DropIndex(
                name: "IX_inventory_inputs_ProductId",
                table: "inventory_inputs");

            migrationBuilder.DropIndex(
                name: "IX_inventories_ProductId",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "inventory_outputs");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "inventory_inputs");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "inventories");
        }
    }
}
