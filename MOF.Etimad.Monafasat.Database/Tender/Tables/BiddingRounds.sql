CREATE TABLE [Tender].[BiddingRounds] (
    [CreatedAt]            DATETIME2 (7)  NOT NULL,
    [CreatedBy]            NVARCHAR (256) NULL,
    [UpdatedAt]            DATETIME2 (7)  NULL,
    [UpdatedBy]            NVARCHAR (256) NULL,
    [IsActive]             BIT            NULL,
    [BiddingRoundId]       INT            IDENTITY (1, 1) NOT NULL,
    [StartDate]            DATETIME2 (7)  NOT NULL,
    [EndDate]              DATETIME2 (7)  NOT NULL,
    [BiddingRoundStatusId] INT            NOT NULL,
    [TenderId]             INT            NOT NULL,
    CONSTRAINT [PK_BiddingRounds] PRIMARY KEY CLUSTERED ([BiddingRoundId] ASC),
    CONSTRAINT [FK_BiddingRounds_BiddingRoundStatus_BiddingRoundStatusId] FOREIGN KEY ([BiddingRoundStatusId]) REFERENCES [LookUps].[BiddingRoundStatus] ([BiddingRoundStatusId]),
    CONSTRAINT [FK_BiddingRounds_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_BiddingRounds_BiddingRoundStatusId]
    ON [Tender].[BiddingRounds]([BiddingRoundStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BiddingRounds_TenderId]
    ON [Tender].[BiddingRounds]([TenderId] ASC);

