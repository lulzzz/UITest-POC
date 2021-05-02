CREATE TABLE [Qualification].[SupplierPreQualificationDocument] (
    [CreatedAt]                          DATETIME2 (7)  NOT NULL,
    [CreatedBy]                          NVARCHAR (256) NULL,
    [UpdatedAt]                          DATETIME2 (7)  NULL,
    [UpdatedBy]                          NVARCHAR (256) NULL,
    [IsActive]                           BIT            NULL,
    [SupplierPreQualificationDocumentId] INT            IDENTITY (1, 1) NOT NULL,
    [SupplierId]                         NVARCHAR (20)  NULL,
    [PreQualificationId]                 INT            NOT NULL,
    [StatusId]                           INT            NULL,
    [PreQualificationResult]             INT            NULL,
    [RejectionReason]                    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_SupplierPreQualificationDocument] PRIMARY KEY CLUSTERED ([SupplierPreQualificationDocumentId] ASC),
    CONSTRAINT [FK_SupplierPreQualificationDocument_OfferStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[OfferStatus] ([OfferStatusId]),
    CONSTRAINT [FK_SupplierPreQualificationDocument_Supplier_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_SupplierPreQualificationDocument_Tender_PreQualificationId] FOREIGN KEY ([PreQualificationId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierPreQualificationDocument_PreQualificationId]
    ON [Qualification].[SupplierPreQualificationDocument]([PreQualificationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierPreQualificationDocument_StatusId]
    ON [Qualification].[SupplierPreQualificationDocument]([StatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierPreQualificationDocument_SupplierId]
    ON [Qualification].[SupplierPreQualificationDocument]([SupplierId] ASC);

