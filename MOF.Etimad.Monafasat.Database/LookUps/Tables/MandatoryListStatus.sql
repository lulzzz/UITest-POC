CREATE TABLE [LookUps].[MandatoryListStatus] (
    [StatusId] INT            NOT NULL,
    [NameAr]   NVARCHAR (100) NULL,
    [NameEn]   NVARCHAR (100) NULL,
    CONSTRAINT [PK_MandatoryListStatus] PRIMARY KEY CLUSTERED ([StatusId] ASC)
);

