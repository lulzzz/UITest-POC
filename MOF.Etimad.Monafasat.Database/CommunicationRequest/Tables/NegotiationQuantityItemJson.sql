CREATE TABLE [CommunicationRequest].[NegotiationQuantityItemJson] (
    [Id]                                    BIGINT         IDENTITY (1, 1) NOT NULL,
    [NegotiationSupplierQuantityTableId]    BIGINT         NOT NULL,
    [NegotiationSupplierQuantityTableItems] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_NegotiationQuantityItemJson] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NegotiationQuantityItemJson_NegotiationSupplierQuantityTable_NegotiationSupplierQuantityTableId] FOREIGN KEY ([NegotiationSupplierQuantityTableId]) REFERENCES [CommunicationRequest].[NegotiationSupplierQuantityTable] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_NegotiationQuantityItemJson_NegotiationSupplierQuantityTableId]
    ON [CommunicationRequest].[NegotiationQuantityItemJson]([NegotiationSupplierQuantityTableId] ASC);

