CREATE TABLE [CommunicationRequest].[PlaintRequest] (
    [CreatedAt]                    DATETIME2 (7)   NOT NULL,
    [CreatedBy]                    NVARCHAR (256)  NULL,
    [UpdatedAt]                    DATETIME2 (7)   NULL,
    [UpdatedBy]                    NVARCHAR (256)  NULL,
    [IsActive]                     BIT             NULL,
    [PlainRequestId]               INT             IDENTITY (1, 1) NOT NULL,
    [PlaintReason]                 NVARCHAR (1000) NULL,
    [Notes]                        NVARCHAR (1000) NULL,
    [AgencyCommunicationRequestId] INT             NOT NULL,
    [IsEscalation]                 BIT             NOT NULL,
    [OfferId]                      INT             NOT NULL,
    CONSTRAINT [PK_PlaintRequest] PRIMARY KEY CLUSTERED ([PlainRequestId] ASC),
    CONSTRAINT [FK_PlaintRequest_AgencyCommunicationRequest_AgencyCommunicationRequestId] FOREIGN KEY ([AgencyCommunicationRequestId]) REFERENCES [CommunicationRequest].[AgencyCommunicationRequest] ([AgencyRequestId]),
    CONSTRAINT [FK_PlaintRequest_Offer_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offer].[Offer] ([OfferId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PlaintRequest_AgencyCommunicationRequestId]
    ON [CommunicationRequest].[PlaintRequest]([AgencyCommunicationRequestId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PlaintRequest_OfferId]
    ON [CommunicationRequest].[PlaintRequest]([OfferId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define a unique identifer of plaint request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'PlaintRequest', @level2type = N'COLUMN', @level2name = N'PlainRequestId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the reason of plaint request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'PlaintRequest', @level2type = N'COLUMN', @level2name = N'PlaintReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define notes for more details about the plaint request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'PlaintRequest', @level2type = N'COLUMN', @level2name = N'Notes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the related agency communication request for plaint request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'PlaintRequest', @level2type = N'COLUMN', @level2name = N'AgencyCommunicationRequestId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag determine if the plaint request has an escalation request or not', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'PlaintRequest', @level2type = N'COLUMN', @level2name = N'IsEscalation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the related Offer for plaint request', @level0type = N'SCHEMA', @level0name = N'CommunicationRequest', @level1type = N'TABLE', @level1name = N'PlaintRequest', @level2type = N'COLUMN', @level2name = N'OfferId';

