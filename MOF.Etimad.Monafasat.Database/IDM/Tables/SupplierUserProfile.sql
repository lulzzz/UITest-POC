CREATE TABLE [IDM].[SupplierUserProfile] (
    [CreatedAt]     DATETIME2 (7)  NOT NULL,
    [CreatedBy]     NVARCHAR (256) NULL,
    [UpdatedAt]     DATETIME2 (7)  NULL,
    [UpdatedBy]     NVARCHAR (256) NULL,
    [IsActive]      BIT            NULL,
    [SelectedCr]    NVARCHAR (20)  NOT NULL,
    [UserProfileId] INT            NOT NULL,
    CONSTRAINT [PK_SupplierUserProfile] PRIMARY KEY CLUSTERED ([SelectedCr] ASC, [UserProfileId] ASC),
    CONSTRAINT [FK_SupplierUserProfile_Supplier_SelectedCr] FOREIGN KEY ([SelectedCr]) REFERENCES [IDM].[Supplier] ([SelectedCr]) ON DELETE CASCADE,
    CONSTRAINT [FK_SupplierUserProfile_UserProfile_UserProfileId] FOREIGN KEY ([UserProfileId]) REFERENCES [IDM].[UserProfile] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SupplierUserProfile_UserProfileId]
    ON [IDM].[SupplierUserProfile]([UserProfileId] ASC);

