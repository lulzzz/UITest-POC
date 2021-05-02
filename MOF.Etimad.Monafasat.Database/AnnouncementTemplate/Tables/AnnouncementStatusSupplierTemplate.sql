CREATE TABLE [AnnouncementTemplate].[AnnouncementStatusSupplierTemplate] (
    [Id]   INT             NOT NULL,
    [Name] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_AnnouncementStatusSupplierTemplate] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of announcement status supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementStatusSupplierTemplate', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define name of status supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementStatusSupplierTemplate', @level2type = N'COLUMN', @level2name = N'Name';

