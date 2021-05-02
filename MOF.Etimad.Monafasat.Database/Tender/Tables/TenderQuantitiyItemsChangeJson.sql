CREATE TABLE [Tender].[TenderQuantitiyItemsChangeJson] (
    [Id]                             BIGINT         IDENTITY (1, 1) NOT NULL,
    [TenderQuantityTableChangeId]    BIGINT         NOT NULL,
    [TenderQuantityTableItemChanges] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TenderQuantitiyItemsChangeJson] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenderQuantitiyItemsChangeJson_TenderQuantityTableChanges_TenderQuantityTableChangeId] FOREIGN KEY ([TenderQuantityTableChangeId]) REFERENCES [Tender].[TenderQuantityTableChanges] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TenderQuantitiyItemsChangeJson_TenderQuantityTableChangeId]
    ON [Tender].[TenderQuantitiyItemsChangeJson]([TenderQuantityTableChangeId] ASC);

