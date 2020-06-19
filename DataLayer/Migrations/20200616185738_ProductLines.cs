using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ProductLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductLine",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketRef = table.Column<string>(nullable: true),
                    ProductReference = table.Column<string>(nullable: true),
                    Quantity = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLine", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductLine_Products_ProductReference",
                        column: x => x.ProductReference,
                        principalTable: "Products",
                        principalColumn: "Reference",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductLine_Tickets_TicketRef",
                        column: x => x.TicketRef,
                        principalTable: "Tickets",
                        principalColumn: "TicketRef",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductLine_ProductReference",
                table: "ProductLine",
                column: "ProductReference");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLine_TicketRef",
                table: "ProductLine",
                column: "TicketRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductLine");
        }
    }
}
