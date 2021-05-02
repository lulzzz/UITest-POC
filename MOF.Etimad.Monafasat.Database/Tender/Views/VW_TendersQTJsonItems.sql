

CREATE VIEW [Tender].[VW_TendersQTJsonItems]
                                AS
								 SELECT qt.[TenderQuantityTableId] AS QTableId, qt.Id as QTableItemsId, qi.Id AS Id, ColumnId, TenderFormHeaderId, ActivityTemplateId, ItemNumber, [Value], ColumnName
	                            FROM [Tender].[TenderQuantitiyItemsJson] qt CROSS APPLY OPENJSON(qt.[TenderQuantityTableItems]) WITH ([Id] bigint, ColumnId[bigint], TenderFormHeaderId[bigint], ActivityTemplateId[int], ItemNumber[bigint], 
	                            Value nvarchar(max), ColumnName nvarchar(max)) AS qi

