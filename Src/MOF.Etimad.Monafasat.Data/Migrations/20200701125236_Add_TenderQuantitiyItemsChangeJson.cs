using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class Add_TenderQuantitiyItemsChangeJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenderQuantityTableItemChanges",
                schema: "Tender");

            migrationBuilder.CreateTable(
                name: "TenderQuantitiyItemsChangeJson",
                schema: "Tender",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenderQuantityTableChangeId = table.Column<long>(nullable: false),
                    TenderQuantityTableItemChanges = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderQuantitiyItemsChangeJson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderQuantitiyItemsChangeJson_TenderQuantityTableChanges_TenderQuantityTableChangeId",
                        column: x => x.TenderQuantityTableChangeId,
                        principalSchema: "Tender",
                        principalTable: "TenderQuantityTableChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 1,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(4027));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 2,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5396));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 3,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5434));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 4,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5436));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 5,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5438));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 6,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5439));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 7,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5441));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 8,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5443));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 9,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5445));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 10,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5447));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 11,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5448));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 12,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5450));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 13,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 1, 15, 52, 30, 809, DateTimeKind.Local).AddTicks(5452));

            migrationBuilder.CreateIndex(
                name: "IX_TenderQuantitiyItemsChangeJson_TenderQuantityTableChangeId",
                schema: "Tender",
                table: "TenderQuantitiyItemsChangeJson",
                column: "TenderQuantityTableChangeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenderQuantitiyItemsChangeJson",
                schema: "Tender");

            migrationBuilder.CreateTable(
                name: "TenderQuantityTableItemChanges",
                schema: "Tender",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityTemplateId = table.Column<int>(type: "int", nullable: true),
                    ColumnId = table.Column<long>(type: "bigint", nullable: false),
                    ColumnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ItemNumber = table.Column<long>(type: "bigint", nullable: false),
                    TenderFormHeaderId = table.Column<long>(type: "bigint", nullable: true),
                    TenderQuantityTableChangesId = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderQuantityTableItemChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderQuantityTableItemChanges_TenderQuantityTableChanges_TenderQuantityTableChangesId",
                        column: x => x.TenderQuantityTableChangesId,
                        principalSchema: "Tender",
                        principalTable: "TenderQuantityTableChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 1,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 29, 14, 40, 12, 517, DateTimeKind.Local).AddTicks(4281));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 2,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 29, 14, 40, 12, 517, DateTimeKind.Local).AddTicks(5619));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 3,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 29, 14, 40, 12, 517, DateTimeKind.Local).AddTicks(5651));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 4,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 29, 14, 40, 12, 517, DateTimeKind.Local).AddTicks(5654));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 5,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 29, 14, 40, 12, 517, DateTimeKind.Local).AddTicks(5656));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 6,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 29, 14, 40, 12, 517, DateTimeKind.Local).AddTicks(5658));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 7,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 29, 14, 40, 12, 517, DateTimeKind.Local).AddTicks(5660));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 8,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 29, 14, 40, 12, 517, DateTimeKind.Local).AddTicks(5662));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 9,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 29, 14, 40, 12, 517, DateTimeKind.Local).AddTicks(5664));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 10,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 29, 14, 40, 12, 517, DateTimeKind.Local).AddTicks(5666));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 11,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 29, 14, 40, 12, 517, DateTimeKind.Local).AddTicks(5668));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 12,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 29, 14, 40, 12, 517, DateTimeKind.Local).AddTicks(5669));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 13,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 29, 14, 40, 12, 517, DateTimeKind.Local).AddTicks(5671));

            migrationBuilder.CreateIndex(
                name: "IX_TenderQuantityTableItemChanges_TenderQuantityTableChangesId",
                schema: "Tender",
                table: "TenderQuantityTableItemChanges",
                column: "TenderQuantityTableChangesId");
        }
    }
}
