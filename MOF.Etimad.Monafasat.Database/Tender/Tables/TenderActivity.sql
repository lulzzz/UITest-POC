CREATE TABLE [Tender].[TenderActivity] (
    [CreatedAt]        DATETIME2 (7)  NOT NULL,
    [CreatedBy]        NVARCHAR (256) NULL,
    [UpdatedAt]        DATETIME2 (7)  NULL,
    [UpdatedBy]        NVARCHAR (256) NULL,
    [IsActive]         BIT            NULL,
    [TenderActivityId] INT            IDENTITY (1, 1) NOT NULL,
    [ActivityId]       INT            NOT NULL,
    [TenderId]         INT            NOT NULL,
    CONSTRAINT [PK_TenderActivity] PRIMARY KEY CLUSTERED ([TenderActivityId] ASC),
    CONSTRAINT [FK_TenderActivity_Activity_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [LookUps].[Activity] ([ActivityId]),
    CONSTRAINT [FK_TenderActivity_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderActivity_ActivityId]
    ON [Tender].[TenderActivity]([ActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderActivity_TenderId]
    ON [Tender].[TenderActivity]([TenderId] ASC);

