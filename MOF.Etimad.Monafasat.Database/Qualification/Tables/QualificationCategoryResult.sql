CREATE TABLE [Qualification].[QualificationCategoryResult] (
    [CreatedAt]                   DATETIME2 (7)   NOT NULL,
    [CreatedBy]                   NVARCHAR (256)  NULL,
    [UpdatedAt]                   DATETIME2 (7)   NULL,
    [UpdatedBy]                   NVARCHAR (256)  NULL,
    [IsActive]                    BIT             NULL,
    [ID]                          INT             IDENTITY (1, 1) NOT NULL,
    [QualificationItemCategoryId] INT             NOT NULL,
    [TenderId]                    INT             NOT NULL,
    [SupplierSelectedCr]          NVARCHAR (20)   NULL,
    [ResultValue]                 DECIMAL (18, 2) NOT NULL,
    [Percentage]                  DECIMAL (18, 2) NOT NULL,
    [Weight]                      DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_QualificationCategoryResult] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_QualificationCategoryResult_QualificationItemCategory_QualificationItemCategoryId] FOREIGN KEY ([QualificationItemCategoryId]) REFERENCES [Qualification].[QualificationItemCategory] ([ID]),
    CONSTRAINT [FK_QualificationCategoryResult_Supplier_SupplierSelectedCr] FOREIGN KEY ([SupplierSelectedCr]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_QualificationCategoryResult_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationCategoryResult_QualificationItemCategoryId]
    ON [Qualification].[QualificationCategoryResult]([QualificationItemCategoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationCategoryResult_SupplierSelectedCr]
    ON [Qualification].[QualificationCategoryResult]([SupplierSelectedCr] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationCategoryResult_TenderId]
    ON [Qualification].[QualificationCategoryResult]([TenderId] ASC);

