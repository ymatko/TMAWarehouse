using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMAWWarehouse.Services.TMARequests.Migrations
{
    /// <inheritdoc />
    public partial class deleteTMARequestRow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TMARequestRows");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TMARequestRows",
                columns: table => new
                {
                    RequestRowID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    PriceWithoutVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitOfMeasurement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMARequestRows", x => x.RequestRowID);
                    table.ForeignKey(
                        name: "FK_TMARequestRows_TMARequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "TMARequests",
                        principalColumn: "RequestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TMARequestRows_RequestID",
                table: "TMARequestRows",
                column: "RequestID");
        }
    }
}
