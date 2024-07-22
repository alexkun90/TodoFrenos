using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModuleAppointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointModifyDate",
                table: "SupplierAppointments");

            migrationBuilder.DropColumn(
                name: "AppointDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointModifyDate",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierEmail",
                table: "SupplierAppointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SupplierEmail",
                table: "SupplierAppointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointModifyDate",
                table: "SupplierAppointments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "AppointDate",
                table: "Appointments",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointModifyDate",
                table: "Appointments",
                type: "datetime2",
                nullable: true);
        }
    }
}
