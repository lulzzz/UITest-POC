CREATE TABLE [CommunicationRequest].[NegotiationSupplierQuantityTableItem] (
    [CreatedAt]                          DATETIME2 (7)  NOT NULL,
    [CreatedBy]                          NVARCHAR (256) NULL,
    [UpdatedAt]                          DATETIME2 (7)  NULL,
    [UpdatedBy]                          NVARCHAR (256) NULL,
    [IsActive]                           BIT            NULL,
    [Id]                                 BIGINT         IDENTITY (1, 1) NOT NULL,
    [ColumnId]                           BIGINT         NOT NULL,
    [TenderFormHeaderId]                 BIGINT         NULL,
    [ActivityTemplateId]                 INT            NULL,
    [NegotiationSupplierQuantityTableId] BIGINT         NOT NULL,
    [ItemNumber]                         BIGINT         NOT NULL,
    [Value]                              NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_NegotiationSupplierQuantityTableItem] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_NegotiationSupplierQuantityTableItem_NegotiationSupplierQuantityTableId]
    ON [CommunicationRequest].[NegotiationSupplierQuantityTableItem]([NegotiationSupplierQuantityTableId] ASC);

