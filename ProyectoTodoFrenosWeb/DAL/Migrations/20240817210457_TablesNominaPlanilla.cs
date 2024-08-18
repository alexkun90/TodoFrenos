using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class TablesNominaPlanilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpleadoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApellidoEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaContrato = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SalarioBase = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PlusesSalariales = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Puesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoCivil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactoEmergencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoEmpleado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpleadoId);
                });

            migrationBuilder.CreateTable(
                name: "Asistences",
                columns: table => new
                {
                    AsistenciaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<long>(type: "bigint", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraLlegada = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HoraSalida = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoAsistencia = table.Column<int>(type: "int", nullable: false),
                    EmployeeEmpleadoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistences", x => x.AsistenciaId);
                    table.ForeignKey(
                        name: "FK_Asistences_Employees_EmployeeEmpleadoId",
                        column: x => x.EmployeeEmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "EmpleadoId");
                });

            migrationBuilder.CreateTable(
                name: "Deducciones",
                columns: table => new
                {
                    DeduccionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    SalarioBruto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SEM = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IVM = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LPT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ImpuestoRenta = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalDeduccion = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deducciones", x => x.DeduccionId);
                    table.ForeignKey(
                        name: "FK_Deducciones_Employees_EmployeeId",
                        column: x => x.EmployeeId,
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
                    Entidad = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiasIncapacitado = table.Column<int>(type: "int", nullable: false),
                    EmployeeEmpleadoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisabilityEmployees", x => x.IncapacidadId);
                    table.ForeignKey(
                        name: "FK_DisabilityEmployees_Employees_EmployeeEmpleadoId",
                        column: x => x.EmployeeEmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "EmpleadoId");
                });

            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    VacationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<long>(type: "bigint", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeEmpleadoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.VacationId);
                    table.ForeignKey(
                        name: "FK_Vacations_Employees_EmployeeEmpleadoId",
                        column: x => x.EmployeeEmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "EmpleadoId");
                });

            migrationBuilder.CreateTable(
                name: "Playrolls",
                columns: table => new
                {
                    NominaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<long>(type: "bigint", nullable: false),
                    DeduccionId = table.Column<long>(type: "bigint", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalarioNeto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployeeEmpleadoId = table.Column<long>(type: "bigint", nullable: true),
                    DeduccionesDeduccionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playrolls", x => x.NominaId);
                    table.ForeignKey(
                        name: "FK_Playrolls_Deducciones_DeduccionesDeduccionId",
                        column: x => x.DeduccionesDeduccionId,
                        principalTable: "Deducciones",
                        principalColumn: "DeduccionId");
                    table.ForeignKey(
                        name: "FK_Playrolls_Employees_EmployeeEmpleadoId",
                        column: x => x.EmployeeEmpleadoId,
                        principalTable: "Employees",
                        principalColumn: "EmpleadoId");
                });

            migrationBuilder.CreateTable(
                name: "HistoryPlayrolls",
                columns: table => new
                {
                    HistoricoNominaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NominaId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayrollNominaId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryPlayrolls", x => x.HistoricoNominaId);
                    table.ForeignKey(
                        name: "FK_HistoryPlayrolls_Playrolls_PlayrollNominaId",
                        column: x => x.PlayrollNominaId,
                        principalTable: "Playrolls",
                        principalColumn: "NominaId");
                });

            migrationBuilder.CreateTable(
                name: "PlanillaEmpleados",
                columns: table => new
                {
                    PlanillaEmpleadoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NominaId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HorasTrabajadas = table.Column<int>(type: "int", nullable: true),
                    HorasExtras = table.Column<int>(type: "int", nullable: true),
                    Incapacidades = table.Column<int>(type: "int", nullable: true),
                    SalarioNetoFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlayrollNominaId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanillaEmpleados", x => x.PlanillaEmpleadoId);
                    table.ForeignKey(
                        name: "FK_PlanillaEmpleados_Playrolls_PlayrollNominaId",
                        column: x => x.PlayrollNominaId,
                        principalTable: "Playrolls",
                        principalColumn: "NominaId");
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

            migrationBuilder.CreateIndex(
                name: "IX_Asistences_EmployeeEmpleadoId",
                table: "Asistences",
                column: "EmployeeEmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Deducciones_EmployeeId",
                table: "Deducciones",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DisabilityEmployees_EmployeeEmpleadoId",
                table: "DisabilityEmployees",
                column: "EmployeeEmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoPlanilla_PlanillaEmpleadoId",
                table: "HistoricoPlanilla",
                column: "PlanillaEmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryPlayrolls_PlayrollNominaId",
                table: "HistoryPlayrolls",
                column: "PlayrollNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanillaEmpleados_PlayrollNominaId",
                table: "PlanillaEmpleados",
                column: "PlayrollNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_Playrolls_DeduccionesDeduccionId",
                table: "Playrolls",
                column: "DeduccionesDeduccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Playrolls_EmployeeEmpleadoId",
                table: "Playrolls",
                column: "EmployeeEmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_EmployeeEmpleadoId",
                table: "Vacations",
                column: "EmployeeEmpleadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistences");

            migrationBuilder.DropTable(
                name: "DisabilityEmployees");

            migrationBuilder.DropTable(
                name: "HistoricoPlanilla");

            migrationBuilder.DropTable(
                name: "HistoryPlayrolls");

            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.DropTable(
                name: "PlanillaEmpleados");

            migrationBuilder.DropTable(
                name: "Playrolls");

            migrationBuilder.DropTable(
                name: "Deducciones");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
