CREATE TABLE [Qualification].[QualificationTypeCategory] (
    [ID]                         INT IDENTITY (1, 1) NOT NULL,
    [QualificationTypeId]        INT NOT NULL,
    [QualificationSubCategoryId] INT NOT NULL,
    CONSTRAINT [PK_QualificationTypeCategory] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_QualificationTypeCategory_QualificationSubCategory_QualificationSubCategoryId] FOREIGN KEY ([QualificationSubCategoryId]) REFERENCES [Qualification].[QualificationSubCategory] ([ID]),
    CONSTRAINT [FK_QualificationTypeCategory_QualificationType_QualificationTypeId] FOREIGN KEY ([QualificationTypeId]) REFERENCES [Qualification].[QualificationType] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationTypeCategory_QualificationSubCategoryId]
    ON [Qualification].[QualificationTypeCategory]([QualificationSubCategoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationTypeCategory_QualificationTypeId]
    ON [Qualification].[QualificationTypeCategory]([QualificationTypeId] ASC);

