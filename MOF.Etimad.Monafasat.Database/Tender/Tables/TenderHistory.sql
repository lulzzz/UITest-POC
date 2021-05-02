CREATE TABLE [Tender].[TenderHistory] (
    [CreatedAt]       DATETIME2 (7)   NOT NULL,
    [CreatedBy]       NVARCHAR (256)  NULL,
    [UpdatedAt]       DATETIME2 (7)   NULL,
    [UpdatedBy]       NVARCHAR (256)  NULL,
    [IsActive]        BIT             NULL,
    [TenderHistoryId] INT             IDENTITY (1, 1) NOT NULL,
    [UserId]          INT             NOT NULL,
    [TenderId]        INT             NOT NULL,
    [StatusId]        INT             NOT NULL,
    [RejectionReason] NVARCHAR (2000) NULL,
    [ActionId]        INT             NOT NULL,
    CONSTRAINT [PK_TenderHistory] PRIMARY KEY CLUSTERED ([TenderHistoryId] ASC),
    CONSTRAINT [FK_TenderHistory_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId]),
    CONSTRAINT [FK_TenderHistory_TenderAction_ActionId] FOREIGN KEY ([ActionId]) REFERENCES [LookUps].[TenderAction] ([TenderActionId]),
    CONSTRAINT [FK_TenderHistory_TenderStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[TenderStatus] ([TenderStatusId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderHistory_ActionId]
    ON [Tender].[TenderHistory]([ActionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderHistory_StatusId]
    ON [Tender].[TenderHistory]([StatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderHistory_TenderId]
    ON [Tender].[TenderHistory]([TenderId] ASC);

