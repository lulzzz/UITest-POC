CREATE TABLE [Tender].[TenderArea] (
    [CreatedAt] DATETIME2 (7)  NOT NULL,
    [CreatedBy] NVARCHAR (256) NULL,
    [UpdatedAt] DATETIME2 (7)  NULL,
    [UpdatedBy] NVARCHAR (256) NULL,
    [IsActive]  BIT            NULL,
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [AreaId]    INT            NOT NULL,
    [TenderId]  INT            NOT NULL,
    CONSTRAINT [PK_TenderArea] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenderArea_Area_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [LookUps].[Area] ([AreaId]),
    CONSTRAINT [FK_TenderArea_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderArea_AreaId]
    ON [Tender].[TenderArea]([AreaId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderArea_TenderId]
    ON [Tender].[TenderArea]([TenderId] ASC);

