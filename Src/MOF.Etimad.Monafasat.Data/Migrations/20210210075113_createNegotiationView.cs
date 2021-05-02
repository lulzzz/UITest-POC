using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class createNegotiationView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createSupplierView = @"CREATE VIEW [CommunicationRequest].[VW_NegotiationQTJsonItems]
                                    AS					 
	                                    SELECT qt.[NegotiationSupplierQuantityTableId] AS NegoTableId, qt.Id as QTableJsonId, qi.Id AS Id, ColumnId, TenderFormHeaderId, ActivityTemplateId, qi.negotiationsupplierQuantityTableId as [negotiationsupplierQTId], ItemNumber, [Value]
	                                    FROM [CommunicationRequest].[NegotiationQuantityItemJson] qt 
	                                    CROSS APPLY OPENJSON(qt.NegotiationSupplierQuantityTableItems) WITH ([Id] bigint, ColumnId[bigint], TenderFormHeaderId[bigint], ActivityTemplateId[int], negotiationsupplierQuantityTableId[bigint], ItemNumber[bigint], 
	                                    [Value] nvarchar(max)) AS qi
                                    GO";

            migrationBuilder.Sql(createSupplierView);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropSupplierView = @"drop view [CommunicationRequest].[VW_NegotiationQTJsonItems]";
            migrationBuilder.Sql(dropSupplierView);
        }
    }
}