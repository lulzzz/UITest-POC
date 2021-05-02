CREATE TABLE [Qualification].[QualificationSupplierData] (
    [CreatedAt]                    DATETIME2 (7)   NOT NULL,
    [CreatedBy]                    NVARCHAR (256)  NULL,
    [UpdatedAt]                    DATETIME2 (7)   NULL,
    [UpdatedBy]                    NVARCHAR (256)  NULL,
    [IsActive]                     BIT             NULL,
    [ID]                           INT             IDENTITY (1, 1) NOT NULL,
    [QualificationConfigurationId] INT             NOT NULL,
    [TenderId]                     INT             NOT NULL,
    [SupplierValue]                DECIMAL (18, 2) NOT NULL,
    [QualificationItemId]          INT             NULL,
    [PointValue]                   DECIMAL (18, 2) NOT NULL,
    [SupplierSelectedCr]           NVARCHAR (20)   NULL,
    [Weight]                       DECIMAL (18, 2) NOT NULL,
    [QualificationLookupId]        INT             NULL,
    [InsuranceProvider]            NVARCHAR (MAX)  NULL,
    [LevelOfCoverage]              DECIMAL (18, 2) NOT NULL,
    [CoverageExpireDate]           DATETIME2 (7)   NULL,
    CONSTRAINT [PK_QualificationSupplierData] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_QualificationSupplierData_QualificationConfiguration_QualificationConfigurationId] FOREIGN KEY ([QualificationConfigurationId]) REFERENCES [Qualification].[QualificationConfiguration] ([ID]),
    CONSTRAINT [FK_QualificationSupplierData_QualificationItem_QualificationItemId] FOREIGN KEY ([QualificationItemId]) REFERENCES [Qualification].[QualificationItem] ([ID]),
    CONSTRAINT [FK_QualificationSupplierData_QualificationLookup_QualificationLookupId] FOREIGN KEY ([QualificationLookupId]) REFERENCES [Qualification].[QualificationLookup] ([ID]),
    CONSTRAINT [FK_QualificationSupplierData_Supplier_SupplierSelectedCr] FOREIGN KEY ([SupplierSelectedCr]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_QualificationSupplierData_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSupplierData_QualificationConfigurationId]
    ON [Qualification].[QualificationSupplierData]([QualificationConfigurationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSupplierData_QualificationItemId]
    ON [Qualification].[QualificationSupplierData]([QualificationItemId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSupplierData_QualificationLookupId]
    ON [Qualification].[QualificationSupplierData]([QualificationLookupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSupplierData_SupplierSelectedCr]
    ON [Qualification].[QualificationSupplierData]([SupplierSelectedCr] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationSupplierData_TenderId]
    ON [Qualification].[QualificationSupplierData]([TenderId] ASC);

