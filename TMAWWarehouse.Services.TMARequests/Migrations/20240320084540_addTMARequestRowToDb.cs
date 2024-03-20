using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMAWWarehouse.Services.TMARequests.Migrations
{
    /// <inheritdoc />
    public partial class addTMARequestRowToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "TMARequests");

            migrationBuilder.DropColumn(
                name: "ItemID",
                table: "TMARequests");

            migrationBuilder.DropColumn(
                name: "PriceWithoutVAT",
                table: "TMARequests");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "TMARequests");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurement",
                table: "TMARequests");

            migrationBuilder.CreateTable(
                name: "TMARequestRows",
                columns: table => new
                {
                    RequestRowID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    UnitOfMeasurement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PriceWithoutVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TMARequestRows");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "TMARequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemID",
                table: "TMARequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceWithoutVAT",
                table: "TMARequests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "TMARequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurement",
                table: "TMARequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
