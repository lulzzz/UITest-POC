CREATE TABLE [Offer].[OfferHistory] (
    [CreatedAt]                        DATETIME2 (7)   NOT NULL,
    [CreatedBy]                        NVARCHAR (256)  NULL,
    [UpdatedAt]                        DATETIME2 (7)   NULL,
    [UpdatedBy]                        NVARCHAR (256)  NULL,
    [IsActive]                         BIT             NULL,
    [OfferHistoryId]                   INT             IDENTITY (1, 1) NOT NULL,
    [UserId]                           INT             NOT NULL,
    [TenderId]                         INT             NOT NULL,
    [CommericalRegisterNo]             NVARCHAR (MAX)  NULL,
    [OfferId]                          INT             NOT NULL,
    [TenderStatusId]                   INT             NOT NULL,
    [OfferStatusId]                    INT             NOT NULL,
    [OfferAcceptanceStatusId]          INT             NULL,
    [OfferTechnicalEvaluationStatusId] INT             NULL,
    [RejectionReason]                  NVARCHAR (2000) NULL,
    [ActionId]                         INT             NOT NULL,
    CONSTRAINT [PK_OfferHistory] PRIMARY KEY CLUSTERED ([OfferHistoryId] ASC),
    CONSTRAINT [FK_OfferHistory_Offer_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offer].[Offer] ([OfferId]),
    CONSTRAINT [FK_OfferHistory_OfferStatus_OfferStatusId] FOREIGN KEY ([OfferStatusId]) REFERENCES [LookUps].[OfferStatus] ([OfferStatusId]),
    CONSTRAINT [FK_OfferHistory_TenderAction_ActionId] FOREIGN KEY ([ActionId]) REFERENCES [LookUps].[TenderAction] ([TenderActionId]),
    CONSTRAINT [FK_OfferHistory_TenderStatus_TenderStatusId] FOREIGN KEY ([TenderStatusId]) REFERENCES [LookUps].[TenderStatus] ([TenderStatusId])
);


GO
CREATE NONCLUSTERED INDEX [IX_OfferHistory_ActionId]
    ON [Offer].[OfferHistory]([ActionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OfferHistory_OfferId]
    ON [Offer].[OfferHistory]([OfferId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OfferHistory_OfferStatusId]
    ON [Offer].[OfferHistory]([OfferStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OfferHistory_TenderStatusId]
    ON [Offer].[OfferHistory]([TenderStatusId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Represent  Offer Data ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Unique identifer Of Offer Presentation Way lookup', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferHistory', @level2type = N'COLUMN', @level2name = N'OfferHistoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Id of related Tender', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferHistory', @level2type = N'COLUMN', @level2name = N'TenderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Commerical Register Number for the owner supplier for the offer ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferHistory', @level2type = N'COLUMN', @level2name = N'CommericalRegisterNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the related offer', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferHistory', @level2type = N'COLUMN', @level2name = N'OfferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the status of offer like (under establishment,Sent,cancelled ...etc )', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferHistory', @level2type = N'COLUMN', @level2name = N'OfferStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the financial evaluation result [Accepted or Rejected]', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferHistory', @level2type = N'COLUMN', @level2name = N'OfferAcceptanceStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Technical evaluation result [Accepted or Rejected]', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferHistory', @level2type = N'COLUMN', @level2name = N'OfferTechnicalEvaluationStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Rejection reason in awrding stage', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferHistory', @level2type = N'COLUMN', @level2name = N'RejectionReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The type of action on the offer', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'OfferHistory', @level2type = N'COLUMN', @level2name = N'ActionId';

