using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConectandoTalentos.Data.Migrations
{
    /// <inheritdoc />
    public partial class EdicionProductos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "productos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "imgProducto",
                table: "productos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "productos");

            migrationBuilder.DropColumn(
                name: "imgProducto",
                table: "productos");
        }
    }
}
