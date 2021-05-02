CREATE TABLE [Offer].[SupplierSpecificationDetail] (
    [CreatedAt]                DATETIME2 (7)  NOT NULL,
    [CreatedBy]                NVARCHAR (256) NULL,
    [UpdatedAt]                DATETIME2 (7)  NULL,
    [UpdatedBy]                NVARCHAR (256) NULL,
    [IsActive]                 BIT            NULL,
    [SpecificationId]          INT            IDENTITY (1, 1) NOT NULL,
    [IsForApplier]             BIT            NOT NULL,
    [Degree]                   INT            NULL,
    [ConstructionWorkId]       INT            NULL,
    [MaintenanceRunningWorkId] INT            NULL,
    [CompinedDetailId]         INT            NOT NULL,
    CONSTRAINT [PK_SupplierSpecificationDetail] PRIMARY KEY CLUSTERED ([SpecificationId] ASC),
    CONSTRAINT [FK_SupplierSpecificationDetail_ConstructionWork_ConstructionWorkId] FOREIGN KEY ([ConstructionWorkId]) REFERENCES [LookUps].[ConstructionWork] ([ConstructionWorkId]),
    CONSTRAINT [FK_SupplierSpecificationDetail_MaintenanceRunningWork_MaintenanceRunningWorkId] FOREIGN KEY ([MaintenanceRunningWorkId]) REFERENCES [LookUps].[MaintenanceRunningWork] ([MaintenanceRunningWorkId]),
    CONSTRAINT [FK_SupplierSpecificationDetail_SupplierCombinedDetail_CompinedDetailId] FOREIGN KEY ([CompinedDetailId]) REFERENCES [Offer].[SupplierCombinedDetail] ([CombinedDetailId])
);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierSpecificationDetail_CompinedDetailId]
    ON [Offer].[SupplierSpecificationDetail]([CompinedDetailId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierSpecificationDetail_ConstructionWorkId]
    ON [Offer].[SupplierSpecificationDetail]([ConstructionWorkId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierSpecificationDetail_MaintenanceRunningWorkId]
    ON [Offer].[SupplierSpecificationDetail]([MaintenanceRunningWorkId] ASC);

