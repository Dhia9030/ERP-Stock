using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebOrder.Migrations
{
    /// <inheritdoc />
    public partial class updatelocaldatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempOrders",
                columns: table => new
                {
                    TempOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempOrders", x => x.TempOrderId);
                });

            migrationBuilder.CreateTable(
                name: "TempOrderProducts",
                columns: table => new
                {
                    TempOrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempOrderProducts", x => new { x.TempOrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_TempOrderProducts_TempOrders_TempOrderId",
                        column: x => x.TempOrderId,
                        principalTable: "TempOrders",
                        principalColumn: "TempOrderId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempOrderProducts");

            migrationBuilder.DropTable(
                name: "TempOrders");
        }
    }
}
