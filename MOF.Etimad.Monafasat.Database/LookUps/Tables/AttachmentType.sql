CREATE TABLE [LookUps].[AttachmentType] (
    [AttachmentTypeId] INT             NOT NULL,
    [NameEn]           NVARCHAR (1024) NULL,
    [NameAr]           NVARCHAR (1024) NULL,
    CONSTRAINT [PK_AttachmentType] PRIMARY KEY CLUSTERED ([AttachmentTypeId] ASC)
);

