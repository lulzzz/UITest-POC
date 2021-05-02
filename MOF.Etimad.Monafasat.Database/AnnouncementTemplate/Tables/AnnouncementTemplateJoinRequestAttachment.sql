CREATE TABLE [AnnouncementTemplate].[AnnouncementTemplateJoinRequestAttachment] (
    [Id]                            INT             IDENTITY (1, 1) NOT NULL,
    [CreatedAt]                     DATETIME2 (7)   NOT NULL,
    [CreatedBy]                     NVARCHAR (256)  NULL,
    [UpdatedAt]                     DATETIME2 (7)   NULL,
    [UpdatedBy]                     NVARCHAR (256)  NULL,
    [IsActive]                      BIT             NULL,
    [Name]                          NVARCHAR (200)  NOT NULL,
    [FileNetReferenceId]            NVARCHAR (1024) NULL,
    [JoinRequestSupplierTemplateId] INT             NOT NULL,
    CONSTRAINT [PK_AnnouncementTemplateJoinRequestAttachment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnouncementTemplateJoinRequestAttachment_AnnouncementRequestSupplierTemplate_JoinRequestSupplierTemplateId] FOREIGN KEY ([JoinRequestSupplierTemplateId]) REFERENCES [AnnouncementTemplate].[AnnouncementRequestSupplierTemplate] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of announcement supplier template attachment', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementTemplateJoinRequestAttachment', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define created date of attachment for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementTemplateJoinRequestAttachment', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who cretead attachment for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementTemplateJoinRequestAttachment', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define updated date of attachment for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementTemplateJoinRequestAttachment', @level2type = N'COLUMN', @level2name = N'UpdatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who updated attachment for announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementTemplateJoinRequestAttachment', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define attachment is active or not', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementTemplateJoinRequestAttachment', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define name of announcement supplier template attachment', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementTemplateJoinRequestAttachment', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define file net id', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementTemplateJoinRequestAttachment', @level2type = N'COLUMN', @level2name = N'FileNetReferenceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define forigne key of join request announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementTemplateJoinRequestAttachment', @level2type = N'COLUMN', @level2name = N'JoinRequestSupplierTemplateId';

