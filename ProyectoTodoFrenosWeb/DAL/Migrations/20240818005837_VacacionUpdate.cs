using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class VacacionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_Employees_EmployeeEmpleadoId",
                table: "Vacations");

            migrationBuilder.DropIndex(
                name: "IX_Vacations_EmployeeEmpleadoId",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "EmployeeEmpleadoId",
                table: "Vacations");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_EmpleadoId",
                table: "Vacations",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_Employees_EmpleadoId",
                table: "Vacations",
                column: "EmpleadoId",
                principalTable: "Employees",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_Employees_EmpleadoId",
                table: "Vacations");

            migrationBuilder.DropIndex(
                name: "IX_Vacations_EmpleadoId",
                table: "Vacations");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeEmpleadoId",
                table: "Vacations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_EmployeeEmpleadoId",
                table: "Vacations",
                column: "EmployeeEmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_Employees_EmployeeEmpleadoId",
                table: "Vacations",
                column: "EmployeeEmpleadoId",
                principalTable: "Employees",
                principalColumn: "EmpleadoId");
        }
    }
}
