CREATE TABLE [Qualification].[QualificationSupplierDataYearly] (
    [CreatedAt]           DATETIME2 (7)   NOT NULL,
    [CreatedBy]           NVARCHAR (256)  NULL,
    [UpdatedAt]           DATETIME2 (7)   NULL,
    [UpdatedBy]           NVARCHAR (256)  NULL,
    [IsActive]            BIT             NULL,
    [ID]                  INT             IDENTITY (1, 1) NOT NULL,
    [TenderId]            INT             NOT NULL,
    [QualificationYearId] INT             NOT NULL,
    [SupplierValue]       DECIMAL (18, 2) NOT NULL,
    [QualificationItemId] INT             NULL,
    [SupplierSelectedCr]  NVARCHAR (20)   NULL,
    CONSTRAINT [PK_QualificationSupplierDataYearly] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_QualificationSupplierDataYearly_QualificationItem_QualificationItemId] FOREIGN KEY ([QualificationItemId]) REFERENCES [Qualification].[QualificationItem] ([ID]),
    CONSTRAINT [FK_QualificationSupplierDataYearly_QualificationYear_QualificationYearId] FOREIGN KEY ([QualificationYearId]) REFERENCES [Qualification].[QualificationYear] ([ID]),
    CONSTRAINT [FK_QualificationSupplierDataYearly_Supplier_SupplierSelectedCr] FOREIGN KEY ([SupplierSelectedCr]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_QualificationSupplierDataYearly_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSupplierDataYearly_QualificationItemId]
    ON [Qualification].[QualificationSupplierDataYearly]([QualificationItemId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSupplierDataYearly_QualificationYearId]
    ON [Qualification].[QualificationSupplierDataYearly]([QualificationYearId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSupplierDataYearly_SupplierSelectedCr]
    ON [Qualification].[QualificationSupplierDataYearly]([SupplierSelectedCr] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSupplierDataYearly_TenderId]
    ON [Qualification].[QualificationSupplierDataYearly]([TenderId] ASC);

