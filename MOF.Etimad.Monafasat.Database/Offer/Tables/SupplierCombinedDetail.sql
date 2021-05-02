CREATE TABLE [Offer].[SupplierCombinedDetail] (
    [CreatedAt]                    DATETIME2 (7)  NOT NULL,
    [CreatedBy]                    NVARCHAR (256) NULL,
    [UpdatedAt]                    DATETIME2 (7)  NULL,
    [UpdatedBy]                    NVARCHAR (256) NULL,
    [IsActive]                     BIT            NULL,
    [CombinedDetailId]             INT            IDENTITY (1, 1) NOT NULL,
    [CombinedId]                   INT            NOT NULL,
    [IsChamberJoiningAttached]     BIT            NULL,
    [IsChamberJoiningValid]        BIT            NULL,
    [IsCommercialRegisterAttached] BIT            NULL,
    [IsCommercialRegisterValid]    BIT            NULL,
    [IsSaudizationAttached]        BIT            NULL,
    [IsSaudizationValidDate]       BIT            NULL,
    [IsSpecificationValidDate]     BIT            NULL,
    [IsSpecificationAttached]      BIT            NULL,
    [IsSocialInsuranceValidDate]   BIT            NULL,
    [IsSocialInsuranceAttached]    BIT            NULL,
    [IsZakatAttached]              BIT            NOT NULL,
    [IsZakatValidDate]             BIT            NOT NULL,
    CONSTRAINT [PK_SupplierCombinedDetail] PRIMARY KEY CLUSTERED ([CombinedDetailId] ASC),
    CONSTRAINT [FK_SupplierCombinedDetail_OfferSolidarity_CombinedId] FOREIGN KEY ([CombinedId]) REFERENCES [Offer].[OfferSolidarity] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_SupplierCombinedDetail_CombinedId]
    ON [Offer].[SupplierCombinedDetail]([CombinedId] ASC);

