using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthTea.Migrations
{
    /// <inheritdoc />
    public partial class namingconventionchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "OrderItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartId",
                table: "CartItems",
                newName: "CartId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "CartItems",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderItems",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "CartItems",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartItems",
                newName: "ShoppingCartId");
        }
    }
}
