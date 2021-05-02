CREATE TABLE [Offer].[TechnicianReportAttachments] (
    [CreatedAt]          DATETIME2 (7)   NOT NULL,
    [CreatedBy]          NVARCHAR (256)  NULL,
    [UpdatedAt]          DATETIME2 (7)   NULL,
    [UpdatedBy]          NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [AttachmentId]       INT             IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (200)  NOT NULL,
    [AttachmentTypeId]   INT             NOT NULL,
    [FileNetReferenceId] NVARCHAR (1024) NULL,
    [OfferId]            INT             NOT NULL,
    CONSTRAINT [PK_TechnicianReportAttachments] PRIMARY KEY CLUSTERED ([AttachmentId] ASC),
    CONSTRAINT [FK_TechnicianReportAttachments_AttachmentType_AttachmentTypeId] FOREIGN KEY ([AttachmentTypeId]) REFERENCES [LookUps].[AttachmentType] ([AttachmentTypeId]),
    CONSTRAINT [FK_TechnicianReportAttachments_Offer_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offer].[Offer] ([OfferId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TechnicianReportAttachments_AttachmentTypeId]
    ON [Offer].[TechnicianReportAttachments]([AttachmentTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TechnicianReportAttachments_OfferId]
    ON [Offer].[TechnicianReportAttachments]([OfferId] ASC);

