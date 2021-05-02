--DROP TABLE [CommunicationRequest].[NegotiationSupplierQuantityTableItem];

GO

CREATE TABLE [CommunicationRequest].[NegotiationQuantityItemJson] (
    [Id] bigint NOT NULL IDENTITY,
    [NegotiationSupplierQuantityTableId] bigint NOT NULL,
    [NegotiationSupplierQuantityTableItems] nvarchar(max) NULL,
    CONSTRAINT [PK_NegotiationQuantityItemJson] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NegotiationQuantityItemJson_NegotiationSupplierQuantityTable_NegotiationSupplierQuantityTableId] FOREIGN KEY ([NegotiationSupplierQuantityTableId]) REFERENCES [CommunicationRequest].[NegotiationSupplierQuantityTable] ([Id]) ON DELETE NO ACTION
);

GO

CREATE UNIQUE INDEX [IX_NegotiationQuantityItemJson_NegotiationSupplierQuantityTableId] ON [CommunicationRequest].[NegotiationQuantityItemJson] ([NegotiationSupplierQuantityTableId]);

GO

alter table [CommunicationRequest].[NegotiationSupplierQuantityTableItem]
drop   
FK_NegotiationSupplierQuantityTableItem_NegotiationSupplierQuantityTable_NegotiationSupplierQuantityTableId

go
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200721114821_Negotiation-QT-Convert-Json', N'3.1.0');

GO

