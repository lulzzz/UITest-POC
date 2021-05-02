CREATE TABLE [Tender].[BiddingRoundOffer] (
    [CreatedAt]           DATETIME2 (7)   NOT NULL,
    [CreatedBy]           NVARCHAR (256)  NULL,
    [UpdatedAt]           DATETIME2 (7)   NULL,
    [UpdatedBy]           NVARCHAR (256)  NULL,
    [IsActive]            BIT             NULL,
    [BiddingRoundOfferId] INT             IDENTITY (1, 1) NOT NULL,
    [BiddingRoundId]      INT             NOT NULL,
    [OfferId]             INT             NOT NULL,
    [OfferValue]          DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_BiddingRoundOffer] PRIMARY KEY CLUSTERED ([BiddingRoundOfferId] ASC),
    CONSTRAINT [FK_BiddingRoundOffer_BiddingRounds_BiddingRoundId] FOREIGN KEY ([BiddingRoundId]) REFERENCES [Tender].[BiddingRounds] ([BiddingRoundId]),
    CONSTRAINT [FK_BiddingRoundOffer_Offer_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offer].[Offer] ([OfferId])
);


GO
CREATE NONCLUSTERED INDEX [IX_BiddingRoundOffer_BiddingRoundId]
    ON [Tender].[BiddingRoundOffer]([BiddingRoundId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BiddingRoundOffer_OfferId]
    ON [Tender].[BiddingRoundOffer]([OfferId] ASC);

