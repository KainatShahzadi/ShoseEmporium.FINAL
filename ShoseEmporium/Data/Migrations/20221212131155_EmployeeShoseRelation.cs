using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoseEmporium.Data.Migrations
{
    public partial class EmployeeShoseRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Shose",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shose_EmployeeId",
                table: "Shose",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shose_Employee_EmployeeId",
                table: "Shose",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shose_Employee_EmployeeId",
                table: "Shose");

            migrationBuilder.DropIndex(
                name: "IX_Shose_EmployeeId",
                table: "Shose");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Shose");
        }
    }
}
