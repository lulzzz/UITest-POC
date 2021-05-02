CREATE TABLE [Tender].[PostQualificationSuppliersInvitations] (
    [CreatedAt]                   DATETIME2 (7)  NOT NULL,
    [CreatedBy]                   NVARCHAR (256) NULL,
    [UpdatedAt]                   DATETIME2 (7)  NULL,
    [UpdatedBy]                   NVARCHAR (256) NULL,
    [IsActive]                    BIT            NULL,
    [PostQualificationSupplierId] INT            IDENTITY (1, 1) NOT NULL,
    [PostQualificationId]         INT            NOT NULL,
    [CommercialNumber]            NVARCHAR (20)  NULL,
    [StatusId]                    INT            NOT NULL,
    CONSTRAINT [PK_PostQualificationSuppliersInvitations] PRIMARY KEY CLUSTERED ([PostQualificationSupplierId] ASC),
    CONSTRAINT [FK_PostQualificationSuppliersInvitations_InvitationStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[InvitationStatus] ([InvitationStatusId]),
    CONSTRAINT [FK_PostQualificationSuppliersInvitations_Supplier_CommercialNumber] FOREIGN KEY ([CommercialNumber]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_PostQualificationSuppliersInvitations_Tender_PostQualificationId] FOREIGN KEY ([PostQualificationId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_PostQualificationSuppliersInvitations_CommercialNumber]
    ON [Tender].[PostQualificationSuppliersInvitations]([CommercialNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PostQualificationSuppliersInvitations_PostQualificationId]
    ON [Tender].[PostQualificationSuppliersInvitations]([PostQualificationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PostQualificationSuppliersInvitations_StatusId]
    ON [Tender].[PostQualificationSuppliersInvitations]([StatusId] ASC);

