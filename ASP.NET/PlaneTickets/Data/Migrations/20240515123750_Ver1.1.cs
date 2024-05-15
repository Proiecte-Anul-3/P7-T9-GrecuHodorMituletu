using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaneTickets.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ver11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Cart");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Cart",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "Cart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_TicketId",
                table: "Cart",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Ticket_TicketId",
                table: "Cart",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Ticket_TicketId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_TicketId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Cart");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Cart",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
