using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSystemNominaPlanilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistences_Employees_EmployeeEmpleadoId",
                table: "Asistences");

            migrationBuilder.DropForeignKey(
                name: "FK_DisabilityEmployees_Employees_EmployeeEmpleadoId",
                table: "DisabilityEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryPlayrolls_Playrolls_PlayrollNominaId",
                table: "HistoryPlayrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanillaEmpleados_Playrolls_PlayrollNominaId",
                table: "PlanillaEmpleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Playrolls_Deducciones_DeduccionesDeduccionId",
                table: "Playrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_Playrolls_Employees_EmployeeEmpleadoId",
                table: "Playrolls");

            migrationBuilder.DropIndex(
                name: "IX_Playrolls_DeduccionesDeduccionId",
                table: "Playrolls");

            migrationBuilder.DropIndex(
                name: "IX_Playrolls_EmployeeEmpleadoId",
                table: "Playrolls");

            migrationBuilder.DropIndex(
                name: "IX_PlanillaEmpleados_PlayrollNominaId",
                table: "PlanillaEmpleados");

            migrationBuilder.DropIndex(
                name: "IX_HistoryPlayrolls_PlayrollNominaId",
                table: "HistoryPlayrolls");

            migrationBuilder.DropIndex(
                name: "IX_DisabilityEmployees_EmployeeEmpleadoId",
                table: "DisabilityEmployees");

            migrationBuilder.DropIndex(
                name: "IX_Asistences_EmployeeEmpleadoId",
                table: "Asistences");

            migrationBuilder.DropColumn(
                name: "DeduccionesDeduccionId",
                table: "Playrolls");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "Playrolls");

            migrationBuilder.DropColumn(
                name: "EmployeeEmpleadoId",
                table: "Playrolls");

            migrationBuilder.DropColumn(
                name: "PlayrollNominaId",
                table: "PlanillaEmpleados");

            migrationBuilder.DropColumn(
                name: "PlayrollNominaId",
                table: "HistoryPlayrolls");

            migrationBuilder.DropColumn(
                name: "EmployeeEmpleadoId",
                table: "DisabilityEmployees");

            migrationBuilder.DropColumn(
                name: "EmployeeEmpleadoId",
                table: "Asistences");

            migrationBuilder.CreateIndex(
                name: "IX_Playrolls_DeduccionId",
                table: "Playrolls",
                column: "DeduccionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanillaEmpleados_NominaId",
                table: "PlanillaEmpleados",
                column: "NominaId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryPlayrolls_NominaId",
                table: "HistoryPlayrolls",
                column: "NominaId");

            migrationBuilder.CreateIndex(
                name: "IX_DisabilityEmployees_EmpleadoId",
                table: "DisabilityEmployees",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistences_EmpleadoId",
                table: "Asistences",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asistences_Employees_EmpleadoId",
                table: "Asistences",
                column: "EmpleadoId",
                principalTable: "Employees",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisabilityEmployees_Employees_EmpleadoId",
                table: "DisabilityEmployees",
                column: "EmpleadoId",
                principalTable: "Employees",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryPlayrolls_Playrolls_NominaId",
                table: "HistoryPlayrolls",
                column: "NominaId",
                principalTable: "Playrolls",
                principalColumn: "NominaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanillaEmpleados_Playrolls_NominaId",
                table: "PlanillaEmpleados",
                column: "NominaId",
                principalTable: "Playrolls",
                principalColumn: "NominaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Playrolls_Deducciones_DeduccionId",
                table: "Playrolls",
                column: "DeduccionId",
                principalTable: "Deducciones",
                principalColumn: "DeduccionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistences_Employees_EmpleadoId",
                table: "Asistences");

            migrationBuilder.DropForeignKey(
                name: "FK_DisabilityEmployees_Employees_EmpleadoId",
                table: "DisabilityEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryPlayrolls_Playrolls_NominaId",
                table: "HistoryPlayrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanillaEmpleados_Playrolls_NominaId",
                table: "PlanillaEmpleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Playrolls_Deducciones_DeduccionId",
                table: "Playrolls");

            migrationBuilder.DropIndex(
                name: "IX_Playrolls_DeduccionId",
                table: "Playrolls");

            migrationBuilder.DropIndex(
                name: "IX_PlanillaEmpleados_NominaId",
                table: "PlanillaEmpleados");

            migrationBuilder.DropIndex(
                name: "IX_HistoryPlayrolls_NominaId",
                table: "HistoryPlayrolls");

            migrationBuilder.DropIndex(
                name: "IX_DisabilityEmployees_EmpleadoId",
                table: "DisabilityEmployees");

            migrationBuilder.DropIndex(
                name: "IX_Asistences_EmpleadoId",
                table: "Asistences");

            migrationBuilder.AddColumn<long>(
                name: "DeduccionesDeduccionId",
                table: "Playrolls",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmpleadoId",
                table: "Playrolls",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeEmpleadoId",
                table: "Playrolls",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PlayrollNominaId",
                table: "PlanillaEmpleados",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PlayrollNominaId",
                table: "HistoryPlayrolls",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeEmpleadoId",
                table: "DisabilityEmployees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeEmpleadoId",
                table: "Asistences",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Playrolls_DeduccionesDeduccionId",
                table: "Playrolls",
                column: "DeduccionesDeduccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Playrolls_EmployeeEmpleadoId",
                table: "Playrolls",
                column: "EmployeeEmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanillaEmpleados_PlayrollNominaId",
                table: "PlanillaEmpleados",
                column: "PlayrollNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryPlayrolls_PlayrollNominaId",
                table: "HistoryPlayrolls",
                column: "PlayrollNominaId");

            migrationBuilder.CreateIndex(
                name: "IX_DisabilityEmployees_EmployeeEmpleadoId",
                table: "DisabilityEmployees",
                column: "EmployeeEmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistences_EmployeeEmpleadoId",
                table: "Asistences",
                column: "EmployeeEmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asistences_Employees_EmployeeEmpleadoId",
                table: "Asistences",
                column: "EmployeeEmpleadoId",
                principalTable: "Employees",
                principalColumn: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DisabilityEmployees_Employees_EmployeeEmpleadoId",
                table: "DisabilityEmployees",
                column: "EmployeeEmpleadoId",
                principalTable: "Employees",
                principalColumn: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryPlayrolls_Playrolls_PlayrollNominaId",
                table: "HistoryPlayrolls",
                column: "PlayrollNominaId",
                principalTable: "Playrolls",
                principalColumn: "NominaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanillaEmpleados_Playrolls_PlayrollNominaId",
                table: "PlanillaEmpleados",
                column: "PlayrollNominaId",
                principalTable: "Playrolls",
                principalColumn: "NominaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playrolls_Deducciones_DeduccionesDeduccionId",
                table: "Playrolls",
                column: "DeduccionesDeduccionId",
                principalTable: "Deducciones",
                principalColumn: "DeduccionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playrolls_Employees_EmployeeEmpleadoId",
                table: "Playrolls",
                column: "EmployeeEmpleadoId",
                principalTable: "Employees",
                principalColumn: "EmpleadoId");
        }
    }
}
