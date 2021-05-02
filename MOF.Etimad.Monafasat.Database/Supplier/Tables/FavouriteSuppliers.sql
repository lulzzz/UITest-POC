CREATE TABLE [Supplier].[FavouriteSuppliers] (
    [CreatedAt]               DATETIME2 (7)  NOT NULL,
    [CreatedBy]               NVARCHAR (256) NULL,
    [UpdatedAt]               DATETIME2 (7)  NULL,
    [UpdatedBy]               NVARCHAR (256) NULL,
    [IsActive]                BIT            NULL,
    [FavouriteSupplierId]     INT            IDENTITY (1, 1) NOT NULL,
    [FavouriteSupplierListId] INT            NOT NULL,
    [SupplierCRNumber]        NVARCHAR (20)  NULL,
    CONSTRAINT [PK_FavouriteSuppliers] PRIMARY KEY CLUSTERED ([FavouriteSupplierId] ASC),
    CONSTRAINT [FK_FavouriteSuppliers_FavouriteSupplierLists_FavouriteSupplierListId] FOREIGN KEY ([FavouriteSupplierListId]) REFERENCES [Supplier].[FavouriteSupplierLists] ([FavouriteSupplierListId]),
    CONSTRAINT [FK_FavouriteSuppliers_Supplier_SupplierCRNumber] FOREIGN KEY ([SupplierCRNumber]) REFERENCES [IDM].[Supplier] ([SelectedCr])
);


GO
CREATE NONCLUSTERED INDEX [IX_FavouriteSuppliers_FavouriteSupplierListId]
    ON [Supplier].[FavouriteSuppliers]([FavouriteSupplierListId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FavouriteSuppliers_SupplierCRNumber]
    ON [Supplier].[FavouriteSuppliers]([SupplierCRNumber] ASC);

