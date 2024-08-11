using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class TablasNomina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Puesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nominas",
                columns: table => new
                {
                    NominaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFinaliza = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalNomina = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nominas", x => x.NominaId);
                });

            migrationBuilder.CreateTable(
                name: "DetallesNominas",
                columns: table => new
                {
                    DetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NominaId = table.Column<int>(type: "int", nullable: true),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true),
                    SalarioBase = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Semanal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Mensual = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Diario = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Hora = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CantidadHoras = table.Column<int>(type: "int", nullable: true),
                    HorasExtra = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CantidadHorasExtra = table.Column<int>(type: "int", nullable: true),
                    Ccss = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Vales = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Pago = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesNominas", x => x.DetalleId);
                    table.ForeignKey(
                        name: "FK_DetallesNominas_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DetallesNominas_Nominas_NominaId",
                        column: x => x.NominaId,
                        principalTable: "Nominas",
                        principalColumn: "NominaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesNominas_EmpleadoId",
                table: "DetallesNominas",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesNominas_NominaId",
                table: "DetallesNominas",
                column: "NominaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesNominas");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Nominas");
        }
    }
}
