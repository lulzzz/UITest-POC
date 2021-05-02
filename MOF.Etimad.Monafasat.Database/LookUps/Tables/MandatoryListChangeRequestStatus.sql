CREATE TABLE [LookUps].[MandatoryListChangeRequestStatus] (
    [StatusId] INT            NOT NULL,
    [NameAr]   NVARCHAR (100) NULL,
    [NameEn]   NVARCHAR (100) NULL,
    CONSTRAINT [PK_MandatoryListChangeRequestStatus] PRIMARY KEY CLUSTERED ([StatusId] ASC)
);

