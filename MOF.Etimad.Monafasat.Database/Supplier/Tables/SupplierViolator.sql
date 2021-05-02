CREATE TABLE [Supplier].[SupplierViolator] (
    [CreatedAt]             DATETIME2 (7)  NOT NULL,
    [CreatedBy]             NVARCHAR (256) NULL,
    [UpdatedAt]             DATETIME2 (7)  NULL,
    [UpdatedBy]             NVARCHAR (256) NULL,
    [IsActive]              BIT            NULL,
    [SupplierViolatorId]    INT            IDENTITY (1, 1) NOT NULL,
    [TenderChangeRequestId] INT            NOT NULL,
    [CR]                    NVARCHAR (20)  NULL,
    CONSTRAINT [PK_SupplierViolator] PRIMARY KEY CLUSTERED ([SupplierViolatorId] ASC),
    CONSTRAINT [FK_SupplierViolator_Supplier_CR] FOREIGN KEY ([CR]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_SupplierViolator_TenderChangeRequest_TenderChangeRequestId] FOREIGN KEY ([TenderChangeRequestId]) REFERENCES [Tender].[TenderChangeRequest] ([TenderChangeRequestId])
);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierViolator_CR]
    ON [Supplier].[SupplierViolator]([CR] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierViolator_TenderChangeRequestId]
    ON [Supplier].[SupplierViolator]([TenderChangeRequestId] ASC);

