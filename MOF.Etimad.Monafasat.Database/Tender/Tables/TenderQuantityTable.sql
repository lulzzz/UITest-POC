CREATE TABLE [Tender].[TenderQuantityTable] (
    [CreatedAt] DATETIME2 (7)  NOT NULL,
    [CreatedBy] NVARCHAR (256) NULL,
    [UpdatedAt] DATETIME2 (7)  NULL,
    [UpdatedBy] NVARCHAR (256) NULL,
    [IsActive]  BIT            NULL,
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [TenderId]  INT            NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [FormId]    INT            NOT NULL,
    CONSTRAINT [PK_TenderQuantityTable] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenderQuantityTable_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderQuantityTable_TenderId]
    ON [Tender].[TenderQuantityTable]([TenderId] ASC);

