CREATE TABLE [AnnouncementTemplate].[AnnouncementActivitySupplierTemplate] (
    [AnnouncementActivityId] INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]              DATETIME2 (7)  NOT NULL,
    [CreatedBy]              NVARCHAR (256) NULL,
    [UpdatedAt]              DATETIME2 (7)  NULL,
    [UpdatedBy]              NVARCHAR (256) NULL,
    [IsActive]               BIT            NULL,
    [ActivityId]             INT            NOT NULL,
    [AnnouncementId]         INT            NOT NULL,
    CONSTRAINT [PK_AnnouncementActivitySupplierTemplate] PRIMARY KEY CLUSTERED ([AnnouncementActivityId] ASC),
    CONSTRAINT [FK_AnnouncementActivitySupplierTemplate_Activity_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [LookUps].[Activity] ([ActivityId]),
    CONSTRAINT [FK_AnnouncementActivitySupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [AnnouncementTemplate].[AnnouncementSupplierTemplate] ([AnnouncementId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementActivitySupplierTemplate_ActivityId]
    ON [AnnouncementTemplate].[AnnouncementActivitySupplierTemplate]([ActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementActivitySupplierTemplate_AnnouncementId]
    ON [AnnouncementTemplate].[AnnouncementActivitySupplierTemplate]([AnnouncementId] ASC);

