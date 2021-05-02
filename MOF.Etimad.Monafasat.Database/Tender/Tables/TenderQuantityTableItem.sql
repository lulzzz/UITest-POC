CREATE TABLE [Tender].[TenderQuantityTableItem] (
    [CreatedAt]             DATETIME2 (7)  NOT NULL,
    [CreatedBy]             NVARCHAR (256) NULL,
    [UpdatedAt]             DATETIME2 (7)  NULL,
    [UpdatedBy]             NVARCHAR (256) NULL,
    [IsActive]              BIT            NULL,
    [Id]                    BIGINT         IDENTITY (1, 1) NOT NULL,
    [ColumnId]              BIGINT         NOT NULL,
    [TenderFormHeaderId]    BIGINT         NULL,
    [ActivityTemplateId]    INT            NULL,
    [ColumnName]            NVARCHAR (MAX) NULL,
    [Value]                 NVARCHAR (MAX) NULL,
    [ItemNumber]            BIGINT         NOT NULL,
    [TenderQuantityTableId] BIGINT         NOT NULL,
    CONSTRAINT [PK_TenderQuantityTableItem] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderQuantityTableItem_TenderQuantityTableId]
    ON [Tender].[TenderQuantityTableItem]([TenderQuantityTableId] ASC);

