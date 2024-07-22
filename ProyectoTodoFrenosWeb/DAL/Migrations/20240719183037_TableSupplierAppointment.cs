using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class TableSupplierAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Inspections_InspectionsIdInsep",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "WorkPerformeds");

            migrationBuilder.DropTable(
                name: "Inspections");

            migrationBuilder.DropTable(
                name: "CheckLists");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_InspectionsIdInsep",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "NextChangeDue",
                table: "VehicleInspections");

            migrationBuilder.DropColumn(
                name: "InspectionsIdInsep",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "Kilometraje",
                table: "VehicleInspections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OilChangeKilometraje",
                table: "VehicleInspections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppointState",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointCreationDate",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SupplierLists",
                columns: table => new
                {
                    SupplierListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierLists", x => x.SupplierListId);
                });

            migrationBuilder.CreateTable(
                name: "SupplierAppointments",
                columns: table => new
                {
                    SupplierAppointId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierListId = table.Column<int>(type: "int", nullable: false),
                    AppointCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierAppointments", x => x.SupplierAppointId);
                    table.ForeignKey(
                        name: "FK_SupplierAppointments_SupplierLists_SupplierListId",
                        column: x => x.SupplierListId,
                        principalTable: "SupplierLists",
                        principalColumn: "SupplierListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAppointments_SupplierListId",
                table: "SupplierAppointments",
                column: "SupplierListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierAppointments");

            migrationBuilder.DropTable(
                name: "SupplierLists");

            migrationBuilder.DropColumn(
                name: "Kilometraje",
                table: "VehicleInspections");

            migrationBuilder.DropColumn(
                name: "OilChangeKilometraje",
                table: "VehicleInspections");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextChangeDue",
                table: "VehicleInspections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AppointState",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointCreationDate",
                table: "Appointments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "InspectionsIdInsep",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CheckLists",
                columns: table => new
                {
                    IdCheckList = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryVe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdInspect = table.Column<int>(type: "int", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateVeh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckLists", x => x.IdCheckList);
                });

            migrationBuilder.CreateTable(
                name: "Inspections",
                columns: table => new
                {
                    IdInsep = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<long>(type: "bigint", nullable: false),
                    CheckListIdCheckList = table.Column<int>(type: "int", nullable: true),
                    DateInspection = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdMecanico = table.Column<int>(type: "int", nullable: false),
                    IdVehicle = table.Column<long>(type: "bigint", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recomendation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspections", x => x.IdInsep);
                    table.ForeignKey(
                        name: "FK_Inspections_CheckLists_CheckListIdCheckList",
                        column: x => x.CheckListIdCheckList,
                        principalTable: "CheckLists",
                        principalColumn: "IdCheckList");
                    table.ForeignKey(
                        name: "FK_Inspections_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkPerformeds",
                columns: table => new
                {
                    IdWorkPer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InspeIdInsep = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdInspe = table.Column<int>(type: "int", nullable: false),
                    NextMaintenance = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPerformeds", x => x.IdWorkPer);
                    table.ForeignKey(
                        name: "FK_WorkPerformeds_Inspections_InspeIdInsep",
                        column: x => x.InspeIdInsep,
                        principalTable: "Inspections",
                        principalColumn: "IdInsep",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_InspectionsIdInsep",
                table: "Appointments",
                column: "InspectionsIdInsep");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_CheckListIdCheckList",
                table: "Inspections",
                column: "CheckListIdCheckList");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_VehicleId",
                table: "Inspections",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPerformeds_InspeIdInsep",
                table: "WorkPerformeds",
                column: "InspeIdInsep");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Inspections_InspectionsIdInsep",
                table: "Appointments",
                column: "InspectionsIdInsep",
                principalTable: "Inspections",
                principalColumn: "IdInsep");
        }
    }
}
