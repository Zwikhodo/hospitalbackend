using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalSystem.Migrations
{
    public partial class TableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Persons_PatientId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientReports_Persons_EmployeeId",
                table: "PatientReports");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "PatientReports",
                newName: "ProcedureId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientReports_EmployeeId",
                table: "PatientReports",
                newName: "IX_PatientReports_ProcedureId");

            migrationBuilder.RenameColumn(
                name: "Procedure",
                table: "Bills",
                newName: "ProcedureCharge");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Bills",
                newName: "PatientReportId");

            migrationBuilder.RenameColumn(
                name: "DoctorCharge",
                table: "Bills",
                newName: "PrescribedTestCharge");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_PatientId",
                table: "Bills",
                newName: "IX_Bills_PatientReportId");

            migrationBuilder.AddColumn<decimal>(
                name: "CostPerNight",
                table: "Rooms",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Procedures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ExaminationId",
                table: "PatientReports",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PrescribedTestId",
                table: "PatientReports",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostPerNight",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EmployeeCharge",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_PatientReports_ExaminationId",
                table: "PatientReports",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientReports_PrescribedTestId",
                table: "PatientReports",
                column: "PrescribedTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_PatientReports_PatientReportId",
                table: "Bills",
                column: "PatientReportId",
                principalTable: "PatientReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReports_Examinations_ExaminationId",
                table: "PatientReports",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReports_PrescribedTests_PrescribedTestId",
                table: "PatientReports",
                column: "PrescribedTestId",
                principalTable: "PrescribedTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReports_Procedures_ProcedureId",
                table: "PatientReports",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_PatientReports_PatientReportId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientReports_Examinations_ExaminationId",
                table: "PatientReports");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientReports_PrescribedTests_PrescribedTestId",
                table: "PatientReports");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientReports_Procedures_ProcedureId",
                table: "PatientReports");

            migrationBuilder.DropIndex(
                name: "IX_PatientReports_ExaminationId",
                table: "PatientReports");

            migrationBuilder.DropIndex(
                name: "IX_PatientReports_PrescribedTestId",
                table: "PatientReports");

            migrationBuilder.DropColumn(
                name: "CostPerNight",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "ExaminationId",
                table: "PatientReports");

            migrationBuilder.DropColumn(
                name: "PrescribedTestId",
                table: "PatientReports");

            migrationBuilder.DropColumn(
                name: "CostPerNight",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "EmployeeCharge",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "ProcedureId",
                table: "PatientReports",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientReports_ProcedureId",
                table: "PatientReports",
                newName: "IX_PatientReports_EmployeeId");

            migrationBuilder.RenameColumn(
                name: "ProcedureCharge",
                table: "Bills",
                newName: "Procedure");

            migrationBuilder.RenameColumn(
                name: "PrescribedTestCharge",
                table: "Bills",
                newName: "DoctorCharge");

            migrationBuilder.RenameColumn(
                name: "PatientReportId",
                table: "Bills",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_PatientReportId",
                table: "Bills",
                newName: "IX_Bills_PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Persons_PatientId",
                table: "Bills",
                column: "PatientId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReports_Persons_EmployeeId",
                table: "PatientReports",
                column: "EmployeeId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
