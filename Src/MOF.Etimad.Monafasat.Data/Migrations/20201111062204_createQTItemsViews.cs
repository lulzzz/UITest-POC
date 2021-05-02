using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class createQTItemsViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createSupplierView = @"CREATE VIEW [Offer].[VW_QTJsonItems]
                                AS
                                SELECT qt.suppliertenderquantitytableid AS QTableId, qi.Id AS Id, ColumnId, TenderFormHeaderId, ActivityTemplateId, ItemNumber, [Value], AlternativeValue, IsDefault
                                FROM [Offer].[SupplierTenderQuantityTableItemJson] qt CROSS APPLY OPENJSON(qt.[SupplierTenderQuantityTableItems]) WITH ([Id] bigint, ColumnId[bigint], TenderFormHeaderId[bigint], ActivityTemplateId[int], ItemNumber[bigint], 
                                Value nvarchar(max), AlternativeValue nvarchar(max), IsDefault bit) AS qi
                                GO";

            migrationBuilder.Sql(createSupplierView);
            var createTenderView = @"CREATE VIEW [Tender].[VW_TendersQTJsonItems]
                                AS
								 SELECT qt.[TenderQuantityTableId] AS QTableId, qi.Id AS Id, ColumnId, TenderFormHeaderId, ActivityTemplateId, ItemNumber, [Value], ColumnName
	                            FROM [Tender].[TenderQuantitiyItemsJson] qt CROSS APPLY OPENJSON(qt.[TenderQuantityTableItems]) WITH ([Id] bigint, ColumnId[bigint], TenderFormHeaderId[bigint], ActivityTemplateId[int], ItemNumber[bigint], 
	                            Value nvarchar(max), ColumnName nvarchar(max)) AS qi
                                GO";
            migrationBuilder.Sql(createTenderView);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropSupplierView = @"drop view [Offer].[VW_QTJsonItems]";
            migrationBuilder.Sql(dropSupplierView);
            var dropTenderView = @"drop view [Tender].[VW_TendersQTJsonItems]";
            migrationBuilder.Sql(dropTenderView);
        }
    }
}
