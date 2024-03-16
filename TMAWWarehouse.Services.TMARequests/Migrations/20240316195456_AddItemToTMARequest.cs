using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMAWWarehouse.Services.TMARequests.Migrations
{
    /// <inheritdoc />
    public partial class AddItemToTMARequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "TMARequests");

            migrationBuilder.AddColumn<int>(
                name: "ItemID",
                table: "TMARequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemID",
                table: "TMARequests");

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "TMARequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
