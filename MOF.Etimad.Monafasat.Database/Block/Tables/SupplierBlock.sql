CREATE TABLE [Block].[SupplierBlock] (
    [CreatedAt]                      DATETIME2 (7)  NOT NULL,
    [CreatedBy]                      NVARCHAR (256) NULL,
    [UpdatedAt]                      DATETIME2 (7)  NULL,
    [UpdatedBy]                      NVARCHAR (256) NULL,
    [IsActive]                       BIT            NULL,
    [SupplierBlockId]                INT            IDENTITY (1, 1) NOT NULL,
    [CommercialRegistrationNo]       NVARCHAR (20)  NULL,
    [CommercialSupplierName]         NVARCHAR (200) NULL,
    [AdminFileName]                  NVARCHAR (200) NULL,
    [AdminFileNetReferenceId]        NVARCHAR (200) NULL,
    [SecretaryFileName]              NVARCHAR (200) NULL,
    [SecretaryFileNetReferenceId]    NVARCHAR (200) NULL,
    [BlockDetails]                   NVARCHAR (500) NULL,
    [BlockTypeId]                    INT            NULL,
    [SupplierTypeId]                 INT            NOT NULL,
    [LicenseNumber]                  NVARCHAR (20)  NULL,
    [IsOldBlock]                     BIT            NOT NULL,
    [OrganizationTypeId]             INT            NOT NULL,
    [CommercialRegistrationNoOrigin] NVARCHAR (20)  NULL,
    [ResolutionNumber]               NVARCHAR (20)  NULL,
    [BlockStartDate]                 DATETIME2 (7)  NULL,
    [BlockEndDate]                   DATETIME2 (7)  NULL,
    [AdminBlockReason]               NVARCHAR (500) NULL,
    [SecretaryBlockReason]           NVARCHAR (500) NULL,
    [ManagerRejectionReason]         NVARCHAR (500) NULL,
    [SecretaryRejectionReason]       NVARCHAR (500) NULL,
    [CommertialRegistrationNoOrigin] NVARCHAR (20)  NULL,
    [Punishment]                     NVARCHAR (500) NULL,
    [AgencyCode]                     NVARCHAR (20)  NULL,
    [BlockStatusId]                  INT            NOT NULL,
    [IsTotallyBlocked]               BIT            NOT NULL,
    CONSTRAINT [PK_SupplierBlock] PRIMARY KEY CLUSTERED ([SupplierBlockId] ASC),
    CONSTRAINT [FK_SupplierBlock_BlockStatus_BlockStatusId] FOREIGN KEY ([BlockStatusId]) REFERENCES [LookUps].[BlockStatus] ([BlockStatusId]),
    CONSTRAINT [FK_SupplierBlock_BlockType_BlockTypeId] FOREIGN KEY ([BlockTypeId]) REFERENCES [LookUps].[BlockType] ([BlockTypeId]),
    CONSTRAINT [FK_SupplierBlock_GovAgency_AgencyCode] FOREIGN KEY ([AgencyCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode]),
    CONSTRAINT [FK_SupplierBlock_Supplier_CommercialRegistrationNo] FOREIGN KEY ([CommercialRegistrationNo]) REFERENCES [IDM].[Supplier] ([SelectedCr])
);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierBlock_AgencyCode]
    ON [Block].[SupplierBlock]([AgencyCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierBlock_BlockStatusId]
    ON [Block].[SupplierBlock]([BlockStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierBlock_BlockTypeId]
    ON [Block].[SupplierBlock]([BlockTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierBlock_CommercialRegistrationNo]
    ON [Block].[SupplierBlock]([CommercialRegistrationNo] ASC);

