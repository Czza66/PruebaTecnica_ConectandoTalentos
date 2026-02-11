using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConectandoTalentos.Data.Migrations
{
    /// <inheritdoc />
    public partial class EdicionModeloProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "productos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "orden",
                table: "productos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "activo",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "orden",
                table: "productos");
        }
    }
}
