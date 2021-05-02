using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class Remove_SupplierTenderQuantityTableItemJson_QuantityItemJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "SupplierTenderQuantityTableItem",
            //    schema: "Offer");

            //migrationBuilder.DropTable(
            //    name: "TenderQuantityTableItem");

            migrationBuilder.DropColumn(
                name: "QuantitiyItemsJson",
                schema: "Offer",
                table: "SupplierTenderQuantityTableItemJson");

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 1,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 12, 43, 10, 302, DateTimeKind.Local).AddTicks(8537));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 2,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 12, 43, 10, 303, DateTimeKind.Local).AddTicks(899));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 3,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 12, 43, 10, 303, DateTimeKind.Local).AddTicks(944));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 4,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 12, 43, 10, 303, DateTimeKind.Local).AddTicks(950));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 5,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 12, 43, 10, 303, DateTimeKind.Local).AddTicks(954));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 6,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 12, 43, 10, 303, DateTimeKind.Local).AddTicks(958));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 7,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 12, 43, 10, 303, DateTimeKind.Local).AddTicks(962));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 8,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 12, 43, 10, 303, DateTimeKind.Local).AddTicks(965));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 9,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 12, 43, 10, 303, DateTimeKind.Local).AddTicks(969));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 10,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 12, 43, 10, 303, DateTimeKind.Local).AddTicks(973));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 11,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 12, 43, 10, 303, DateTimeKind.Local).AddTicks(977));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 12,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 12, 43, 10, 303, DateTimeKind.Local).AddTicks(981));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 13,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 12, 43, 10, 303, DateTimeKind.Local).AddTicks(984));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuantitiyItemsJson",
                schema: "Offer",
                table: "SupplierTenderQuantityTableItemJson",
                type: "nvarchar(max)",
                nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "TenderQuantityTableItem",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ActivityTemplateId = table.Column<int>(type: "int", nullable: true),
            //        ColumnId = table.Column<long>(type: "bigint", nullable: false),
            //        ColumnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ItemNumber = table.Column<long>(type: "bigint", nullable: false),
            //        TenderFormHeaderId = table.Column<long>(type: "bigint", nullable: true),
            //        Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TenderQuantityTableItem", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SupplierTenderQuantityTableItem",
            //    schema: "Offer",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ActivityTemplateId = table.Column<int>(type: "int", nullable: true),
            //        AlternativeValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ColumnId = table.Column<long>(type: "bigint", nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        IsActive = table.Column<bool>(type: "bit", nullable: true),
            //        IsDefault = table.Column<bool>(type: "bit", nullable: false),
            //        ItemNumber = table.Column<long>(type: "bigint", nullable: false),
            //        SupplierTenderQuantityTableId = table.Column<long>(type: "bigint", nullable: false),
            //        TenderFormHeaderId = table.Column<long>(type: "bigint", nullable: true),
            //        TenderQuantityTableItemId = table.Column<long>(type: "bigint", nullable: true),
            //        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SupplierTenderQuantityTableItem", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_SupplierTenderQuantityTableItem_SupplierTenderQuantityTable_SupplierTenderQuantityTableId",
            //            column: x => x.SupplierTenderQuantityTableId,
            //            principalSchema: "Offer",
            //            principalTable: "SupplierTenderQuantityTable",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_SupplierTenderQuantityTableItem_TenderQuantityTableItem_TenderQuantityTableItemId",
            //            column: x => x.TenderQuantityTableItemId,
            //            principalTable: "TenderQuantityTableItem",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 1,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 21, 13, 48, 14, 811, DateTimeKind.Local).AddTicks(5119));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 2,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 21, 13, 48, 14, 811, DateTimeKind.Local).AddTicks(6695));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 3,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 21, 13, 48, 14, 811, DateTimeKind.Local).AddTicks(6730));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 4,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 21, 13, 48, 14, 811, DateTimeKind.Local).AddTicks(6733));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 5,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 21, 13, 48, 14, 811, DateTimeKind.Local).AddTicks(6736));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 6,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 21, 13, 48, 14, 811, DateTimeKind.Local).AddTicks(6738));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 7,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 21, 13, 48, 14, 811, DateTimeKind.Local).AddTicks(6741));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 8,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 21, 13, 48, 14, 811, DateTimeKind.Local).AddTicks(6743));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 9,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 21, 13, 48, 14, 811, DateTimeKind.Local).AddTicks(6745));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 10,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 21, 13, 48, 14, 811, DateTimeKind.Local).AddTicks(6748));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 11,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 21, 13, 48, 14, 811, DateTimeKind.Local).AddTicks(6750));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 12,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 21, 13, 48, 14, 811, DateTimeKind.Local).AddTicks(6752));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 13,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 7, 21, 13, 48, 14, 811, DateTimeKind.Local).AddTicks(6755));

            //migrationBuilder.CreateIndex(
            //    name: "IX_SupplierTenderQuantityTableItem_SupplierTenderQuantityTableId",
            //    schema: "Offer",
            //    table: "SupplierTenderQuantityTableItem",
            //    column: "SupplierTenderQuantityTableId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SupplierTenderQuantityTableItem_TenderQuantityTableItemId",
            //    schema: "Offer",
            //    table: "SupplierTenderQuantityTableItem",
            //    column: "TenderQuantityTableItemId");
        }
    }
}
