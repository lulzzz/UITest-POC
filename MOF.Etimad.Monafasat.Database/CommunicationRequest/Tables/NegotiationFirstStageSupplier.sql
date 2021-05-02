CREATE TABLE [CommunicationRequest].[NegotiationFirstStageSupplier] (
    [CreatedAt]                   DATETIME2 (7)   NOT NULL,
    [CreatedBy]                   NVARCHAR (256)  NULL,
    [UpdatedAt]                   DATETIME2 (7)   NULL,
    [UpdatedBy]                   NVARCHAR (256)  NULL,
    [IsActive]                    BIT             NULL,
    [Id]                          INT             IDENTITY (1, 1) NOT NULL,
    [OfferId]                     INT             NOT NULL,
    [NegotiationId]               INT             NOT NULL,
    [SupplierCR]                  NVARCHAR (20)   NULL,
    [offerOriginalAmount]         DECIMAL (18, 2) NOT NULL,
    [NegotiationSupplierStatusId] INT             NULL,
    [PeriodStartDateTime]         DATETIME2 (7)   NULL,
    [IsReported]                  BIT             NOT NULL,
    CONSTRAINT [PK_NegotiationFirstStageSupplier] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NegotiationFirstStageSupplier_Negotiation_NegotiationId] FOREIGN KEY ([NegotiationId]) REFERENCES [CommunicationRequest].[Negotiation] ([NegotiationId]),
    CONSTRAINT [FK_NegotiationFirstStageSupplier_NegotiationSupplierStatus_NegotiationSupplierStatusId] FOREIGN KEY ([NegotiationSupplierStatusId]) REFERENCES [LookUps].[NegotiationSupplierStatus] ([NegotiationSupplierStatusId]),
    CONSTRAINT [FK_NegotiationFirstStageSupplier_Offer_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offer].[Offer] ([OfferId]),
    CONSTRAINT [FK_NegotiationFirstStageSupplier_Supplier_SupplierCR] FOREIGN KEY ([SupplierCR]) REFERENCES [IDM].[Supplier] ([SelectedCr])
);


GO
CREATE NONCLUSTERED INDEX [IX_NegotiationFirstStageSupplier_NegotiationId]
    ON [CommunicationRequest].[NegotiationFirstStageSupplier]([NegotiationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_NegotiationFirstStageSupplier_NegotiationSupplierStatusId]
    ON [CommunicationRequest].[NegotiationFirstStageSupplier]([NegotiationSupplierStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_NegotiationFirstStageSupplier_OfferId]
    ON [CommunicationRequest].[NegotiationFirstStageSupplier]([OfferId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_NegotiationFirstStageSupplier_SupplierCR]
    ON [CommunicationRequest].[NegotiationFirstStageSupplier]([SupplierCR] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Describe first Stage Negotiation Suppliers List', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'NegotiationFirstStageSupplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the Uniqee Identifier for  Negotiation First stage Suppliers List Table', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'NegotiationFirstStageSupplier', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define The related offer', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'NegotiationFirstStageSupplier', @level2type = N'COLUMN', @level2name = N'OfferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define The Negotiation Table ', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'NegotiationFirstStageSupplier', @level2type = N'COLUMN', @level2name = N'NegotiationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the supplier who will recieved the negotiation request  ', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'NegotiationFirstStageSupplier', @level2type = N'COLUMN', @level2name = N'SupplierCR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define The Amount of the offer before deduction', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'NegotiationFirstStageSupplier', @level2type = N'COLUMN', @level2name = N'offerOriginalAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The status of Request for the supplier is accepted from supplier or rejected or still not sent to the supplier', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'NegotiationFirstStageSupplier', @level2type = N'COLUMN', @level2name = N'NegotiationSupplierStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define The start date that the supplier recieved the request ', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'NegotiationFirstStageSupplier', @level2type = N'COLUMN', @level2name = N'PeriodStartDateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define if the supplier notified that he has Negotiation request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'NegotiationFirstStageSupplier', @level2type = N'COLUMN', @level2name = N'IsReported';

