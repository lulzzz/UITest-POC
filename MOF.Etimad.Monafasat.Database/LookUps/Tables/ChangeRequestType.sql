CREATE TABLE [LookUps].[ChangeRequestType] (
    [Id]     INT             NOT NULL,
    [NameEn] NVARCHAR (1024) NULL,
    [NameAr] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_ChangeRequestType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

