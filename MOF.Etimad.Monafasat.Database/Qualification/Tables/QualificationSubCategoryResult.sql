CREATE TABLE [Qualification].[QualificationSubCategoryResult] (
    [CreatedAt]                  DATETIME2 (7)   NOT NULL,
    [CreatedBy]                  NVARCHAR (256)  NULL,
    [UpdatedAt]                  DATETIME2 (7)   NULL,
    [UpdatedBy]                  NVARCHAR (256)  NULL,
    [IsActive]                   BIT             NULL,
    [ID]                         INT             IDENTITY (1, 1) NOT NULL,
    [QualificationSubCategoryId] INT             NOT NULL,
    [TenderId]                   INT             NOT NULL,
    [SupplierSelectedCr]         NVARCHAR (20)   NULL,
    [ResultValue]                DECIMAL (18, 2) NOT NULL,
    [Percentage]                 DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_QualificationSubCategoryResult] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_QualificationSubCategoryResult_QualificationSubCategory_QualificationSubCategoryId] FOREIGN KEY ([QualificationSubCategoryId]) REFERENCES [Qualification].[QualificationSubCategory] ([ID]),
    CONSTRAINT [FK_QualificationSubCategoryResult_Supplier_SupplierSelectedCr] FOREIGN KEY ([SupplierSelectedCr]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_QualificationSubCategoryResult_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSubCategoryResult_QualificationSubCategoryId]
    ON [Qualification].[QualificationSubCategoryResult]([QualificationSubCategoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSubCategoryResult_SupplierSelectedCr]
    ON [Qualification].[QualificationSubCategoryResult]([SupplierSelectedCr] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSubCategoryResult_TenderId]
    ON [Qualification].[QualificationSubCategoryResult]([TenderId] ASC);

