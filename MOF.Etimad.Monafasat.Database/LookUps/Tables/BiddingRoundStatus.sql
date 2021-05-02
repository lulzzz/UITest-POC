CREATE TABLE [LookUps].[BiddingRoundStatus] (
    [BiddingRoundStatusId] INT             NOT NULL,
    [Name]                 NVARCHAR (1024) NULL,
    CONSTRAINT [PK_BiddingRoundStatus] PRIMARY KEY CLUSTERED ([BiddingRoundStatusId] ASC)
);

