using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Consultation.Migrations
{
    public partial class alterConsultationConfirmationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultatoionConfirmation");

            migrationBuilder.CreateTable(
                name: "ConsultationConfirmation",
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
                    table.PrimaryKey("PK_ConsultationConfirmation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultationConfirmation_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ConsultationConfirmation_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationConfirmation_ProductId",
                table: "ConsultationConfirmation",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationConfirmation_UserId",
                table: "ConsultationConfirmation",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultationConfirmation");

            migrationBuilder.CreateTable(
                name: "ConsultatoionConfirmation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Expired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultatoionConfirmation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultatoionConfirmation_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ConsultatoionConfirmation_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultatoionConfirmation_ProductId",
                table: "ConsultatoionConfirmation",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultatoionConfirmation_UserId",
                table: "ConsultatoionConfirmation",
                column: "UserId");
        }
    }
}
