CREATE TABLE [Tender].[TenderQuantitableItems] (
    [CreatedAt]             DATETIME2 (7)  NOT NULL,
    [CreatedBy]             NVARCHAR (255) NULL,
    [UpdatedAt]             DATETIME2 (7)  NULL,
    [UpdatedBy]             NVARCHAR (255) NULL,
    [IsActive]              BIT            NULL,
    [Id]                    BIGINT         NOT NULL,
    [ColumnId]              BIGINT         NOT NULL,
    [TenderFormHeaderId]    BIGINT         NULL,
    [ActivityTemplateId]    INT            NULL,
    [ColumnName]            NVARCHAR (MAX) NULL,
    [Value]                 NVARCHAR (MAX) NULL,
    [ItemNumber]            BIGINT         NOT NULL,
    [TenderQuantityTableId] BIGINT         NOT NULL
);

