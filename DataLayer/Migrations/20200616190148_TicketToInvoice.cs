using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DataLayer.Migrations
{
    public partial class TicketToInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLine_Tickets_TicketRef",
                table: "ProductLine");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    TicketRef = table.Column<string>(nullable: false),
                    NameSeller = table.Column<string>(nullable: true),
                    Recipe = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    PaymentMethod = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.TicketRef);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLine_Invoice_TicketRef",
                table: "ProductLine",
                column: "TicketRef",
                principalTable: "Invoices",
                principalColumn: "TicketRef",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLine_Invoice_TicketRef",
                table: "ProductLine");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketRef = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    NameSeller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recipe = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketRef);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLine_Tickets_TicketRef",
                table: "ProductLine",
                column: "TicketRef",
                principalTable: "Tickets",
                principalColumn: "TicketRef",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
