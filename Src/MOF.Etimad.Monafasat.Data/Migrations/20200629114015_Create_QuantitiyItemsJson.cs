using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class Create_QuantitiyItemsJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenderQuantityTableItem_TenderQuantityTable_TenderQuantityTableId",
                schema: "Tender",
                table: "TenderQuantityTableItem");

            migrationBuilder.DropIndex(
                name: "IX_TenderQuantityTableItem_TenderQuantityTableId",
                schema: "Tender",
                table: "TenderQuantityTableItem");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Tender",
                table: "TenderQuantityTableItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Tender",
                table: "TenderQuantityTableItem");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Tender",
                table: "TenderQuantityTableItem");

            migrationBuilder.DropColumn(
                name: "TenderQuantityTableId",
                schema: "Tender",
                table: "TenderQuantityTableItem");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Tender",
                table: "TenderQuantityTableItem");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "Tender",
                table: "TenderQuantityTableItem");

            migrationBuilder.RenameTable(
                name: "TenderQuantityTableItem",
                schema: "Tender",
                newName: "TenderQuantityTableItem");

            migrationBuilder.CreateTable(
                name: "TenderQuantitiyItemsJson",
                schema: "Tender",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenderQuantityTableId = table.Column<long>(nullable: false),
                    TenderQuantityTableItems = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderQuantitiyItemsJson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderQuantitiyItemsJson_TenderQuantityTable_TenderQuantityTableId",
                        column: x => x.TenderQuantityTableId,
                        principalSchema: "Tender",
                        principalTable: "TenderQuantityTable",
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
                name: "IX_TenderQuantitiyItemsJson_TenderQuantityTableId",
                schema: "Tender",
                table: "TenderQuantitiyItemsJson",
                column: "TenderQuantityTableId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenderQuantitiyItemsJson",
                schema: "Tender");

            migrationBuilder.RenameTable(
                name: "TenderQuantityTableItem",
                newName: "TenderQuantityTableItem",
                newSchema: "Tender");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Tender",
                table: "TenderQuantityTableItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Tender",
                table: "TenderQuantityTableItem",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Tender",
                table: "TenderQuantityTableItem",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenderQuantityTableId",
                schema: "Tender",
                table: "TenderQuantityTableItem",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Tender",
                table: "TenderQuantityTableItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "Tender",
                table: "TenderQuantityTableItem",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 1,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(5388));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 2,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(6982));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 3,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7098));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 4,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7104));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 5,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7109));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 6,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7113));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 7,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7117));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 8,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7122));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 9,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7126));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 10,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7131));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 11,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7135));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 12,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7140));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 13,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7145));

            migrationBuilder.CreateIndex(
                name: "IX_TenderQuantityTableItem_TenderQuantityTableId",
                schema: "Tender",
                table: "TenderQuantityTableItem",
                column: "TenderQuantityTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_TenderQuantityTableItem_TenderQuantityTable_TenderQuantityTableId",
                schema: "Tender",
                table: "TenderQuantityTableItem",
                column: "TenderQuantityTableId",
                principalSchema: "Tender",
                principalTable: "TenderQuantityTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
