using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoseEmporium.Data.Migrations
{
    public partial class ShoseVendorRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoseVendor",
                columns: table => new
                {
                    ShosesId = table.Column<int>(type: "int", nullable: false),
                    VendorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoseVendor", x => new { x.ShosesId, x.VendorsId });
                    table.ForeignKey(
                        name: "FK_ShoseVendor_Shose_ShosesId",
                        column: x => x.ShosesId,
                        principalTable: "Shose",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoseVendor_Vendor_VendorsId",
                        column: x => x.VendorsId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorofShose",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoseId = table.Column<int>(type: "int", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorofShose", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorofShose_Shose_ShoseId",
                        column: x => x.ShoseId,
                        principalTable: "Shose",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorofShose_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoseVendor_VendorsId",
                table: "ShoseVendor",
                column: "VendorsId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorofShose_ShoseId",
                table: "VendorofShose",
                column: "ShoseId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorofShose_VendorId",
                table: "VendorofShose",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoseVendor");

            migrationBuilder.DropTable(
                name: "VendorofShose");
        }
    }
}
