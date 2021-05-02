CREATE TABLE [Tender].[TenderUnitStatusesHistory] (
    [CreatedAt]                   DATETIME2 (7)   NOT NULL,
    [CreatedBy]                   NVARCHAR (256)  NULL,
    [UpdatedAt]                   DATETIME2 (7)   NULL,
    [UpdatedBy]                   NVARCHAR (256)  NULL,
    [IsActive]                    BIT             NULL,
    [TenderUnitStatusesHistoryId] INT             IDENTITY (1, 1) NOT NULL,
    [Comment]                     NVARCHAR (MAX)  NULL,
    [TenderId]                    INT             NOT NULL,
    [TenderUnitStatusId]          INT             NOT NULL,
    [TenderUnitUpdateTypeId]      INT             NULL,
    [EstimatedValue]              DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_TenderUnitStatusesHistory] PRIMARY KEY CLUSTERED ([TenderUnitStatusesHistoryId] ASC),
    CONSTRAINT [FK_TenderUnitStatusesHistory_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId]),
    CONSTRAINT [FK_TenderUnitStatusesHistory_TenderUnitStatus_TenderUnitStatusId] FOREIGN KEY ([TenderUnitStatusId]) REFERENCES [LookUps].[TenderUnitStatus] ([TenderUnitStatusId]),
    CONSTRAINT [FK_TenderUnitStatusesHistory_TenderUnitUpdateType_TenderUnitUpdateTypeId] FOREIGN KEY ([TenderUnitUpdateTypeId]) REFERENCES [LookUps].[TenderUnitUpdateType] ([TenderUnitUpdateTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderUnitStatusesHistory_TenderId]
    ON [Tender].[TenderUnitStatusesHistory]([TenderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderUnitStatusesHistory_TenderUnitStatusId]
    ON [Tender].[TenderUnitStatusesHistory]([TenderUnitStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderUnitStatusesHistory_TenderUnitUpdateTypeId]
    ON [Tender].[TenderUnitStatusesHistory]([TenderUnitUpdateTypeId] ASC);

