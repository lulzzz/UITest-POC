IF SCHEMA_ID(N'Settings') IS NULL EXEC(N'CREATE SCHEMA [Settings];');

GO

CREATE TABLE [Settings].[NationalProductSettings] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [Rating] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_NationalProductSettings] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200129094353_AddedNationalProductSettings', N'3.1.0');

GO