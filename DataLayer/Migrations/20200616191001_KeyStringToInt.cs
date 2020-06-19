using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class KeyStringToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLine_Products_ProductReference",
                table: "ProductLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLine_Invoice_TicketRef",
                table: "ProductLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductLine",
                table: "ProductLine");

            migrationBuilder.DropIndex(
                name: "IX_ProductLine_ProductReference",
                table: "ProductLine");

            migrationBuilder.DropIndex(
                name: "IX_ProductLine_TicketRef",
                table: "ProductLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ProductLine");

            migrationBuilder.DropColumn(
                name: "ProductReference",
                table: "ProductLine");

            migrationBuilder.DropColumn(
                name: "TicketRef",
                table: "ProductLine");

            migrationBuilder.AlterColumn<string>(
                name: "Reference",
                table: "Products",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Products",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProductLineId",
                table: "ProductLine",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductLine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketInvoiceId",
                table: "ProductLine",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TicketRef",
                table: "Invoice",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Invoice",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductLine",
                table: "ProductLine",
                column: "ProductLineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLine_ProductId",
                table: "ProductLine",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLine_TicketInvoiceId",
                table: "ProductLine",
                column: "TicketInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLine_Products_ProductId",
                table: "ProductLine",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLine_Invoice_TicketInvoiceId",
                table: "ProductLine",
                column: "TicketInvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLine_Products_ProductId",
                table: "ProductLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLine_Invoice_TicketInvoiceId",
                table: "ProductLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductLine",
                table: "ProductLine");

            migrationBuilder.DropIndex(
                name: "IX_ProductLine_ProductId",
                table: "ProductLine");

            migrationBuilder.DropIndex(
                name: "IX_ProductLine_TicketInvoiceId",
                table: "ProductLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductLineId",
                table: "ProductLine");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductLine");

            migrationBuilder.DropColumn(
                name: "TicketInvoiceId",
                table: "ProductLine");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Invoice");

            migrationBuilder.AlterColumn<string>(
                name: "Reference",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ProductLine",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ProductReference",
                table: "ProductLine",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TicketRef",
                table: "ProductLine",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TicketRef",
                table: "Invoice",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Reference");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductLine",
                table: "ProductLine",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "TicketRef");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLine_ProductReference",
                table: "ProductLine",
                column: "ProductReference");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLine_TicketRef",
                table: "ProductLine",
                column: "TicketRef");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLine_Products_ProductReference",
                table: "ProductLine",
                column: "ProductReference",
                principalTable: "Products",
                principalColumn: "Reference",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLine_Invoice_TicketRef",
                table: "ProductLine",
                column: "TicketRef",
                principalTable: "Invoice",
                principalColumn: "TicketRef",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
