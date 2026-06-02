using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    OldDepartmentId = table.Column<int>(type: "int", nullable: true),
                    NewDepartmentId = table.Column<int>(type: "int", nullable: true),
                    OldDesignationId = table.Column<int>(type: "int", nullable: true),
                    NewDesignationId = table.Column<int>(type: "int", nullable: true),
                    OldSalary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    NewSalary = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ChangeTypeId = table.Column<int>(type: "int", nullable: false),
                    EffectiveDtae = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeHistories_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeHistories_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeHistories_Departments_NewDepartmentId",
                        column: x => x.NewDepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeHistories_Departments_OldDepartmentId",
                        column: x => x.OldDepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeHistories_Designations_NewDesignationId",
                        column: x => x.NewDesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeHistories_Designations_OldDesignationId",
                        column: x => x.OldDesignationId,
                        principalTable: "Designations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeHistories_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeHistories_systemCodeDetails_ChangeTypeId",
                        column: x => x.ChangeTypeId,
                        principalTable: "systemCodeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistories_ChangeTypeId",
                table: "EmployeeHistories",
                column: "ChangeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistories_CreatedById",
                table: "EmployeeHistories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistories_EmployeeId",
                table: "EmployeeHistories",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistories_ModifiedById",
                table: "EmployeeHistories",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistories_NewDepartmentId",
                table: "EmployeeHistories",
                column: "NewDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistories_NewDesignationId",
                table: "EmployeeHistories",
                column: "NewDesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistories_OldDepartmentId",
                table: "EmployeeHistories",
                column: "OldDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistories_OldDesignationId",
                table: "EmployeeHistories",
                column: "OldDesignationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeHistories");
        }
    }
}
