ALTER TABLE [Announcement].[Announcement] ADD [AgencyCode] nvarchar(20) NULL;

GO

ALTER TABLE [Announcement].[Announcement] ADD [ApprovedBy] nvarchar(200) NULL;

GO

ALTER TABLE [Announcement].[Announcement] ADD [BranchId] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Announcement].[Announcement] ADD [ReferenceNumber] nvarchar(100) NULL;

GO

CREATE INDEX [IX_Announcement_AgencyCode] ON [Announcement].[Announcement] ([AgencyCode]);

GO

CREATE INDEX [IX_Announcement_BranchId] ON [Announcement].[Announcement] ([BranchId]);

GO

ALTER TABLE [Announcement].[Announcement] ADD CONSTRAINT [FK_Announcement_GovAgency_AgencyCode] FOREIGN KEY ([AgencyCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode]) ON DELETE NO ACTION;

GO

ALTER TABLE [Announcement].[Announcement] ALTER COLUMN [BranchId] int NULL;

GO

ALTER TABLE [Announcement].[Announcement] ADD CONSTRAINT [FK_Announcement_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch].[Branch] ([BranchId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200508043612_Update_Announcement', N'3.1.0');

GO

