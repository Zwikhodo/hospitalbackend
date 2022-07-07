using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalSystem.Migrations
{
    public partial class EmployeeLogCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckIn",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CheckOut",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Appointments",
                newName: "StartTime");

            migrationBuilder.AlterColumn<string>(
                name: "AppointmentNumber",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Appointments",
                newName: "Duration");

            migrationBuilder.AddColumn<string>(
                name: "CheckIn",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckOut",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppointmentNumber",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
