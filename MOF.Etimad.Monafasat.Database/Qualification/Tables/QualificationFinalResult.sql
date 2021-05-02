CREATE TABLE [Qualification].[QualificationFinalResult] (
    [CreatedAt]             DATETIME2 (7)   NOT NULL,
    [CreatedBy]             NVARCHAR (256)  NULL,
    [UpdatedAt]             DATETIME2 (7)   NULL,
    [UpdatedBy]             NVARCHAR (256)  NULL,
    [IsActive]              BIT             NULL,
    [ID]                    INT             IDENTITY (1, 1) NOT NULL,
    [TenderId]              INT             NOT NULL,
    [SupplierSelectedCr]    NVARCHAR (20)   NULL,
    [ResultValue]           DECIMAL (18, 2) NOT NULL,
    [QualificationLookupId] INT             NOT NULL,
    [FailingReason]         NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_QualificationFinalResult] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_QualificationFinalResult_QualificationLookup_QualificationLookupId] FOREIGN KEY ([QualificationLookupId]) REFERENCES [Qualification].[QualificationLookup] ([ID]),
    CONSTRAINT [FK_QualificationFinalResult_Supplier_SupplierSelectedCr] FOREIGN KEY ([SupplierSelectedCr]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_QualificationFinalResult_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationFinalResult_QualificationLookupId]
    ON [Qualification].[QualificationFinalResult]([QualificationLookupId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationFinalResult_SupplierSelectedCr]
    ON [Qualification].[QualificationFinalResult]([SupplierSelectedCr] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_QualificationFinalResult_TenderId]
    ON [Qualification].[QualificationFinalResult]([TenderId] ASC);

