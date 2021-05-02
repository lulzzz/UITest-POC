CREATE TABLE [AnnouncementTemplate].[AnnouncementHistorySupplierTemplate] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [CreatedAt]       DATETIME2 (7)   NOT NULL,
    [CreatedBy]       NVARCHAR (256)  NULL,
    [UpdatedAt]       DATETIME2 (7)   NULL,
    [UpdatedBy]       NVARCHAR (256)  NULL,
    [IsActive]        BIT             NULL,
    [StatusId]        INT             NOT NULL,
    [RejectionReason] NVARCHAR (2000) NULL,
    [AnnouncementId]  INT             NOT NULL,
    [UserId]          INT             NOT NULL,
    CONSTRAINT [PK_AnnouncementHistorySupplierTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnouncementHistorySupplierTemplate_AnnouncementStatusSupplierTemplate_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [AnnouncementTemplate].[AnnouncementStatusSupplierTemplate] ([Id]),
    CONSTRAINT [FK_AnnouncementHistorySupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [AnnouncementTemplate].[AnnouncementSupplierTemplate] ([AnnouncementId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementHistorySupplierTemplate_AnnouncementId]
    ON [AnnouncementTemplate].[AnnouncementHistorySupplierTemplate]([AnnouncementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementHistorySupplierTemplate_StatusId]
    ON [AnnouncementTemplate].[AnnouncementHistorySupplierTemplate]([StatusId] ASC);

