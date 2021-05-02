CREATE TABLE [LookUps].[BlockType] (
    [BlockTypeId] INT            NOT NULL,
    [NameEn]      NVARCHAR (200) NULL,
    [NameAr]      NVARCHAR (200) NULL,
    CONSTRAINT [PK_BlockType] PRIMARY KEY CLUSTERED ([BlockTypeId] ASC)
);

