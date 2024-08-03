using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddShoppingCartAndCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCart",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "ShoppingCarts");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdd",
                table: "ShoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartCartId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_ShoppingCarts_ShoppingCartCartId",
                        column: x => x.ShoppingCartCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "CartId");
                });

           /* migrationBuilder.CreateIndex(
                name: "IX_Appointments_VehicleId",
                table: "Appointments",
                column: "VehicleId");*/

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ShoppingCartCartId",
                table: "CartItems",
                column: "ShoppingCartCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropColumn(
                name: "DateAdd",
                table: "ShoppingCarts");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCart",
                table: "ShoppingCarts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "ShoppingCarts",
                type: "int",
                nullable: true);
        }
    }
}
