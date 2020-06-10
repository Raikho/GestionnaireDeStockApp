using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Reference = table.Column<string>(nullable: false),
                Name = table.Column<string>(nullable: true),
                Price = table.Column<double>(nullable: false),
                Quantity = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Reference);
            });

            migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Username = table.Column<string>(nullable: false),
                Password = table.Column<string>(nullable: true),
                Name = table.Column<string>(nullable: true),
                Surname = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Username);
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
