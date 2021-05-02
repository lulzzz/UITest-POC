CREATE TABLE [Tender].[AttachmentChanges] (
    [CreatedAt]             DATETIME2 (7)   NOT NULL,
    [CreatedBy]             NVARCHAR (256)  NULL,
    [UpdatedAt]             DATETIME2 (7)   NULL,
    [UpdatedBy]             NVARCHAR (256)  NULL,
    [IsActive]              BIT             NULL,
    [TenderAttachmentId]    INT             IDENTITY (1, 1) NOT NULL,
    [Name]                  NVARCHAR (200)  NOT NULL,
    [AttachmentTypeId]      INT             NOT NULL,
    [FileNetReferenceId]    NVARCHAR (1024) NULL,
    [RejectionReason]       NVARCHAR (1024) NULL,
    [TenderChangeRequestId] INT             NOT NULL,
    [DeletedAttachmentId]   INT             NULL,
    CONSTRAINT [PK_AttachmentChanges] PRIMARY KEY CLUSTERED ([TenderAttachmentId] ASC),
    CONSTRAINT [FK_AttachmentChanges_Attachment_DeletedAttachmentId] FOREIGN KEY ([DeletedAttachmentId]) REFERENCES [Tender].[Attachment] ([TenderAttachmentId]),
    CONSTRAINT [FK_AttachmentChanges_AttachmentType_AttachmentTypeId] FOREIGN KEY ([AttachmentTypeId]) REFERENCES [LookUps].[AttachmentType] ([AttachmentTypeId]),
    CONSTRAINT [FK_AttachmentChanges_TenderChangeRequest_TenderChangeRequestId] FOREIGN KEY ([TenderChangeRequestId]) REFERENCES [Tender].[TenderChangeRequest] ([TenderChangeRequestId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AttachmentChanges_AttachmentTypeId]
    ON [Tender].[AttachmentChanges]([AttachmentTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AttachmentChanges_DeletedAttachmentId]
    ON [Tender].[AttachmentChanges]([DeletedAttachmentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AttachmentChanges_TenderChangeRequestId]
    ON [Tender].[AttachmentChanges]([TenderChangeRequestId] ASC);

