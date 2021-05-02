IF SCHEMA_ID(N'Announcement') IS NULL EXEC(N'CREATE SCHEMA [Announcement];');

GO

CREATE TABLE [Announcement].[AnnouncementStatus] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(1024) NULL,
    CONSTRAINT [PK_AnnouncementStatus] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Announcement].[Announcement] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [AnnouncementName] nvarchar(1024) NOT NULL,
    [IntroAboutTender] nvarchar(max) NOT NULL,
    [AnnouncementPeriod] int NOT NULL,
    [InsidKsa] bit NOT NULL,
    [Details] nvarchar(max) NULL,
    [ActivityDescription] nvarchar(2000) NULL,
    [StatusId] int NOT NULL,
    CONSTRAINT [PK_Announcement] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Announcement_AnnouncementStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Announcement].[AnnouncementStatus] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Announcement].[AnnouncementActivity] (
    [TenderActivityId] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [ActivityId] int NOT NULL,
    [TenderId] int NOT NULL,
    [AnnouncementId] int NULL,
    CONSTRAINT [PK_AnnouncementActivity] PRIMARY KEY ([TenderActivityId]),
    CONSTRAINT [FK_AnnouncementActivity_Activity_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [LookUps].[Activity] ([ActivityId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementActivity_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementActivity_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Announcement].[AnnouncementArea] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [AreaId] int NOT NULL,
    [TenderId] int NOT NULL,
    [AnnouncementId] int NULL,
    CONSTRAINT [PK_AnnouncementArea] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnnouncementArea_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementArea_Area_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [LookUps].[Area] ([AreaId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementArea_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Announcement].[AnnouncementConstructionWork] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [ConstructionWorkId] int NOT NULL,
    [AnnouncementId] int NOT NULL,
    CONSTRAINT [PK_AnnouncementConstructionWork] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnnouncementConstructionWork_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementConstructionWork_ConstructionWork_ConstructionWorkId] FOREIGN KEY ([ConstructionWorkId]) REFERENCES [LookUps].[ConstructionWork] ([ConstructionWorkId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Announcement].[AnnouncementCountry] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [CountryId] int NOT NULL,
    [TenderId] int NOT NULL,
    [AnnouncementId] int NULL,
    CONSTRAINT [PK_AnnouncementCountry] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnnouncementCountry_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementCountry_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [LookUps].[Country] ([CountryId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementCountry_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Announcement].[AnnouncementHistory] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [StatusId] int NOT NULL,
    [RejectionReason] nvarchar(2000) NULL,
    [AnnouncementId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_AnnouncementHistory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnnouncementHistory_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementHistory_AnnouncementStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Announcement].[AnnouncementStatus] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Announcement].[AnnouncementMaintenanceRunnigWork] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    [MaintenanceRunningWorkId] int NOT NULL,
    [AnnouncementId] int NOT NULL,
    CONSTRAINT [PK_AnnouncementMaintenanceRunnigWork] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AnnouncementMaintenanceRunnigWork_Announcement_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement].[Announcement] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AnnouncementMaintenanceRunnigWork_MaintenanceRunningWork_MaintenanceRunningWorkId] FOREIGN KEY ([MaintenanceRunningWorkId]) REFERENCES [LookUps].[MaintenanceRunningWork] ([MaintenanceRunningWorkId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Announcement_StatusId] ON [Announcement].[Announcement] ([StatusId]);

GO

CREATE INDEX [IX_AnnouncementActivity_ActivityId] ON [Announcement].[AnnouncementActivity] ([ActivityId]);

GO

CREATE INDEX [IX_AnnouncementActivity_AnnouncementId] ON [Announcement].[AnnouncementActivity] ([AnnouncementId]);

GO

CREATE INDEX [IX_AnnouncementActivity_TenderId] ON [Announcement].[AnnouncementActivity] ([TenderId]);

GO

CREATE INDEX [IX_AnnouncementArea_AnnouncementId] ON [Announcement].[AnnouncementArea] ([AnnouncementId]);

GO

CREATE INDEX [IX_AnnouncementArea_AreaId] ON [Announcement].[AnnouncementArea] ([AreaId]);

GO

CREATE INDEX [IX_AnnouncementArea_TenderId] ON [Announcement].[AnnouncementArea] ([TenderId]);

GO

CREATE INDEX [IX_AnnouncementConstructionWork_AnnouncementId] ON [Announcement].[AnnouncementConstructionWork] ([AnnouncementId]);

GO

CREATE INDEX [IX_AnnouncementConstructionWork_ConstructionWorkId] ON [Announcement].[AnnouncementConstructionWork] ([ConstructionWorkId]);

GO

CREATE INDEX [IX_AnnouncementCountry_AnnouncementId] ON [Announcement].[AnnouncementCountry] ([AnnouncementId]);

GO

CREATE INDEX [IX_AnnouncementCountry_CountryId] ON [Announcement].[AnnouncementCountry] ([CountryId]);

GO

CREATE INDEX [IX_AnnouncementCountry_TenderId] ON [Announcement].[AnnouncementCountry] ([TenderId]);

GO

CREATE INDEX [IX_AnnouncementHistory_AnnouncementId] ON [Announcement].[AnnouncementHistory] ([AnnouncementId]);

GO

CREATE INDEX [IX_AnnouncementHistory_StatusId] ON [Announcement].[AnnouncementHistory] ([StatusId]);

GO

CREATE INDEX [IX_AnnouncementMaintenanceRunnigWork_AnnouncementId] ON [Announcement].[AnnouncementMaintenanceRunnigWork] ([AnnouncementId]);

GO

CREATE INDEX [IX_AnnouncementMaintenanceRunnigWork_MaintenanceRunningWorkId] ON [Announcement].[AnnouncementMaintenanceRunnigWork] ([MaintenanceRunningWorkId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200505152310_Init-Announcement', N'3.1.0');

GO

