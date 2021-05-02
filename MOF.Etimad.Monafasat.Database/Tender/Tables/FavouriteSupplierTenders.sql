CREATE TABLE [Tender].[FavouriteSupplierTenders] (
    [CreatedAt]        DATETIME2 (7)  NOT NULL,
    [CreatedBy]        NVARCHAR (256) NULL,
    [UpdatedAt]        DATETIME2 (7)  NULL,
    [UpdatedBy]        NVARCHAR (256) NULL,
    [IsActive]         BIT            NULL,
    [TenderId]         INT            NOT NULL,
    [SupplierCRNumber] NVARCHAR (25)  NOT NULL,
    CONSTRAINT [PK_FavouriteSupplierTenders] PRIMARY KEY CLUSTERED ([TenderId] ASC, [SupplierCRNumber] ASC),
    CONSTRAINT [FK_FavouriteSupplierTenders_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);

