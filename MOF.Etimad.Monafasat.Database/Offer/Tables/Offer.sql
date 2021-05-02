CREATE TABLE [Offer].[Offer] (
    [CreatedAt]                          DATETIME2 (7)   NOT NULL,
    [CreatedBy]                          NVARCHAR (256)  NULL,
    [UpdatedAt]                          DATETIME2 (7)   NULL,
    [UpdatedBy]                          NVARCHAR (256)  NULL,
    [IsActive]                           BIT             NULL,
    [OfferId]                            INT             IDENTITY (1, 1) NOT NULL,
    [SuplierId]                          INT             NOT NULL,
    [TenderId]                           INT             NOT NULL,
    [OfferStatusId]                      INT             NOT NULL,
    [OfferPresentationWayId]             INT             NULL,
    [OfferAcceptanceStatusId]            INT             NULL,
    [OfferTechnicalEvaluationStatusId]   INT             NULL,
    [IsManuallyApplied]                  BIT             NOT NULL,
    [CommericalRegisterNo]               NVARCHAR (20)   NULL,
    [WithrdrawlDate]                     DATETIME2 (7)   NULL,
    [Notes]                              NVARCHAR (MAX)  NULL,
    [FinantialNotes]                     NVARCHAR (MAX)  NULL,
    [DiscountNotes]                      NVARCHAR (MAX)  NULL,
    [Discount]                           NVARCHAR (MAX)  NULL,
    [IsOfferCopyAttached]                BIT             NULL,
    [IsSolidarity]                       BIT             NOT NULL,
    [IsOfferLetterAttached]              BIT             NULL,
    [OfferLetterNumber]                  NVARCHAR (500)  NULL,
    [OfferLetterDate]                    DATETIME2 (7)   NULL,
    [IsPurchaseBillAttached]             BIT             NULL,
    [IsBankGuaranteeAttached]            BIT             NULL,
    [IsVisitationAttached]               BIT             NULL,
    [JustificationOfRecommendation]      NVARCHAR (1024) NULL,
    [IsOpened]                           BIT             NULL,
    [TotalOfferAwardingValue]            DECIMAL (18, 2) NULL,
    [PartialOfferAwardingValue]          DECIMAL (18, 2) NULL,
    [FinalPriceAfterDiscount]            DECIMAL (18, 2) NULL,
    [FinalPriceBeforeDiscount]           DECIMAL (18, 2) NULL,
    [RejectionReason]                    NVARCHAR (MAX)  NULL,
    [FinantialRejectionReason]           NVARCHAR (MAX)  NULL,
    [IsFinaintialOfferLetterAttached]    BIT             NULL,
    [FinantialOfferLetterNumber]         NVARCHAR (MAX)  NULL,
    [FinantialOfferLetterDate]           DATETIME2 (7)   NULL,
    [IsFinantialOfferLetterCopyAttached] BIT             NULL,
    [IsOfferFinancialDetailsEntered]     BIT             NOT NULL,
    [TechnicalEvaluationLevel]           INT             NOT NULL,
    [FinancialEvaluationLevel]           INT             NOT NULL,
    [OfferWeightAfterCalcNPA]            DECIMAL (18, 2) NULL,
    [ExclusionReason]                    NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_Offer] PRIMARY KEY CLUSTERED ([OfferId] ASC),
    CONSTRAINT [FK_Offer_OfferPresentationWay_OfferPresentationWayId] FOREIGN KEY ([OfferPresentationWayId]) REFERENCES [LookUps].[OfferPresentationWay] ([Id]),
    CONSTRAINT [FK_Offer_OfferStatus_OfferStatusId] FOREIGN KEY ([OfferStatusId]) REFERENCES [LookUps].[OfferStatus] ([OfferStatusId]),
    CONSTRAINT [FK_Offer_Supplier_CommericalRegisterNo] FOREIGN KEY ([CommericalRegisterNo]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_Offer_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Offer_CommericalRegisterNo]
    ON [Offer].[Offer]([CommericalRegisterNo] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Offer_OfferPresentationWayId]
    ON [Offer].[Offer]([OfferPresentationWayId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Offer_OfferStatusId]
    ON [Offer].[Offer]([OfferStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Offer_TenderId]
    ON [Offer].[Offer]([TenderId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Represent  Offer Data ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Unique identifer Of Offer', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'OfferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Not Used', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'SuplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Id of related Tender', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'TenderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the status of offer like (under establishment,Sent,cancelled ...etc )', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'OfferStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define if the offer is presented in one file or two files ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'OfferPresentationWayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the financial evaluation result [Accepted or Rejected]', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'OfferAcceptanceStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Technical evaluation result [Accepted or Rejected]', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'OfferTechnicalEvaluationStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define if the offer was applied out of the system or by the system', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'IsManuallyApplied';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Commerical Register Number for the owner supplier for the offer ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'CommericalRegisterNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Represent the notes entered on Checkng Stage', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'Notes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Represent the notes entered on Checkng Stage', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'FinantialNotes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Represent notes entered while adding  discount , used in awarding stage', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'DiscountNotes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Represent the Discount , used in awarding stage', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'Discount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define if hard copy of offer Applied', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'IsOfferCopyAttached';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define if More than one supplier in one offer ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'IsSolidarity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define if Offer Letter Attached ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'IsOfferLetterAttached';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define  the Offer Letter Number', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'OfferLetterNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Offer Letter Expiry Date', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'OfferLetterDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define if Purchase Bill hard Copy applied to the agency ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'IsPurchaseBillAttached';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define if Bank Guarantee hard Copy appied to the agency ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'IsBankGuaranteeAttached';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define if Visitation hard Copy appied to the agency ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'IsVisitationAttached';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Justification Of Recommendation', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'JustificationOfRecommendation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Is the offer opended and all needed Data filled in oppenning stage', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'IsOpened';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the total value of awarding ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'TotalOfferAwardingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the partial value of awarding if the tender partialy awarded', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'PartialOfferAwardingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The final financial value of the offer after appling VAT and discount', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'FinalPriceAfterDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The final financial value of the offer before appling VAT and discount', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'FinalPriceBeforeDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The Rejection reason in awrding stage', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'RejectionReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The reason for financial rejection in checking stage', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'FinantialRejectionReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Represent if the  Finaintial Offer Letter Attached', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'IsFinaintialOfferLetterAttached';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The number of Finantial Offer Letter', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'FinantialOfferLetterNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Represent if acopy of Finaintial Offer Letter manually Applied', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'IsFinantialOfferLetterCopyAttached';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define if all financial Details entired or Not', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'IsOfferFinancialDetailsEntered';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Technical Evaluation Level of the offer ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'TechnicalEvaluationLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Financial Evaluation Level of the offer ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'FinancialEvaluationLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The final financial value of the offer after appling VAT and discount and Calculation of National Product Equation', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'OfferWeightAfterCalcNPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Represent the Reason of excluding the offer ', @level0type = N'SCHEMA', @level0name = N'Offer', @level1type = N'TABLE', @level1name = N'Offer', @level2type = N'COLUMN', @level2name = N'ExclusionReason';

