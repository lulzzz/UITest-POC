CREATE TABLE [LookUps].[SpendingCategory] (
    [SpendingCategoryId] INT            NOT NULL,
    [NameAr]             NVARCHAR (100) NULL,
    [NameEn]             NVARCHAR (100) NULL,
    CONSTRAINT [PK_SpendingCategory] PRIMARY KEY CLUSTERED ([SpendingCategoryId] ASC)
);

