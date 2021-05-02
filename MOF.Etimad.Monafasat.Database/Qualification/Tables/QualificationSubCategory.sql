CREATE TABLE [Qualification].[QualificationSubCategory] (
    [ID]                      INT            IDENTITY (1, 1) NOT NULL,
    [QualificationCategoryId] INT            NOT NULL,
    [Name]                    NVARCHAR (MAX) NULL,
    [NameEn]                  NVARCHAR (MAX) NULL,
    [IsConfigure]             BIT            NOT NULL,
    CONSTRAINT [PK_QualificationSubCategory] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_QualificationSubCategory_QualificationItemCategory_QualificationCategoryId] FOREIGN KEY ([QualificationCategoryId]) REFERENCES [Qualification].[QualificationItemCategory] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSubCategory_QualificationCategoryId]
    ON [Qualification].[QualificationSubCategory]([QualificationCategoryId] ASC);

