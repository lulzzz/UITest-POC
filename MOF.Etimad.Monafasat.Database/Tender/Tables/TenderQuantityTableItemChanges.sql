CREATE TABLE [Tender].[TenderQuantityTableItemChanges] (
    [CreatedAt]                    DATETIME2 (7)  NOT NULL,
    [CreatedBy]                    NVARCHAR (256) NULL,
    [UpdatedAt]                    DATETIME2 (7)  NULL,
    [UpdatedBy]                    NVARCHAR (256) NULL,
    [IsActive]                     BIT            NULL,
    [Id]                           BIGINT         IDENTITY (1, 1) NOT NULL,
    [ColumnId]                     BIGINT         NOT NULL,
    [TenderFormHeaderId]           BIGINT         NULL,
    [ActivityTemplateId]           INT            NULL,
    [ColumnName]                   NVARCHAR (MAX) NULL,
    [Value]                        NVARCHAR (MAX) NULL,
    [ItemNumber]                   BIGINT         NOT NULL,
    [TenderQuantityTableChangesId] BIGINT         NOT NULL,
    CONSTRAINT [PK_TenderQuantityTableItemChanges] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenderQuantityTableItemChanges_TenderQuantityTableChanges_TenderQuantityTableChangesId] FOREIGN KEY ([TenderQuantityTableChangesId]) REFERENCES [Tender].[TenderQuantityTableChanges] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderQuantityTableItemChanges_TenderQuantityTableChangesId]
    ON [Tender].[TenderQuantityTableItemChanges]([TenderQuantityTableChangesId] ASC);

