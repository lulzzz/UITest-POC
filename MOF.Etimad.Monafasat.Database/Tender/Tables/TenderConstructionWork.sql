CREATE TABLE [Tender].[TenderConstructionWork] (
    [CreatedAt]          DATETIME2 (7)  NOT NULL,
    [CreatedBy]          NVARCHAR (256) NULL,
    [UpdatedAt]          DATETIME2 (7)  NULL,
    [UpdatedBy]          NVARCHAR (256) NULL,
    [IsActive]           BIT            NULL,
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [ConstructionWorkId] INT            NOT NULL,
    [TenderId]           INT            NOT NULL,
    CONSTRAINT [PK_TenderConstructionWork] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenderConstructionWork_ConstructionWork_ConstructionWorkId] FOREIGN KEY ([ConstructionWorkId]) REFERENCES [LookUps].[ConstructionWork] ([ConstructionWorkId]),
    CONSTRAINT [FK_TenderConstructionWork_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderConstructionWork_ConstructionWorkId]
    ON [Tender].[TenderConstructionWork]([ConstructionWorkId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderConstructionWork_TenderId]
    ON [Tender].[TenderConstructionWork]([TenderId] ASC);

