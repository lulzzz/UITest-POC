--1--SupplierTenderQuantityTable

DECLARE @getid CURSOR
DECLARE @table_id INT
DECLARE @json NVARCHAR(MAX)
SET @getid = CURSOR FOR
SELECT [Id]
  FROM [Offer].[SupplierTenderQuantityTable]

OPEN @getid
FETCH NEXT
FROM @getid INTO @table_id
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @json = (SELECT [Id]
      ,[ColumnId]
      ,[TenderFormHeaderId]
      ,[ActivityTemplateId]
      ,[TenderQuantityTableItemId]
      ,[ItemNumber]
      ,[Value]
      ,[AlternativeValue]
      ,[IsDefault]
  FROM [Offer].[SupplierTenderQuantityTableItem]
  WHERE IsActive = 1 AND [SupplierTenderQuantityTableId] = @table_id
  For JSON AUTO)
  select @table_id, @json
  IF EXISTS(SELECT * FROM [Offer].[SupplierTenderQuantityTableItemJson] WHERE [SupplierTenderQuantityTableId] = @table_id)
  BEGIN
  UPDATE [Offer].[SupplierTenderQuantityTableItemJson] SET SupplierTenderQuantityTableItems = @json WHERE SupplierTenderQuantityTableId = @table_id
  END
  ELSE
  BEGIN
  INSERT INTO [Offer].[SupplierTenderQuantityTableItemJson] (SupplierTenderQuantityTableItems, SupplierTenderQuantityTableId) VALUES (@json, @table_id)
  END
    FETCH NEXT
    FROM @getid INTO @table_id
END

CLOSE @getid
DEALLOCATE @getid

go
--2--TenderQuantityTable


DECLARE @getid CURSOR
DECLARE @table_id INT
DECLARE @json NVARCHAR(MAX)
SET @getid = CURSOR FOR
SELECT [Id]
  FROM [Tender].[TenderQuantityTable]
OPEN @getid
FETCH NEXT
FROM @getid INTO @table_id
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @json = (SELECT [Id]
      ,[ColumnId]
      ,[TenderFormHeaderId]
      ,[ActivityTemplateId]
      ,[ColumnName]
      ,[Value]
      ,[ItemNumber]
  FROM [Tender].[TenderQuantityTableItem]
  WHERE IsActive = 1 AND [TenderQuantityTableId] = @table_id
  For JSON AUTO)
  select @table_id, @json
  IF EXISTS(SELECT * FROM [Tender].[TenderQuantitiyItemsJson] WHERE [TenderQuantityTableId] = @table_id)
  BEGIN
  UPDATE [Tender].[TenderQuantitiyItemsJson] SET TenderQuantityTableItems = @json WHERE TenderQuantityTableId = @table_id
  END
  ELSE
  BEGIN
  INSERT INTO [Tender].[TenderQuantitiyItemsJson] (TenderQuantityTableItems, TenderQuantityTableId) VALUES (@json, @table_id)
  END
    FETCH NEXT
    FROM @getid INTO @table_id
END
CLOSE @getid
DEALLOCATE @getid
go
--3--TenderQuantityTableChanges


DECLARE @getid CURSOR
DECLARE @table_id INT
DECLARE @json NVARCHAR(MAX)
SET @getid = CURSOR FOR
SELECT [Id]
  FROM [Tender].[TenderQuantityTableChanges]
OPEN @getid
FETCH NEXT
FROM @getid INTO @table_id
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @json = (SELECT [Id]
      ,[ColumnId]
      ,[TenderFormHeaderId]
      ,[ActivityTemplateId]
      ,[ColumnName]
      ,[Value]
      ,[ItemNumber]
  FROM [Tender].[TenderQuantityTableItemChanges]
  WHERE IsActive = 1 AND [TenderQuantityTableChangesId] = @table_id
  For JSON AUTO)
  select @table_id, @json
  IF EXISTS(SELECT * FROM [Tender].[TenderQuantitiyItemsChangeJson] WHERE [TenderQuantityTableChangeId] = @table_id)
  BEGIN
  UPDATE [Tender].[TenderQuantitiyItemsChangeJson] SET TenderQuantityTableItemChanges = @json WHERE TenderQuantityTableChangeId = @table_id
  END
  ELSE
  BEGIN
  INSERT INTO [Tender].[TenderQuantitiyItemsChangeJson] (TenderQuantityTableItemChanges, TenderQuantityTableChangeId) VALUES (@json, @table_id)
  END
    FETCH NEXT
    FROM @getid INTO @table_id
END
CLOSE @getid
DEALLOCATE @getid


--4---NegotiationSupplierQuantityTable
go
DECLARE @getid CURSOR
DECLARE @table_id INT
DECLARE @json NVARCHAR(MAX)
SET @getid = CURSOR FOR
SELECT   [Id]
  FROM [CommunicationRequest].[NegotiationSupplierQuantityTable]

OPEN @getid
FETCH NEXT
FROM @getid INTO @table_id
WHILE @@FETCH_STATUS = 0
BEGIN
--SELECT @table_id;
    SET @json = ( SELECT [Id]
      ,[ColumnId]
      ,[TenderFormHeaderId]
      ,[ActivityTemplateId]
     ,[negotiationsupplierQuantityTableId]
      ,[ItemNumber]
      ,[Value] 
  FROM [CommunicationRequest].[NegotiationSupplierQuantityTableItem]
  WHERE IsActive = 1 AND [negotiationsupplierQuantityTableId] = @table_id
  For JSON AUTO)
  select @table_id, @json
  IF EXISTS(SELECT * FROM [CommunicationRequest].[NegotiationQuantityItemjSON] WHERE NegotiationSupplierQuantityTableId = @table_id)
  BEGIN
  UPDATE [CommunicationRequest].[NegotiationQuantityItemjSON]  SET  [NegotiationSupplierQuantityTableItems] = @json WHERE NegotiationSupplierQuantityTableId = @table_id
  END
  ELSE
  BEGIN
  INSERT INTO [CommunicationRequest].[NegotiationQuantityItemjSON](NegotiationSupplierQuantityTableItems, NegotiationSupplierQuantityTableId) VALUES (@json, @table_id)
  END
    FETCH NEXT
    FROM @getid INTO @table_id
END

CLOSE @getid
DEALLOCATE @getid