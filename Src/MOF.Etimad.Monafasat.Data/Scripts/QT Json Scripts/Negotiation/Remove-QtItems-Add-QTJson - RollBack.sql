DROP TABLE [CommunicationRequest].[NegotiationQuantityItemJson];

GO

--CREATE TABLE [CommunicationRequest].[NegotiationSupplierQuantityTableItem] (
--    [Id] bigint NOT NULL IDENTITY,
--    [ActivityTemplateId] int NULL,
--    [ColumnId] bigint NOT NULL,
--    [CreatedAt] datetime2 NOT NULL,
--    [CreatedBy] nvarchar(256) NULL,
--    [IsActive] bit NULL,
--    [ItemNumber] bigint NOT NULL,
--    [NegotiationSupplierQuantityTableId] bigint NOT NULL,
--    [TenderFormHeaderId] bigint NULL,
--    [UpdatedAt] datetime2 NULL,
--    [UpdatedBy] nvarchar(256) NULL,
--    [Value] nvarchar(max) NULL,
--    CONSTRAINT [PK_NegotiationSupplierQuantityTableItem] PRIMARY KEY ([Id]),
--    CONSTRAINT [FK_NegotiationSupplierQuantityTableItem_NegotiationSupplierQuantityTable_NegotiationSupplierQuantityTableId] FOREIGN KEY ([NegotiationSupplierQuantityTableId]) REFERENCES [CommunicationRequest].[NegotiationSupplierQuantityTable] ([Id]) ON DELETE NO ACTION
--);

--GO

CREATE INDEX [IX_NegotiationSupplierQuantityTableItem_NegotiationSupplierQuantityTableId] ON [CommunicationRequest].[NegotiationSupplierQuantityTableItem] ([NegotiationSupplierQuantityTableId]);

GO
ALTER TABLE [CommunicationRequest].[NegotiationSupplierQuantityTableItem]
ADD CONSTRAINT FK_NegotiationSupplierQuantityTableItem_NegotiationSupplierQuantityTable_NegotiationSupplierQuantityTableId
FOREIGN KEY ([NegotiationSupplierQuantityTableId]) REFERENCES  [CommunicationRequest].[NegotiationSupplierQuantityTable](Id);
go


DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200721114821_Negotiation-QT-Convert-Json';

GO

