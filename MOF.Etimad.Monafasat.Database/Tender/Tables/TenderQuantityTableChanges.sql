CREATE TABLE [Tender].[TenderQuantityTableChanges] (
    [CreatedAt]               DATETIME2 (7)  NOT NULL,
    [CreatedBy]               NVARCHAR (256) NULL,
    [UpdatedAt]               DATETIME2 (7)  NULL,
    [UpdatedBy]               NVARCHAR (256) NULL,
    [IsActive]                BIT            NULL,
    [Id]                      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]                    NVARCHAR (MAX) NOT NULL,
    [TenderChangeRequestId]   INT            NOT NULL,
    [TenderQuantitiesTableId] BIGINT         NULL,
    [TableChangeStatusId]     INT            NOT NULL,
    [FormId]                  INT            NOT NULL,
    CONSTRAINT [PK_TenderQuantityTableChanges] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenderQuantityTableChanges_TenderChangeRequest_TenderChangeRequestId] FOREIGN KEY ([TenderChangeRequestId]) REFERENCES [Tender].[TenderChangeRequest] ([TenderChangeRequestId]),
    CONSTRAINT [FK_TenderQuantityTableChanges_TenderQuantityTable_TenderQuantitiesTableId] FOREIGN KEY ([TenderQuantitiesTableId]) REFERENCES [Tender].[TenderQuantityTable] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderQuantityTableChanges_TenderChangeRequestId]
    ON [Tender].[TenderQuantityTableChanges]([TenderChangeRequestId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderQuantityTableChanges_TenderQuantitiesTableId]
    ON [Tender].[TenderQuantityTableChanges]([TenderQuantitiesTableId] ASC);

