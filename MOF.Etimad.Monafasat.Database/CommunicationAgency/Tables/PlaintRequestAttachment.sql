CREATE TABLE [CommunicationAgency].[PlaintRequestAttachment] (
    [CreatedAt]          DATETIME2 (7)   NOT NULL,
    [CreatedBy]          NVARCHAR (256)  NULL,
    [UpdatedAt]          DATETIME2 (7)   NULL,
    [UpdatedBy]          NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [AttachmentId]       INT             IDENTITY (1, 1) NOT NULL,
    [PlaintRequestId]    INT             NULL,
    [Name]               NVARCHAR (200)  NOT NULL,
    [AttachmentTypeId]   INT             NOT NULL,
    [FileNetReferenceId] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_PlaintRequestAttachment] PRIMARY KEY CLUSTERED ([AttachmentId] ASC),
    CONSTRAINT [FK_PlaintRequestAttachment_AttachmentType_AttachmentTypeId] FOREIGN KEY ([AttachmentTypeId]) REFERENCES [LookUps].[AttachmentType] ([AttachmentTypeId]),
    CONSTRAINT [FK_PlaintRequestAttachment_PlaintRequest_PlaintRequestId] FOREIGN KEY ([PlaintRequestId]) REFERENCES [CommunicationRequest].[PlaintRequest] ([PlainRequestId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PlaintRequestAttachment_AttachmentTypeId]
    ON [CommunicationAgency].[PlaintRequestAttachment]([AttachmentTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PlaintRequestAttachment_PlaintRequestId]
    ON [CommunicationAgency].[PlaintRequestAttachment]([PlaintRequestId] ASC);

