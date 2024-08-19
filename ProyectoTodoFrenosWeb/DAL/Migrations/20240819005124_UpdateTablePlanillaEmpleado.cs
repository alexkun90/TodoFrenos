using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablePlanillaEmpleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SalarioNetoFinal",
                table: "PlanillaEmpleados",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaPago",
                table: "PlanillaEmpleados",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "ApellidoEmpleado",
                table: "PlanillaEmpleados",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cedula",
                table: "PlanillaEmpleados",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IVM",
                table: "PlanillaEmpleados",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ImpuestoRenta",
                table: "PlanillaEmpleados",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LPT",
                table: "PlanillaEmpleados",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreEmpleado",
                table: "PlanillaEmpleados",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Puesto",
                table: "PlanillaEmpleados",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SEM",
                table: "PlanillaEmpleados",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalarioBruto",
                table: "PlanillaEmpleados",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDeducciones",
                table: "PlanillaEmpleados",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApellidoEmpleado",
                table: "PlanillaEmpleados");

            migrationBuilder.DropColumn(
                name: "Cedula",
                table: "PlanillaEmpleados");

            migrationBuilder.DropColumn(
                name: "IVM",
                table: "PlanillaEmpleados");

            migrationBuilder.DropColumn(
                name: "ImpuestoRenta",
                table: "PlanillaEmpleados");

            migrationBuilder.DropColumn(
                name: "LPT",
                table: "PlanillaEmpleados");

            migrationBuilder.DropColumn(
                name: "NombreEmpleado",
                table: "PlanillaEmpleados");

            migrationBuilder.DropColumn(
                name: "Puesto",
                table: "PlanillaEmpleados");

            migrationBuilder.DropColumn(
                name: "SEM",
                table: "PlanillaEmpleados");

            migrationBuilder.DropColumn(
                name: "SalarioBruto",
                table: "PlanillaEmpleados");

            migrationBuilder.DropColumn(
                name: "TotalDeducciones",
                table: "PlanillaEmpleados");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalarioNetoFinal",
                table: "PlanillaEmpleados",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaPago",
                table: "PlanillaEmpleados",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
