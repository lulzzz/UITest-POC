using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class applyofferJSON : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplierTenderQuantityTableItemJson",
                schema: "Offer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantitiyItemsJson = table.Column<string>(nullable: true),
                    SupplierTenderQuantityTableItems = table.Column<string>(nullable: true),
                    SupplierTenderQuantityTableId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierTenderQuantityTableItemJson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierTenderQuantityTableItemJson_SupplierTenderQuantityTable_SupplierTenderQuantityTableId",
                        column: x => x.SupplierTenderQuantityTableId,
                        principalSchema: "Offer",
                        principalTable: "SupplierTenderQuantityTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 21, 15, 16, 9, 195, DateTimeKind.Local).AddTicks(8400));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 21, 15, 16, 9, 195, DateTimeKind.Local).AddTicks(9176));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 21, 15, 16, 9, 195, DateTimeKind.Local).AddTicks(9192));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 21, 15, 16, 9, 195, DateTimeKind.Local).AddTicks(9192));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 21, 15, 16, 9, 195, DateTimeKind.Local).AddTicks(9192));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 21, 15, 16, 9, 195, DateTimeKind.Local).AddTicks(9192));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 21, 15, 16, 9, 195, DateTimeKind.Local).AddTicks(9192));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 21, 15, 16, 9, 195, DateTimeKind.Local).AddTicks(9197));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 21, 15, 16, 9, 195, DateTimeKind.Local).AddTicks(9197));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 21, 15, 16, 9, 195, DateTimeKind.Local).AddTicks(9197));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 21, 15, 16, 9, 195, DateTimeKind.Local).AddTicks(9197));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 21, 15, 16, 9, 195, DateTimeKind.Local).AddTicks(9197));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 21, 15, 16, 9, 195, DateTimeKind.Local).AddTicks(9197));

            migrationBuilder.CreateIndex(
                name: "IX_SupplierTenderQuantityTableItemJson_SupplierTenderQuantityTableId",
                schema: "Offer",
                table: "SupplierTenderQuantityTableItemJson",
                column: "SupplierTenderQuantityTableId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierTenderQuantityTableItemJson",
                schema: "Offer");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(4027));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5396));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5434));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5436));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5438));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5439));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5441));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5443));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5445));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5447));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5448));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5450));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5452));
        }
    }
}
