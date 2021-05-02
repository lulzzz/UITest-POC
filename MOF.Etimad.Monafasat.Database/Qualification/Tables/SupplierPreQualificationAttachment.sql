CREATE TABLE [Qualification].[SupplierPreQualificationAttachment] (
    [CreatedAt]           DATETIME2 (7)   NOT NULL,
    [CreatedBy]           NVARCHAR (256)  NULL,
    [UpdatedAt]           DATETIME2 (7)   NULL,
    [UpdatedBy]           NVARCHAR (256)  NULL,
    [IsActive]            BIT             NULL,
    [AttachmentId]        INT             IDENTITY (1, 1) NOT NULL,
    [FileName]            NVARCHAR (1024) NULL,
    [FileNetReferenceId]  NVARCHAR (1024) NULL,
    [SupPreQAttachmentId] INT             NOT NULL,
    CONSTRAINT [PK_SupplierPreQualificationAttachment] PRIMARY KEY CLUSTERED ([AttachmentId] ASC),
    CONSTRAINT [FK_SupplierPreQualificationAttachment_SupplierPreQualificationDocument_SupPreQAttachmentId] FOREIGN KEY ([SupPreQAttachmentId]) REFERENCES [Qualification].[SupplierPreQualificationDocument] ([SupplierPreQualificationDocumentId])
);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierPreQualificationAttachment_SupPreQAttachmentId]
    ON [Qualification].[SupplierPreQualificationAttachment]([SupPreQAttachmentId] ASC);

