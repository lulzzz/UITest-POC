CREATE TABLE [Tender].[Attachment] (
    [CreatedAt]          DATETIME2 (7)   NOT NULL,
    [CreatedBy]          NVARCHAR (256)  NULL,
    [UpdatedAt]          DATETIME2 (7)   NULL,
    [UpdatedBy]          NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [TenderAttachmentId] INT             IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (200)  NOT NULL,
    [AttachmentTypeId]   INT             NOT NULL,
    [FileNetReferenceId] NVARCHAR (1024) NULL,
    [TenderId]           INT             NOT NULL,
    [ChangeStatusId]     INT             NULL,
    [ReviewStatusId]     INT             NULL,
    [RejectionReason]    NVARCHAR (1024) NULL,
    CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED ([TenderAttachmentId] ASC),
    CONSTRAINT [FK_Attachment_AttachmentType_AttachmentTypeId] FOREIGN KEY ([AttachmentTypeId]) REFERENCES [LookUps].[AttachmentType] ([AttachmentTypeId]),
    CONSTRAINT [FK_Attachment_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Attachment_AttachmentTypeId]
    ON [Tender].[Attachment]([AttachmentTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Attachment_TenderId]
    ON [Tender].[Attachment]([TenderId] ASC);

