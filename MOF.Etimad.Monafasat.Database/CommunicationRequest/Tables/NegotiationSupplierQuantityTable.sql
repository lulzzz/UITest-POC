CREATE TABLE [CommunicationRequest].[NegotiationSupplierQuantityTable] (
    [CreatedAt]                 DATETIME2 (7)  NOT NULL,
    [CreatedBy]                 NVARCHAR (256) NULL,
    [UpdatedAt]                 DATETIME2 (7)  NULL,
    [UpdatedBy]                 NVARCHAR (256) NULL,
    [IsActive]                  BIT            NULL,
    [Id]                        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]                      NVARCHAR (MAX) NOT NULL,
    [refNegotiationSecondStage] INT            NOT NULL,
    [SupplierQuantityTableId]   BIGINT         NOT NULL,
    CONSTRAINT [PK_NegotiationSupplierQuantityTable] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NegotiationSupplierQuantityTable_Negotiation_refNegotiationSecondStage] FOREIGN KEY ([refNegotiationSecondStage]) REFERENCES [CommunicationRequest].[Negotiation] ([NegotiationId]),
    CONSTRAINT [FK_NegotiationSupplierQuantityTable_SupplierTenderQuantityTable_SupplierQuantityTableId] FOREIGN KEY ([SupplierQuantityTableId]) REFERENCES [Offer].[SupplierTenderQuantityTable] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_NegotiationSupplierQuantityTable_SupplierQuantityTableId]
    ON [CommunicationRequest].[NegotiationSupplierQuantityTable]([SupplierQuantityTableId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_NegotiationSupplierQuantityTable_refNegotiationSecondStage]
    ON [CommunicationRequest].[NegotiationSupplierQuantityTable]([refNegotiationSecondStage] ASC);

