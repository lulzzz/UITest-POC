
 CREATE VIEW [Offer].[VW_QTJsonItems]
                                AS
                                SELECT qt.suppliertenderquantitytableid AS QTableId, qt.Id as QTableItemsId, qi.Id AS Id, ColumnId, TenderFormHeaderId, ActivityTemplateId, ItemNumber, [Value], AlternativeValue, IsDefault
                                FROM [Offer].[SupplierTenderQuantityTableItemJson] qt CROSS APPLY OPENJSON(qt.[SupplierTenderQuantityTableItems]) WITH ([Id] bigint, ColumnId[bigint], TenderFormHeaderId[bigint], ActivityTemplateId[int], ItemNumber[bigint], 
                                Value nvarchar(max), AlternativeValue nvarchar(max), IsDefault bit) AS qi

