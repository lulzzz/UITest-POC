using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class Delete_Unused_Negotiation_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NegotiationQuantityTableItem",
                schema: "CommunicationRequest");

            migrationBuilder.DropTable(
                name: "NegotiationQuantityTable",
                schema: "CommunicationRequest");

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 1,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 14, 11, 37, 33, 94, DateTimeKind.Local).AddTicks(438));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 2,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 14, 11, 37, 33, 94, DateTimeKind.Local).AddTicks(2041));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 3,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 14, 11, 37, 33, 94, DateTimeKind.Local).AddTicks(2073));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 4,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 14, 11, 37, 33, 94, DateTimeKind.Local).AddTicks(2075));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 5,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 14, 11, 37, 33, 94, DateTimeKind.Local).AddTicks(2077));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 6,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 14, 11, 37, 33, 94, DateTimeKind.Local).AddTicks(2079));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 7,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 14, 11, 37, 33, 94, DateTimeKind.Local).AddTicks(2080));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 8,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 14, 11, 37, 33, 94, DateTimeKind.Local).AddTicks(2082));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 9,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 14, 11, 37, 33, 94, DateTimeKind.Local).AddTicks(2084));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 10,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 14, 11, 37, 33, 94, DateTimeKind.Local).AddTicks(2086));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 11,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 14, 11, 37, 33, 94, DateTimeKind.Local).AddTicks(2087));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 12,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 14, 11, 37, 33, 94, DateTimeKind.Local).AddTicks(2089));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 13,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 14, 11, 37, 33, 94, DateTimeKind.Local).AddTicks(2091));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NegotiationQuantityTable",
                schema: "CommunicationRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierQuantityTableId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    refNegotiationSecondStage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegotiationQuantityTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NegotiationQuantityTable_Negotiation_refNegotiationSecondStage",
                        column: x => x.refNegotiationSecondStage,
                        principalSchema: "CommunicationRequest",
                        principalTable: "Negotiation",
                        principalColumn: "NegotiationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NegotiationQuantityTableItem",
                schema: "CommunicationRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileRefId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NegotiationQuantityTableId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SupplierQuantityTableItemId = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    originalQTY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegotiationQuantityTableItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NegotiationQuantityTableItem_NegotiationQuantityTable_NegotiationQuantityTableId",
                        column: x => x.NegotiationQuantityTableId,
                        principalSchema: "CommunicationRequest",
                        principalTable: "NegotiationQuantityTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 8, 10, 16, 13, 27, 107, DateTimeKind.Local).AddTicks(9340));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(898));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(938));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(941));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(943));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(945));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(948));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(950));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(952));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(955));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(957));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(959));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(962));

            migrationBuilder.CreateIndex(
                name: "IX_NegotiationQuantityTable_refNegotiationSecondStage",
                schema: "CommunicationRequest",
                table: "NegotiationQuantityTable",
                column: "refNegotiationSecondStage");

            migrationBuilder.CreateIndex(
                name: "IX_NegotiationQuantityTableItem_NegotiationQuantityTableId",
                schema: "CommunicationRequest",
                table: "NegotiationQuantityTableItem",
                column: "NegotiationQuantityTableId");
        }
    }
}
