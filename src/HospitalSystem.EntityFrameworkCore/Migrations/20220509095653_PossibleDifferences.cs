using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalSystem.Migrations
{
    public partial class PossibleDifferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescribedTests_Rooms_RoomId",
                table: "PrescribedTests");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "PrescribedTests",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_PrescribedTests_RoomId",
                table: "PrescribedTests",
                newName: "IX_PrescribedTests_EmployeeId");

            migrationBuilder.AlterColumn<string>(
                name: "RoomNumber",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescribedTests_Persons_EmployeeId",
                table: "PrescribedTests",
                column: "EmployeeId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescribedTests_Persons_EmployeeId",
                table: "PrescribedTests");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "PrescribedTests",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_PrescribedTests_EmployeeId",
                table: "PrescribedTests",
                newName: "IX_PrescribedTests_RoomId");

            migrationBuilder.AlterColumn<int>(
                name: "RoomNumber",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescribedTests_Rooms_RoomId",
                table: "PrescribedTests",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
