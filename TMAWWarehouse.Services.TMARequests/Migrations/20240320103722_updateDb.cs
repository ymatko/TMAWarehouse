using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMAWWarehouse.Services.TMARequests.Migrations
{
    /// <inheritdoc />
    public partial class updateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
