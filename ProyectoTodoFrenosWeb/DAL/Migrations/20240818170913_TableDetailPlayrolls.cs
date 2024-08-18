using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class TableDetailPlayrolls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deducciones_Employees_EmployeeId",
                table: "Deducciones");

            migrationBuilder.DropColumn(
                name: "DiasVacaciones",
                table: "Playrolls");

            migrationBuilder.DropColumn(
                name: "HorasExtras",
                table: "Playrolls");

            migrationBuilder.DropColumn(
                name: "Incapacidad",
                table: "Playrolls");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Deducciones",
                newName: "NominaDetalleId");

            migrationBuilder.RenameIndex(
                name: "IX_Deducciones_EmployeeId",
                table: "Deducciones",
                newName: "IX_Deducciones_NominaDetalleId");

            migrationBuilder.AddColumn<int>(
                name: "CantDiasLaborales",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlayrollDetails",
                columns: table => new
                {
                    NominaDetalleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    HorasExtras = table.Column<int>(type: "int", nullable: false),
                    DiasVacaciones = table.Column<int>(type: "int", nullable: false),
                    Incapacidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SalarioBruto = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayrollDetails", x => x.NominaDetalleId);
                    table.ForeignKey(
                        name: "FK_PlayrollDetails_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayrollDetails_EmployeeId",
                table: "PlayrollDetails",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deducciones_PlayrollDetails_NominaDetalleId",
                table: "Deducciones",
                column: "NominaDetalleId",
                principalTable: "PlayrollDetails",
                principalColumn: "NominaDetalleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deducciones_PlayrollDetails_NominaDetalleId",
                table: "Deducciones");

            migrationBuilder.DropTable(
                name: "PlayrollDetails");

            migrationBuilder.DropColumn(
                name: "CantDiasLaborales",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "NominaDetalleId",
                table: "Deducciones",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Deducciones_NominaDetalleId",
                table: "Deducciones",
                newName: "IX_Deducciones_EmployeeId");

            migrationBuilder.AddColumn<int>(
                name: "DiasVacaciones",
                table: "Playrolls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HorasExtras",
                table: "Playrolls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Incapacidad",
                table: "Playrolls",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Deducciones_Employees_EmployeeId",
                table: "Deducciones",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
