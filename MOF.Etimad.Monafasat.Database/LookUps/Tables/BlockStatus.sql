CREATE TABLE [LookUps].[BlockStatus] (
    [BlockStatusId]     INT            NOT NULL,
    [BlockStatusName]   NVARCHAR (200) NULL,
    [BlockStatusNameAr] NVARCHAR (200) NULL,
    CONSTRAINT [PK_BlockStatus] PRIMARY KEY CLUSTERED ([BlockStatusId] ASC)
);

