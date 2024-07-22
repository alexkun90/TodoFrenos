using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTypeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierAppointments_SupplierLists_SupplierListId",
                table: "SupplierAppointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierLists",
                table: "SupplierLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierAppointments",
                table: "SupplierAppointments");

            migrationBuilder.AlterColumn<long>(
                name: "SupplierListId",
                table: "SupplierLists",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "SupplierListId",
                table: "SupplierAppointments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "SupplierAppointId",
                table: "SupplierAppointments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierLists",
                table: "SupplierLists",
                column: "SupplierListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierAppointments",
                table: "SupplierAppointments",
                column: "SupplierAppointId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierAppointments_SupplierLists_SupplierListId",
                table: "SupplierAppointments",
                column: "SupplierListId",
                principalTable: "SupplierLists",
                principalColumn: "SupplierListId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierAppointments_SupplierLists_SupplierListId",
                table: "SupplierAppointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierLists",
                table: "SupplierLists");

            migrationBuilder.DropPrimaryKey(
               name: "PK_SupplierAppointments",
               table: "SupplierAppointments");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierListId",
                table: "SupplierLists",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierListId",
                table: "SupplierAppointments",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierAppointId",
                table: "SupplierAppointments",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
               name: "PK_SupplierLists",
               table: "SupplierLists",
               column: "SupplierListId");

            migrationBuilder.AddPrimaryKey(
               name: "PK_SupplierAppointments",
               table: "SupplierAppointments",
               column: "SupplierAppointId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierAppointments_SupplierLists_SupplierListId",
                table: "SupplierAppointments",
                column: "SupplierListId",
                principalTable: "SupplierLists",
                principalColumn: "SupplierListId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
