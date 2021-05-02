CREATE TABLE [Qualification].[QualificationConfiguration] (
    [CreatedAt]           DATETIME2 (7)   NOT NULL,
    [CreatedBy]           NVARCHAR (256)  NULL,
    [UpdatedAt]           DATETIME2 (7)   NULL,
    [UpdatedBy]           NVARCHAR (256)  NULL,
    [IsActive]            BIT             NULL,
    [ID]                  INT             IDENTITY (1, 1) NOT NULL,
    [TenderId]            INT             NOT NULL,
    [QualificationItemId] INT             NOT NULL,
    [Min]                 DECIMAL (18, 2) NOT NULL,
    [Max]                 DECIMAL (18, 2) NOT NULL,
    [Value]               DECIMAL (18, 2) NOT NULL,
    [Weight]              DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_QualificationConfiguration] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_QualificationConfiguration_QualificationItem_QualificationItemId] FOREIGN KEY ([QualificationItemId]) REFERENCES [Qualification].[QualificationItem] ([ID]),
    CONSTRAINT [FK_QualificationConfiguration_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationConfiguration_QualificationItemId]
    ON [Qualification].[QualificationConfiguration]([QualificationItemId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationConfiguration_TenderId]
    ON [Qualification].[QualificationConfiguration]([TenderId] ASC);

