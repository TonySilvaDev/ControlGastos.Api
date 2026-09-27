using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlGastos.Api.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablaTipoOperacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoOperacionId",
                table: "Gastos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipoOperacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoOperacion", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gastos_TipoOperacionId",
                table: "Gastos",
                column: "TipoOperacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gastos_TipoOperacion_TipoOperacionId",
                table: "Gastos",
                column: "TipoOperacionId",
                principalTable: "TipoOperacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gastos_TipoOperacion_TipoOperacionId",
                table: "Gastos");

            migrationBuilder.DropTable(
                name: "TipoOperacion");

            migrationBuilder.DropIndex(
                name: "IX_Gastos_TipoOperacionId",
                table: "Gastos");

            migrationBuilder.DropColumn(
                name: "TipoOperacionId",
                table: "Gastos");
        }
    }
}
