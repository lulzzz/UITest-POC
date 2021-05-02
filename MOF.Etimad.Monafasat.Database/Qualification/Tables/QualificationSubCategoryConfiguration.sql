CREATE TABLE [Qualification].[QualificationSubCategoryConfiguration] (
    [CreatedAt]                  DATETIME2 (7)   NOT NULL,
    [CreatedBy]                  NVARCHAR (256)  NULL,
    [UpdatedAt]                  DATETIME2 (7)   NULL,
    [UpdatedBy]                  NVARCHAR (256)  NULL,
    [IsActive]                   BIT             NULL,
    [ID]                         INT             IDENTITY (1, 1) NOT NULL,
    [TenderId]                   INT             NOT NULL,
    [QualificationSubCategoryId] INT             NOT NULL,
    [Weight]                     DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_QualificationSubCategoryConfiguration] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_QualificationSubCategoryConfiguration_QualificationSubCategory_QualificationSubCategoryId] FOREIGN KEY ([QualificationSubCategoryId]) REFERENCES [Qualification].[QualificationSubCategory] ([ID]),
    CONSTRAINT [FK_QualificationSubCategoryConfiguration_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSubCategoryConfiguration_QualificationSubCategoryId]
    ON [Qualification].[QualificationSubCategoryConfiguration]([QualificationSubCategoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSubCategoryConfiguration_TenderId]
    ON [Qualification].[QualificationSubCategoryConfiguration]([TenderId] ASC);

