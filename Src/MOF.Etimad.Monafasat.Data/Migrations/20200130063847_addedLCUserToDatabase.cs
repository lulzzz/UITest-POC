using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class addedLCUserToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "UserRole",
                columns: new[] { "UserRoleId", "DisplayNameAr", "DisplayNameEn", "Name" },
                values: new object[] { 43, "مسؤول متطلبات المحتوى المحلي", "Local Content Officer", "NewMonafasat_LocalContentOfficer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 43);

            migrationBuilder.CreateTable(
                name: "QuantitiesTable",
                schema: "Tender",
                columns: table => new
                {
                    QuantitiesTableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    TenderId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantitiesTable", x => x.QuantitiesTableId);
                    table.ForeignKey(
                        name: "FK_QuantitiesTable_Tender_TenderId",
                        column: x => x.TenderId,
                        principalSchema: "Tender",
                        principalTable: "Tender",
                        principalColumn: "TenderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierQuantityTable",
                schema: "Offer",
                columns: table => new
                {
                    TableQuantityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdjustedTotalDiscount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdjustedTotalPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdjustedTotalVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AlternativeFinalPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternativeTotalDiscount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternativeTotalPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternativeTotalVAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckingFinalPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FinalPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    TableQuantityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenderQuantityTableId = table.Column<int>(type: "int", nullable: true),
                    TotalDiscount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierQuantityTable", x => x.TableQuantityId);
                    table.ForeignKey(
                        name: "FK_SupplierQuantityTable_Offer_OfferId",
                        column: x => x.OfferId,
                        principalSchema: "Offer",
                        principalTable: "Offer",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierQuantityTable_QuantitiesTable_TenderQuantityTableId",
                        column: x => x.TenderQuantityTableId,
                        principalSchema: "Tender",
                        principalTable: "QuantitiesTable",
                        principalColumn: "QuantitiesTableId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuantitiesTableChanges",
                schema: "Tender",
                columns: table => new
                {
                    QuantitiesTableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    TableChangeStatusId = table.Column<int>(type: "int", nullable: false),
                    TenderChangeRequestId = table.Column<int>(type: "int", nullable: false),
                    TenderQuantitiesTableId = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantitiesTableChanges", x => x.QuantitiesTableId);
                    table.ForeignKey(
                        name: "FK_QuantitiesTableChanges_TenderChangeRequest_TenderChangeRequestId",
                        column: x => x.TenderChangeRequestId,
                        principalSchema: "Tender",
                        principalTable: "TenderChangeRequest",
                        principalColumn: "TenderChangeRequestId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuantitiesTableChanges_QuantitiesTable_TenderQuantitiesTableId",
                        column: x => x.TenderQuantitiesTableId,
                        principalSchema: "Tender",
                        principalTable: "QuantitiesTable",
                        principalColumn: "QuantitiesTableId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuantityTableItem",
                schema: "Tender",
                columns: table => new
                {
                    QuantityTableItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ItemAttachmentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemAttachmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QuantityTableID = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityTableItem", x => x.QuantityTableItemId);
                    table.ForeignKey(
                        name: "FK_QuantityTableItem_QuantitiesTable_QuantityTableID",
                        column: x => x.QuantityTableID,
                        principalSchema: "Tender",
                        principalTable: "QuantitiesTable",
                        principalColumn: "QuantitiesTableId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierQuantityTableTemp",
                schema: "Supplier",
                columns: table => new
                {
                    TableQuantityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    SupplierQuantityTableId = table.Column<int>(type: "int", nullable: false),
                    TableQuantityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPriceAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierQuantityTableTemp", x => x.TableQuantityId);
                    table.ForeignKey(
                        name: "FK_SupplierQuantityTableTemp_Offer_OfferId",
                        column: x => x.OfferId,
                        principalSchema: "Offer",
                        principalTable: "Offer",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierQuantityTableTemp_SupplierQuantityTable_SupplierQuantityTableId",
                        column: x => x.SupplierQuantityTableId,
                        principalSchema: "Offer",
                        principalTable: "SupplierQuantityTable",
                        principalColumn: "TableQuantityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuantitiesTableItemsChanges",
                schema: "Tender",
                columns: table => new
                {
                    QuantityTableItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ItemAttachmentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemAttachmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemChangeStatusId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QuantityTableID = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantitiesTableItemsChanges", x => x.QuantityTableItemId);
                    table.ForeignKey(
                        name: "FK_QuantitiesTableItemsChanges_QuantitiesTableChanges_QuantityTableID",
                        column: x => x.QuantityTableID,
                        principalSchema: "Tender",
                        principalTable: "QuantitiesTableChanges",
                        principalColumn: "QuantitiesTableId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierQuantityTableItem",
                schema: "Offer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdjustedDiscount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdjustedPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdjustedVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Discount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ItemAttachmentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemAttachmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalItemId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SupplierTableQuantityId = table.Column<int>(type: "int", nullable: false),
                    TenderQuantityTableItemId = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    isSelected = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierQuantityTableItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierQuantityTableItem_SupplierQuantityTableItem_OriginalItemId",
                        column: x => x.OriginalItemId,
                        principalSchema: "Offer",
                        principalTable: "SupplierQuantityTableItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierQuantityTableItem_SupplierQuantityTable_SupplierTableQuantityId",
                        column: x => x.SupplierTableQuantityId,
                        principalSchema: "Offer",
                        principalTable: "SupplierQuantityTable",
                        principalColumn: "TableQuantityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierQuantityTableItem_QuantityTableItem_TenderQuantityTableItemId",
                        column: x => x.TenderQuantityTableItemId,
                        principalSchema: "Tender",
                        principalTable: "QuantityTableItem",
                        principalColumn: "QuantityTableItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierQuantityTableItemsTemp",
                schema: "Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ItemAttachmentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemAttachmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SupplierQuantityTableTempId = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierQuantityTableItemsTemp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierQuantityTableItemsTemp_SupplierQuantityTableTemp_SupplierQuantityTableTempId",
                        column: x => x.SupplierQuantityTableTempId,
                        principalSchema: "Supplier",
                        principalTable: "SupplierQuantityTableTemp",
                        principalColumn: "TableQuantityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 29, 12, 43, 47, 897, DateTimeKind.Local).AddTicks(560));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 29, 12, 43, 47, 897, DateTimeKind.Local).AddTicks(2089));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 29, 12, 43, 47, 897, DateTimeKind.Local).AddTicks(2127));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 29, 12, 43, 47, 897, DateTimeKind.Local).AddTicks(2130));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 29, 12, 43, 47, 897, DateTimeKind.Local).AddTicks(2132));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 29, 12, 43, 47, 897, DateTimeKind.Local).AddTicks(2134));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 29, 12, 43, 47, 897, DateTimeKind.Local).AddTicks(2135));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 29, 12, 43, 47, 897, DateTimeKind.Local).AddTicks(2137));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 29, 12, 43, 47, 897, DateTimeKind.Local).AddTicks(2139));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 29, 12, 43, 47, 897, DateTimeKind.Local).AddTicks(2140));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 29, 12, 43, 47, 897, DateTimeKind.Local).AddTicks(2142));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 29, 12, 43, 47, 897, DateTimeKind.Local).AddTicks(2144));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 29, 12, 43, 47, 897, DateTimeKind.Local).AddTicks(2146));

            migrationBuilder.CreateIndex(
                name: "IX_NegotiationQuantityTableItem_SupplierQuantityTableItemId",
                schema: "CommunicationRequest",
                table: "NegotiationQuantityTableItem",
                column: "SupplierQuantityTableItemId");

            migrationBuilder.CreateIndex(
                name: "IX_NegotiationQuantityTable_SupplierQuantityTableId",
                schema: "CommunicationRequest",
                table: "NegotiationQuantityTable",
                column: "SupplierQuantityTableId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierQuantityTable_OfferId",
                schema: "Offer",
                table: "SupplierQuantityTable",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierQuantityTable_TenderQuantityTableId",
                schema: "Offer",
                table: "SupplierQuantityTable",
                column: "TenderQuantityTableId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierQuantityTableItem_OriginalItemId",
                schema: "Offer",
                table: "SupplierQuantityTableItem",
                column: "OriginalItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierQuantityTableItem_SupplierTableQuantityId",
                schema: "Offer",
                table: "SupplierQuantityTableItem",
                column: "SupplierTableQuantityId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierQuantityTableItem_TenderQuantityTableItemId",
                schema: "Offer",
                table: "SupplierQuantityTableItem",
                column: "TenderQuantityTableItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierQuantityTableItemsTemp_SupplierQuantityTableTempId",
                schema: "Supplier",
                table: "SupplierQuantityTableItemsTemp",
                column: "SupplierQuantityTableTempId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierQuantityTableTemp_OfferId",
                schema: "Supplier",
                table: "SupplierQuantityTableTemp",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierQuantityTableTemp_SupplierQuantityTableId",
                schema: "Supplier",
                table: "SupplierQuantityTableTemp",
                column: "SupplierQuantityTableId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantitiesTable_TenderId",
                schema: "Tender",
                table: "QuantitiesTable",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantitiesTableChanges_TenderChangeRequestId",
                schema: "Tender",
                table: "QuantitiesTableChanges",
                column: "TenderChangeRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantitiesTableChanges_TenderQuantitiesTableId",
                schema: "Tender",
                table: "QuantitiesTableChanges",
                column: "TenderQuantitiesTableId");

            migrationBuilder.CreateIndex(
                name: "IX_QuantitiesTableItemsChanges_QuantityTableID",
                schema: "Tender",
                table: "QuantitiesTableItemsChanges",
                column: "QuantityTableID");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityTableItem_QuantityTableID",
                schema: "Tender",
                table: "QuantityTableItem",
                column: "QuantityTableID");

            migrationBuilder.AddForeignKey(
                name: "FK_NegotiationQuantityTable_SupplierQuantityTable_SupplierQuantityTableId",
                schema: "CommunicationRequest",
                table: "NegotiationQuantityTable",
                column: "SupplierQuantityTableId",
                principalSchema: "Offer",
                principalTable: "SupplierQuantityTable",
                principalColumn: "TableQuantityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NegotiationQuantityTableItem_QuantityTableItem_SupplierQuantityTableItemId",
                schema: "CommunicationRequest",
                table: "NegotiationQuantityTableItem",
                column: "SupplierQuantityTableItemId",
                principalSchema: "Tender",
                principalTable: "QuantityTableItem",
                principalColumn: "QuantityTableItemId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
