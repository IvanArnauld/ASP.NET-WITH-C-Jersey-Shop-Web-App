using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExercisesAPI.Migrations
{
    public partial class trays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalCalories = table.Column<int>(type: "int", nullable: false),
                    TotalCarbs = table.Column<int>(type: "int", nullable: false),
                    TotalCholesterol = table.Column<int>(type: "int", nullable: false),
                    TotalFat = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    TotalFibre = table.Column<int>(type: "int", nullable: false),
                    TotalProtein = table.Column<int>(type: "int", nullable: false),
                    TotalSalt = table.Column<int>(type: "int", nullable: false),
                    Timer = table.Column<byte[]>(type: "timestamp", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrayItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrayId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Timer = table.Column<byte[]>(type: "timestamp", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrayItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrayItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrayItems_Trays_TrayId",
                        column: x => x.TrayId,
                        principalTable: "Trays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrayItems_MenuItemId",
                table: "TrayItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TrayItems_TrayId",
                table: "TrayItems",
                column: "TrayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrayItems");

            migrationBuilder.DropTable(
                name: "Trays");
        }
    }
}
