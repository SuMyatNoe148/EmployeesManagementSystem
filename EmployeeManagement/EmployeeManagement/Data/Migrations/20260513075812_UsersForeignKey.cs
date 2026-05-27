using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class UsersForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "systemProfiles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "systemProfiles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "SystemCodes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "SystemCodes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "systemCodeDetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "systemCodeDetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "LeaveTypes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "LeaveTypes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "LeavePeriods",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "LeavePeriods",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "leaveApplications",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "leaveApplications",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "LeaveAdjustmentEntries",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "LeaveAdjustmentEntries",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Holidays",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Holidays",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Designations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Designations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "countries",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "countries",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Banks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Banks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "ApprovalsUserMatrixs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "ApprovalsUserMatrixs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_systemProfiles_CreatedById",
                table: "systemProfiles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_systemProfiles_ModifiedById",
                table: "systemProfiles",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SystemCodes_CreatedById",
                table: "SystemCodes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SystemCodes_ModifiedById",
                table: "SystemCodes",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_systemCodeDetails_CreatedById",
                table: "systemCodeDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_systemCodeDetails_ModifiedById",
                table: "systemCodeDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveTypes_CreatedById",
                table: "LeaveTypes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveTypes_ModifiedById",
                table: "LeaveTypes",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeavePeriods_CreatedById",
                table: "LeavePeriods",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeavePeriods_ModifiedById",
                table: "LeavePeriods",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_leaveApplications_CreatedById",
                table: "leaveApplications",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_leaveApplications_ModifiedById",
                table: "leaveApplications",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAdjustmentEntries_CreatedById",
                table: "LeaveAdjustmentEntries",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAdjustmentEntries_ModifiedById",
                table: "LeaveAdjustmentEntries",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_CreatedById",
                table: "Holidays",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_ModifiedById",
                table: "Holidays",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CreatedById",
                table: "Employees",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ModifiedById",
                table: "Employees",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Designations_CreatedById",
                table: "Designations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Designations_ModifiedById",
                table: "Designations",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CreatedById",
                table: "Departments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ModifiedById",
                table: "Departments",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_countries_CreatedById",
                table: "countries",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_countries_ModifiedById",
                table: "countries",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CreatedById",
                table: "Banks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_ModifiedById",
                table: "Banks",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalsUserMatrixs_CreatedById",
                table: "ApprovalsUserMatrixs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalsUserMatrixs_ModifiedById",
                table: "ApprovalsUserMatrixs",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalsUserMatrixs_AspNetUsers_CreatedById",
                table: "ApprovalsUserMatrixs",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalsUserMatrixs_AspNetUsers_ModifiedById",
                table: "ApprovalsUserMatrixs",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_AspNetUsers_CreatedById",
                table: "Banks",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_AspNetUsers_ModifiedById",
                table: "Banks",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_countries_AspNetUsers_CreatedById",
                table: "countries",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_countries_AspNetUsers_ModifiedById",
                table: "countries",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_CreatedById",
                table: "Departments",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_ModifiedById",
                table: "Departments",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Designations_AspNetUsers_CreatedById",
                table: "Designations",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Designations_AspNetUsers_ModifiedById",
                table: "Designations",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_CreatedById",
                table: "Employees",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_ModifiedById",
                table: "Employees",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_AspNetUsers_CreatedById",
                table: "Holidays",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_AspNetUsers_ModifiedById",
                table: "Holidays",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAdjustmentEntries_AspNetUsers_CreatedById",
                table: "LeaveAdjustmentEntries",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAdjustmentEntries_AspNetUsers_ModifiedById",
                table: "LeaveAdjustmentEntries",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_leaveApplications_AspNetUsers_CreatedById",
                table: "leaveApplications",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_leaveApplications_AspNetUsers_ModifiedById",
                table: "leaveApplications",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeavePeriods_AspNetUsers_CreatedById",
                table: "LeavePeriods",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeavePeriods_AspNetUsers_ModifiedById",
                table: "LeavePeriods",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveTypes_AspNetUsers_CreatedById",
                table: "LeaveTypes",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveTypes_AspNetUsers_ModifiedById",
                table: "LeaveTypes",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_CreatedById",
                table: "systemCodeDetails",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_ModifiedById",
                table: "systemCodeDetails",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodes_AspNetUsers_CreatedById",
                table: "SystemCodes",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodes_AspNetUsers_ModifiedById",
                table: "SystemCodes",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_systemProfiles_AspNetUsers_CreatedById",
                table: "systemProfiles",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_systemProfiles_AspNetUsers_ModifiedById",
                table: "systemProfiles",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalsUserMatrixs_AspNetUsers_CreatedById",
                table: "ApprovalsUserMatrixs");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalsUserMatrixs_AspNetUsers_ModifiedById",
                table: "ApprovalsUserMatrixs");

            migrationBuilder.DropForeignKey(
                name: "FK_Banks_AspNetUsers_CreatedById",
                table: "Banks");

            migrationBuilder.DropForeignKey(
                name: "FK_Banks_AspNetUsers_ModifiedById",
                table: "Banks");

            migrationBuilder.DropForeignKey(
                name: "FK_countries_AspNetUsers_CreatedById",
                table: "countries");

            migrationBuilder.DropForeignKey(
                name: "FK_countries_AspNetUsers_ModifiedById",
                table: "countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_CreatedById",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_ModifiedById",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Designations_AspNetUsers_CreatedById",
                table: "Designations");

            migrationBuilder.DropForeignKey(
                name: "FK_Designations_AspNetUsers_ModifiedById",
                table: "Designations");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_CreatedById",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_ModifiedById",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_AspNetUsers_CreatedById",
                table: "Holidays");

            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_AspNetUsers_ModifiedById",
                table: "Holidays");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAdjustmentEntries_AspNetUsers_CreatedById",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAdjustmentEntries_AspNetUsers_ModifiedById",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_leaveApplications_AspNetUsers_CreatedById",
                table: "leaveApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_leaveApplications_AspNetUsers_ModifiedById",
                table: "leaveApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_LeavePeriods_AspNetUsers_CreatedById",
                table: "LeavePeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_LeavePeriods_AspNetUsers_ModifiedById",
                table: "LeavePeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveTypes_AspNetUsers_CreatedById",
                table: "LeaveTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveTypes_AspNetUsers_ModifiedById",
                table: "LeaveTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_CreatedById",
                table: "systemCodeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_systemCodeDetails_AspNetUsers_ModifiedById",
                table: "systemCodeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodes_AspNetUsers_CreatedById",
                table: "SystemCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodes_AspNetUsers_ModifiedById",
                table: "SystemCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_systemProfiles_AspNetUsers_CreatedById",
                table: "systemProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_systemProfiles_AspNetUsers_ModifiedById",
                table: "systemProfiles");

            migrationBuilder.DropIndex(
                name: "IX_systemProfiles_CreatedById",
                table: "systemProfiles");

            migrationBuilder.DropIndex(
                name: "IX_systemProfiles_ModifiedById",
                table: "systemProfiles");

            migrationBuilder.DropIndex(
                name: "IX_SystemCodes_CreatedById",
                table: "SystemCodes");

            migrationBuilder.DropIndex(
                name: "IX_SystemCodes_ModifiedById",
                table: "SystemCodes");

            migrationBuilder.DropIndex(
                name: "IX_systemCodeDetails_CreatedById",
                table: "systemCodeDetails");

            migrationBuilder.DropIndex(
                name: "IX_systemCodeDetails_ModifiedById",
                table: "systemCodeDetails");

            migrationBuilder.DropIndex(
                name: "IX_LeaveTypes_CreatedById",
                table: "LeaveTypes");

            migrationBuilder.DropIndex(
                name: "IX_LeaveTypes_ModifiedById",
                table: "LeaveTypes");

            migrationBuilder.DropIndex(
                name: "IX_LeavePeriods_CreatedById",
                table: "LeavePeriods");

            migrationBuilder.DropIndex(
                name: "IX_LeavePeriods_ModifiedById",
                table: "LeavePeriods");

            migrationBuilder.DropIndex(
                name: "IX_leaveApplications_CreatedById",
                table: "leaveApplications");

            migrationBuilder.DropIndex(
                name: "IX_leaveApplications_ModifiedById",
                table: "leaveApplications");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAdjustmentEntries_CreatedById",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAdjustmentEntries_ModifiedById",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.DropIndex(
                name: "IX_Holidays_CreatedById",
                table: "Holidays");

            migrationBuilder.DropIndex(
                name: "IX_Holidays_ModifiedById",
                table: "Holidays");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CreatedById",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ModifiedById",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Designations_CreatedById",
                table: "Designations");

            migrationBuilder.DropIndex(
                name: "IX_Designations_ModifiedById",
                table: "Designations");

            migrationBuilder.DropIndex(
                name: "IX_Departments_CreatedById",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ModifiedById",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_countries_CreatedById",
                table: "countries");

            migrationBuilder.DropIndex(
                name: "IX_countries_ModifiedById",
                table: "countries");

            migrationBuilder.DropIndex(
                name: "IX_Banks_CreatedById",
                table: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_Banks_ModifiedById",
                table: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalsUserMatrixs_CreatedById",
                table: "ApprovalsUserMatrixs");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalsUserMatrixs_ModifiedById",
                table: "ApprovalsUserMatrixs");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "systemProfiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "systemProfiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "SystemCodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "SystemCodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "systemCodeDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "systemCodeDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "LeavePeriods",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "LeavePeriods",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "leaveApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "leaveApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "LeaveAdjustmentEntries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "LeaveAdjustmentEntries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Designations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Designations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "countries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "countries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "ApprovalsUserMatrixs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "ApprovalsUserMatrixs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
