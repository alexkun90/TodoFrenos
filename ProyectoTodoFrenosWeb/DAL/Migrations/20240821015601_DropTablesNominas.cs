using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class DropTablesNominas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistences");

            migrationBuilder.DropTable(
                name: "DetallesNominas");

            migrationBuilder.DropTable(
                name: "DisabilityEmployees");

            migrationBuilder.DropTable(
                name: "HistoricoPlanilla");

            migrationBuilder.DropTable(
                name: "HistoryPlayrolls");

            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Nominas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asistences",
                columns: table => new
                {
                    AsistenciaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<long>(type: "bigint", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraLlegada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraSalida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoAsistencia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistences", x => x.AsistenciaId);
                    table.ForeignKey(
                        name: "FK_Asistences_Employees_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisabilityEmployees",
                columns: table => new
                {
                    IncapacidadId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<long>(type: "bigint", nullable: false),
                    DiasIncapacitado = table.Column<int>(type: "int", nullable: false),
                    Entidad = table.Column<int>(type: "int", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisabilityEmployees", x => x.IncapacidadId);
                    table.ForeignKey(
                        name: "FK_DisabilityEmployees_Employees_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apellido1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Puesto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoPlanilla",
                columns: table => new
                {
                    HistoricoPlanillaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanillaEmpleadoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoPlanilla", x => x.HistoricoPlanillaId);
                    table.ForeignKey(
                        name: "FK_HistoricoPlanilla_PlanillaEmpleados_PlanillaEmpleadoId",
                        column: x => x.PlanillaEmpleadoId,
                        principalTable: "PlanillaEmpleados",
                        principalColumn: "PlanillaEmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryPlayrolls",
                columns: table => new
                {
                    HistoricoNominaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NominaId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryPlayrolls", x => x.HistoricoNominaId);
                    table.ForeignKey(
                        name: "FK_HistoryPlayrolls_Playrolls_NominaId",
                        column: x => x.NominaId,
                        principalTable: "Playrolls",
                        principalColumn: "NominaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nominas",
                columns: table => new
                {
                    NominaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFinaliza = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalNomina = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nominas", x => x.NominaId);
                });

            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    VacationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.VacationId);
                    table.ForeignKey(
                        name: "FK_Vacations_Employees_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesNominas",
                columns: table => new
                {
                    DetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true),
                    NominaId = table.Column<int>(type: "int", nullable: true),
                    CantidadHoras = table.Column<int>(type: "int", nullable: true),
                    CantidadHorasExtra = table.Column<int>(type: "int", nullable: true),
                    Ccss = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Diario = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Hora = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HorasExtra = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Mensual = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Pago = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SalarioBase = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Semanal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Vales = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
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
                name: "IX_Asistences_EmpleadoId",
                table: "Asistences",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesNominas_EmpleadoId",
                table: "DetallesNominas",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesNominas_NominaId",
                table: "DetallesNominas",
                column: "NominaId");

            migrationBuilder.CreateIndex(
                name: "IX_DisabilityEmployees_EmpleadoId",
                table: "DisabilityEmployees",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoPlanilla_PlanillaEmpleadoId",
                table: "HistoricoPlanilla",
                column: "PlanillaEmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryPlayrolls_NominaId",
                table: "HistoryPlayrolls",
                column: "NominaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_EmpleadoId",
                table: "Vacations",
                column: "EmpleadoId");
        }
    }
}
