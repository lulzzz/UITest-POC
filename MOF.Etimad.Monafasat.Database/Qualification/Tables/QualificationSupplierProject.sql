CREATE TABLE [Qualification].[QualificationSupplierProject] (
    [CreatedAt]                   DATETIME2 (7)   NOT NULL,
    [CreatedBy]                   NVARCHAR (256)  NULL,
    [UpdatedAt]                   DATETIME2 (7)   NULL,
    [UpdatedBy]                   NVARCHAR (256)  NULL,
    [IsActive]                    BIT             NULL,
    [ID]                          INT             IDENTITY (1, 1) NOT NULL,
    [TenderId]                    INT             NOT NULL,
    [StartDate]                   DATETIME2 (7)   NULL,
    [EndDate]                     DATETIME2 (7)   NULL,
    [ContractName]                NVARCHAR (MAX)  NULL,
    [Description]                 NVARCHAR (MAX)  NULL,
    [PerformanceEvaluation]       NVARCHAR (MAX)  NULL,
    [ContractValue]               DECIMAL (18, 2) NOT NULL,
    [OwnerName]                   NVARCHAR (MAX)  NULL,
    [PhoneNumber]                 NVARCHAR (MAX)  NULL,
    [EmailAddress]                NVARCHAR (MAX)  NULL,
    [QualificationSupplierDataId] INT             NOT NULL,
    [SupplierSelectedCr]          NVARCHAR (20)   NULL,
    CONSTRAINT [PK_QualificationSupplierProject] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_QualificationSupplierProject_QualificationSupplierData_QualificationSupplierDataId] FOREIGN KEY ([QualificationSupplierDataId]) REFERENCES [Qualification].[QualificationSupplierData] ([ID]),
    CONSTRAINT [FK_QualificationSupplierProject_Supplier_SupplierSelectedCr] FOREIGN KEY ([SupplierSelectedCr]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_QualificationSupplierProject_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSupplierProject_QualificationSupplierDataId]
    ON [Qualification].[QualificationSupplierProject]([QualificationSupplierDataId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSupplierProject_SupplierSelectedCr]
    ON [Qualification].[QualificationSupplierProject]([SupplierSelectedCr] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSupplierProject_TenderId]
    ON [Qualification].[QualificationSupplierProject]([TenderId] ASC);

