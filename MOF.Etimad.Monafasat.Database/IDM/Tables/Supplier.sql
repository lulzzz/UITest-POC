CREATE TABLE [IDM].[Supplier] (
    [CreatedAt]      DATETIME2 (7)  NOT NULL,
    [CreatedBy]      NVARCHAR (256) NULL,
    [UpdatedAt]      DATETIME2 (7)  NULL,
    [UpdatedBy]      NVARCHAR (256) NULL,
    [IsActive]       BIT            NULL,
    [SelectedCr]     NVARCHAR (20)  NOT NULL,
    [SelectedCrName] NVARCHAR (200) NULL,
    CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED ([SelectedCr] ASC)
);

