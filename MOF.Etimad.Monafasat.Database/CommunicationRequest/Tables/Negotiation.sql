CREATE TABLE [CommunicationRequest].[Negotiation] (
    [CreatedAt]                DATETIME2 (7)   NOT NULL,
    [CreatedBy]                NVARCHAR (256)  NULL,
    [UpdatedAt]                DATETIME2 (7)   NULL,
    [UpdatedBy]                NVARCHAR (256)  NULL,
    [IsActive]                 BIT             NULL,
    [NegotiationId]            INT             IDENTITY (1, 1) NOT NULL,
    [SupplierReplyPeriodHours] INT             NOT NULL,
    [AgencyRequestId]          INT             NOT NULL,
    [NegotiationReasonId]      INT             NULL,
    [NegotiationTypeId]        INT             NOT NULL,
    [RejectionReason]          NVARCHAR (MAX)  NULL,
    [StatusId]                 INT             NOT NULL,
    [DiscountLetterRefID]      NVARCHAR (MAX)  NULL,
    [ProjectNumber]            NVARCHAR (MAX)  NULL,
    [DiscountAmount]           DECIMAL (18, 2) NULL,
    [NegotiationFirstStageId]  INT             NULL,
    [OfferId]                  INT             NULL,
    [IsReported]               BIT             NULL,
    [PeriodStartDate]          DATETIME2 (7)   NULL,
    [IsSupplierAgree]          BIT             NULL,
    [IsNewNegotiation]         BIT             NULL,
    [ExtraDiscountValue]       DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_Negotiation] PRIMARY KEY CLUSTERED ([NegotiationId] ASC),
    CONSTRAINT [FK_Negotiation_AgencyCommunicationRequest_AgencyRequestId] FOREIGN KEY ([AgencyRequestId]) REFERENCES [CommunicationRequest].[AgencyCommunicationRequest] ([AgencyRequestId]),
    CONSTRAINT [FK_Negotiation_Negotiation_NegotiationFirstStageId] FOREIGN KEY ([NegotiationFirstStageId]) REFERENCES [CommunicationRequest].[Negotiation] ([NegotiationId]),
    CONSTRAINT [FK_Negotiation_NegotiationReason_NegotiationReasonId] FOREIGN KEY ([NegotiationReasonId]) REFERENCES [LookUps].[NegotiationReason] ([NegotiationReasonId]),
    CONSTRAINT [FK_Negotiation_NegotiationStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[NegotiationStatus] ([NegotiationStatusId]),
    CONSTRAINT [FK_Negotiation_NegotiationType_NegotiationTypeId] FOREIGN KEY ([NegotiationTypeId]) REFERENCES [LookUps].[NegotiationType] ([NegotiationTypeId]),
    CONSTRAINT [FK_Negotiation_Offer_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offer].[Offer] ([OfferId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Negotiation_AgencyRequestId]
    ON [CommunicationRequest].[Negotiation]([AgencyRequestId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Negotiation_NegotiationReasonId]
    ON [CommunicationRequest].[Negotiation]([NegotiationReasonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Negotiation_NegotiationTypeId]
    ON [CommunicationRequest].[Negotiation]([NegotiationTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Negotiation_StatusId]
    ON [CommunicationRequest].[Negotiation]([StatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Negotiation_NegotiationFirstStageId]
    ON [CommunicationRequest].[Negotiation]([NegotiationFirstStageId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Negotiation_OfferId]
    ON [CommunicationRequest].[Negotiation]([OfferId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Describe first Stage Negotiation', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Identity Of Negotiation Request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'NegotiationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'the period that the defined for supplier to reply on the negotiation Request ', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'SupplierReplyPeriodHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Communication Request that the negotiation is related To', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'AgencyRequestId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'the reason for negotiation Note: this column refer to Negotiations Reasons Table ', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'NegotiationReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'the type of negotiation [first stage or second stage] Note: this column refer to Negotiations types Table ', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'NegotiationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Note written by the high level employee if he rejected the request ', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'RejectionReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the status of Negotiation ', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'StatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Referance for the Letter File of Negotiation', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'DiscountLetterRefID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define The project number from Etimad Budget', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'ProjectNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define The amount which the agency needs to deduct from supplier offer', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'DiscountAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Related First Stage Negotiation Request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'NegotiationFirstStageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define The project number from Etimad Budget', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'OfferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define The start date that the supplier recieved the request ', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'PeriodStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'define if the negotiation request should take the new flow or old flow', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'IsNewNegotiation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define The Amount of the offer after supplier deducted axtra amount from his offer ', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'Negotiation', @level2type = N'COLUMN', @level2name = N'ExtraDiscountValue';

