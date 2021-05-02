CREATE TABLE [AnnouncementTemplate].[AnnouncementSuppliersTemplateAttachment] (
    [AnnouncementSuppliersTemplateAttachmentId] INT             IDENTITY (1, 1) NOT NULL,
    [CreatedAt]                                 DATETIME2 (7)   NOT NULL,
    [CreatedBy]                                 NVARCHAR (256)  NULL,
    [UpdatedAt]                                 DATETIME2 (7)   NULL,
    [UpdatedBy]                                 NVARCHAR (256)  NULL,
    [IsActive]                                  BIT             NULL,
    [Name]                                      NVARCHAR (200)  NOT NULL,
    [AttachmentTypeId]                          INT             NOT NULL,
    [FileNetReferenceId]                        NVARCHAR (1024) NULL,
    [AnnouncementId]                            INT             NOT NULL,
    [ChangeStatusId]                            INT             NULL,
    [ReviewStatusId]                            INT             NULL,
    [RejectionReason]                           NVARCHAR (1024) NULL,
    CONSTRAINT [PK_AnnouncementSuppliersTemplateAttachment] PRIMARY KEY CLUSTERED ([AnnouncementSuppliersTemplateAttachmentId] ASC),
    CONSTRAINT [FK_AnnouncementSuppliersTemplateAttachment_AnnouncementSupplierTemplate_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [AnnouncementTemplate].[AnnouncementSupplierTemplate] ([AnnouncementId]),
    CONSTRAINT [FK_AnnouncementSuppliersTemplateAttachment_AttachmentType_AttachmentTypeId] FOREIGN KEY ([AttachmentTypeId]) REFERENCES [LookUps].[AttachmentType] ([AttachmentTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementSuppliersTemplateAttachment_AnnouncementId]
    ON [AnnouncementTemplate].[AnnouncementSuppliersTemplateAttachment]([AnnouncementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementSuppliersTemplateAttachment_AttachmentTypeId]
    ON [AnnouncementTemplate].[AnnouncementSuppliersTemplateAttachment]([AttachmentTypeId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of announcement supplier template attachment', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSuppliersTemplateAttachment', @level2type = N'COLUMN', @level2name = N'AnnouncementSuppliersTemplateAttachmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define created date of attachment for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSuppliersTemplateAttachment', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who cretead attachment for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSuppliersTemplateAttachment', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define updated date of attachment for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSuppliersTemplateAttachment', @level2type = N'COLUMN', @level2name = N'UpdatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who updated attachment for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSuppliersTemplateAttachment', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define attachment is active or not', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSuppliersTemplateAttachment', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define name of announcement supplier template attachment', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSuppliersTemplateAttachment', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define type of  attachment', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSuppliersTemplateAttachment', @level2type = N'COLUMN', @level2name = N'AttachmentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define file net id', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSuppliersTemplateAttachment', @level2type = N'COLUMN', @level2name = N'FileNetReferenceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define forigne key of announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSuppliersTemplateAttachment', @level2type = N'COLUMN', @level2name = N'AnnouncementId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define change status id of attachment for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSuppliersTemplateAttachment', @level2type = N'COLUMN', @level2name = N'ChangeStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define review status id of attachment for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSuppliersTemplateAttachment', @level2type = N'COLUMN', @level2name = N'ReviewStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define rejection reason of attachment for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementSuppliersTemplateAttachment', @level2type = N'COLUMN', @level2name = N'RejectionReason';

