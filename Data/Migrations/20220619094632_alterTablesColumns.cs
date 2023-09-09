using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Consultation.Migrations
{
    public partial class alterTablesColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

           
            migrationBuilder.CreateTable(
                name: "ProductsConfirmation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Expired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsConfirmation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsConfirmation_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsConfirmation_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductPrice",
                columns: new[] { "Id", "Price", "Title" },
                values: new object[] { 1, 5.0, "Weekly" });

            migrationBuilder.InsertData(
                table: "ProductPrice",
                columns: new[] { "Id", "Price", "Title" },
                values: new object[] { 2, 15.0, "Monthly" });

            migrationBuilder.InsertData(
                table: "ProductPrice",
                columns: new[] { "Id", "Price", "Title" },
                values: new object[] { 3, 150.0, "Yearly" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_PriceId",
                table: "Products",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsConfirmation_ProductId",
                table: "ProductsConfirmation",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsConfirmation_UserId",
                table: "ProductsConfirmation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductPrice_PriceId",
                table: "Products",
                column: "PriceId",
                principalTable: "ProductPrice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductPrice_PriceId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductsConfirmation");

            migrationBuilder.DropIndex(
                name: "IX_Products_PriceId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "Products");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
