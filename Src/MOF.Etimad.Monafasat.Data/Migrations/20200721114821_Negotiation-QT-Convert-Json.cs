using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class NegotiationQTConvertJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "NegotiationSupplierQuantityTableItem",
            //    schema: "CommunicationRequest");

            migrationBuilder.CreateTable(
                name: "NegotiationQuantityItemJson",
                schema: "CommunicationRequest",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NegotiationSupplierQuantityTableId = table.Column<long>(nullable: false),
                    NegotiationSupplierQuantityTableItems = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegotiationQuantityItemJson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NegotiationQuantityItemJson_NegotiationSupplierQuantityTable_NegotiationSupplierQuantityTableId",
                        column: x => x.NegotiationSupplierQuantityTableId,
                        principalSchema: "CommunicationRequest",
                        principalTable: "NegotiationSupplierQuantityTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateIndex(
                name: "IX_NegotiationQuantityItemJson_NegotiationSupplierQuantityTableId",
                schema: "CommunicationRequest",
                table: "NegotiationQuantityItemJson",
                column: "NegotiationSupplierQuantityTableId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NegotiationQuantityItemJson",
                schema: "CommunicationRequest");

            //migrationBuilder.CreateTable(
            //    name: "NegotiationSupplierQuantityTableItem",
            //    schema: "CommunicationRequest",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ActivityTemplateId = table.Column<int>(type: "int", nullable: true),
            //        ColumnId = table.Column<long>(type: "bigint", nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        IsActive = table.Column<bool>(type: "bit", nullable: true),
            //        ItemNumber = table.Column<long>(type: "bigint", nullable: false),
            //        NegotiationSupplierQuantityTableId = table.Column<long>(type: "bigint", nullable: false),
            //        TenderFormHeaderId = table.Column<long>(type: "bigint", nullable: true),
            //        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NegotiationSupplierQuantityTableItem", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_NegotiationSupplierQuantityTableItem_NegotiationSupplierQuantityTable_NegotiationSupplierQuantityTableId",
            //            column: x => x.NegotiationSupplierQuantityTableId,
            //            principalSchema: "CommunicationRequest",
            //            principalTable: "NegotiationSupplierQuantityTable",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_NegotiationSupplierQuantityTableItem_NegotiationSupplierQuantityTableId",
                schema: "CommunicationRequest",
                table: "NegotiationSupplierQuantityTableItem",
                column: "NegotiationSupplierQuantityTableId");
        }
    }
}
