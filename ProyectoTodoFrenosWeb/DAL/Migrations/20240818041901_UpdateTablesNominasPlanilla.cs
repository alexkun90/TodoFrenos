using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesNominasPlanilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HorasExtras",
                table: "PlanillaEmpleados");

            migrationBuilder.DropColumn(
                name: "HorasTrabajadas",
                table: "PlanillaEmpleados");

            migrationBuilder.DropColumn(
                name: "Incapacidades",
                table: "PlanillaEmpleados");

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "PlanillaEmpleados",
                newName: "FechaPago");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalarioNeto",
                table: "Playrolls",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "Playrolls",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "Playrolls",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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

            migrationBuilder.AddColumn<int>(
                name: "HorasTrabajadas",
                table: "Employees",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiasVacaciones",
                table: "Playrolls");

            migrationBuilder.DropColumn(
                name: "HorasExtras",
                table: "Playrolls");

            migrationBuilder.DropColumn(
                name: "Incapacidad",
                table: "Playrolls");

            migrationBuilder.DropColumn(
                name: "HorasTrabajadas",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "FechaPago",
                table: "PlanillaEmpleados",
                newName: "FechaCreacion");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalarioNeto",
                table: "Playrolls",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "Playrolls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFin",
                table: "Playrolls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HorasExtras",
                table: "PlanillaEmpleados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HorasTrabajadas",
                table: "PlanillaEmpleados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Incapacidades",
                table: "PlanillaEmpleados",
                type: "int",
                nullable: true);
        }
    }
}
