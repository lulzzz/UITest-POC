CREATE TABLE [LookUps].[AnnouncementTemplateListType] (
    [AnnouncementTemplateSuppliersListTypeId] INT            NOT NULL,
    [NameAr]                                  NVARCHAR (100) NULL,
    [NameEn]                                  NVARCHAR (100) NULL,
    CONSTRAINT [PK_AnnouncementTemplateListType] PRIMARY KEY CLUSTERED ([AnnouncementTemplateSuppliersListTypeId] ASC)
);

