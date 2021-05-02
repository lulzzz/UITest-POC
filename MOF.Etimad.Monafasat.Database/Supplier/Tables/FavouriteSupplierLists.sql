CREATE TABLE [Supplier].[FavouriteSupplierLists] (
    [CreatedAt]               DATETIME2 (7)  NOT NULL,
    [CreatedBy]               NVARCHAR (256) NULL,
    [UpdatedAt]               DATETIME2 (7)  NULL,
    [UpdatedBy]               NVARCHAR (256) NULL,
    [IsActive]                BIT            NULL,
    [FavouriteSupplierListId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]                    NVARCHAR (MAX) NULL,
    [AgencyCode]              NVARCHAR (20)  NULL,
    [BranchId]                INT            NOT NULL,
    CONSTRAINT [PK_FavouriteSupplierLists] PRIMARY KEY CLUSTERED ([FavouriteSupplierListId] ASC),
    CONSTRAINT [FK_FavouriteSupplierLists_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch].[Branch] ([BranchId]),
    CONSTRAINT [FK_FavouriteSupplierLists_GovAgency_AgencyCode] FOREIGN KEY ([AgencyCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode])
);


GO
CREATE NONCLUSTERED INDEX [IX_FavouriteSupplierLists_AgencyCode]
    ON [Supplier].[FavouriteSupplierLists]([AgencyCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FavouriteSupplierLists_BranchId]
    ON [Supplier].[FavouriteSupplierLists]([BranchId] ASC);

