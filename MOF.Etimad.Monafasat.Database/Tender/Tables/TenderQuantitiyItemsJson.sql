CREATE TABLE [Tender].[TenderQuantitiyItemsJson] (
    [Id]                       BIGINT         IDENTITY (1, 1) NOT NULL,
    [TenderQuantityTableId]    BIGINT         NOT NULL,
    [TenderQuantityTableItems] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TenderQuantitiyItemsJson] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenderQuantitiyItemsJson_TenderQuantityTable_TenderQuantityTableId] FOREIGN KEY ([TenderQuantityTableId]) REFERENCES [Tender].[TenderQuantityTable] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TenderQuantitiyItemsJson_TenderQuantityTableId]
    ON [Tender].[TenderQuantitiyItemsJson]([TenderQuantityTableId] ASC);

