CREATE TABLE [CommunicationAgency].[NegotiationAttachment] (
    [CreatedAt]          DATETIME2 (7)   NOT NULL,
    [CreatedBy]          NVARCHAR (256)  NULL,
    [UpdatedAt]          DATETIME2 (7)   NULL,
    [UpdatedBy]          NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [AttachmentId]       INT             IDENTITY (1, 1) NOT NULL,
    [NegotiationId]      INT             NOT NULL,
    [Name]               NVARCHAR (200)  NOT NULL,
    [AttachmentTypeId]   INT             NOT NULL,
    [FileNetReferenceId] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_NegotiationAttachment] PRIMARY KEY CLUSTERED ([AttachmentId] ASC),
    CONSTRAINT [FK_NegotiationAttachment_AttachmentType_AttachmentTypeId] FOREIGN KEY ([AttachmentTypeId]) REFERENCES [LookUps].[AttachmentType] ([AttachmentTypeId]),
    CONSTRAINT [FK_NegotiationAttachment_Negotiation_NegotiationId] FOREIGN KEY ([NegotiationId]) REFERENCES [CommunicationRequest].[Negotiation] ([NegotiationId])
);


GO
CREATE NONCLUSTERED INDEX [IX_NegotiationAttachment_AttachmentTypeId]
    ON [CommunicationAgency].[NegotiationAttachment]([AttachmentTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_NegotiationAttachment_NegotiationId]
    ON [CommunicationAgency].[NegotiationAttachment]([NegotiationId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Describe the Negotiation Attachment', @level0type = N'SCHEMA', @level0name = N'CommunicationAgency', @level1type = N'TABLE', @level1name = N'NegotiationAttachment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define the unique  identifier for negotiation Attachment', @level0type = N'SCHEMA', @level0name = N'CommunicationAgency', @level1type = N'TABLE', @level1name = N'NegotiationAttachment', @level2type = N'COLUMN', @level2name = N'AttachmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define The related of Negotiation request', @level0type = N'SCHEMA', @level0name = N'CommunicationAgency', @level1type = N'TABLE', @level1name = N'NegotiationAttachment', @level2type = N'COLUMN', @level2name = N'NegotiationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define The name of the file', @level0type = N'SCHEMA', @level0name = N'CommunicationAgency', @level1type = N'TABLE', @level1name = N'NegotiationAttachment', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'the type of attachment file like [supplier attachment or agency attachment]', @level0type = N'SCHEMA', @level0name = N'CommunicationAgency', @level1type = N'TABLE', @level1name = N'NegotiationAttachment', @level2type = N'COLUMN', @level2name = N'AttachmentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define The reference number from file Net', @level0type = N'SCHEMA', @level0name = N'CommunicationAgency', @level1type = N'TABLE', @level1name = N'NegotiationAttachment', @level2type = N'COLUMN', @level2name = N'FileNetReferenceId';

