using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class removeBlockTrackingStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockTrackingStatus",
                schema: "Block");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockTrackingStatus",
                schema: "Block",
                columns: table => new
                {
                    BlockTrackingStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SupplierBlockId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockTrackingStatus", x => x.BlockTrackingStatusId);
                    table.ForeignKey(
                        name: "FK_BlockTrackingStatus_SupplierBlock_SupplierBlockId",
                        column: x => x.SupplierBlockId,
                        principalSchema: "Block",
                        principalTable: "SupplierBlock",
                        principalColumn: "SupplierBlockId",
                        onDelete: ReferentialAction.Restrict);
                });


            migrationBuilder.CreateIndex(
                name: "IX_BlockTrackingStatus_SupplierBlockId",
                schema: "Block",
                table: "BlockTrackingStatus",
                column: "SupplierBlockId");
        }
    }
}
