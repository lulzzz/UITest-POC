CREATE TABLE [LookUps].[ChangeRequestStatus] (
    [Id]     INT             NOT NULL,
    [NameEn] NVARCHAR (1024) NULL,
    [NameAr] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_ChangeRequestStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);

