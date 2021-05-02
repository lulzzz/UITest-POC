CREATE TABLE [Announcement].[AnnouncementStatus] (
    [Id]   INT             IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_AnnouncementStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of announcement status', @level0type = N'SCHEMA', @level0name = N'Announcement', @level1type = N'TABLE', @level1name = N'AnnouncementStatus', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define name of anouncement status', @level0type = N'SCHEMA', @level0name = N'Announcement', @level1type = N'TABLE', @level1name = N'AnnouncementStatus', @level2type = N'COLUMN', @level2name = N'Name';

