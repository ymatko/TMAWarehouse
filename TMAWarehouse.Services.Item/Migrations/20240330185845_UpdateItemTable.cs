using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMAWarehouse.Services.Item.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Items",
                newName: "PhotoUrl");

            migrationBuilder.AddColumn<string>(
                name: "PhotoLocalPach",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoLocalPach",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Items",
                newName: "Photo");
        }
    }
}
