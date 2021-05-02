CREATE TABLE [Announcement].[Announcement] (
    [CreatedAt]                      DATETIME2 (7)   NOT NULL,
    [CreatedBy]                      NVARCHAR (256)  NULL,
    [UpdatedAt]                      DATETIME2 (7)   NULL,
    [UpdatedBy]                      NVARCHAR (256)  NULL,
    [IsActive]                       BIT             NULL,
    [AnnouncementName]               NVARCHAR (1024) NULL,
    [IntroAboutTender]               NVARCHAR (MAX)  NULL,
    [AnnouncementPeriod]             INT             NOT NULL,
    [InsidKsa]                       BIT             NOT NULL,
    [Details]                        NVARCHAR (MAX)  NULL,
    [ActivityDescription]            NVARCHAR (2000) NULL,
    [StatusId]                       INT             NOT NULL,
    [PublishedDate]                  DATETIME2 (7)   NULL,
    [TenderTypeId]                   INT             DEFAULT ((0)) NOT NULL,
    [AnnouncementId]                 INT             IDENTITY (1, 1) NOT NULL,
    [ReasonForSelectingTenderTypeId] INT             DEFAULT ((0)) NOT NULL,
    [AgencyCode]                     NVARCHAR (20)   NULL,
    [ApprovedBy]                     NVARCHAR (200)  NULL,
    [BranchId]                       INT             DEFAULT ((0)) NULL,
    [ReferenceNumber]                NVARCHAR (100)  NULL,
    CONSTRAINT [PK_Announcement] PRIMARY KEY CLUSTERED ([AnnouncementId] ASC),
    CONSTRAINT [FK_Announcement_AnnouncementStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Announcement].[AnnouncementStatus] ([Id]),
    CONSTRAINT [FK_Announcement_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch].[Branch] ([BranchId]),
    CONSTRAINT [FK_Announcement_GovAgency_AgencyCode] FOREIGN KEY ([AgencyCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode]),
    CONSTRAINT [FK_Announcement_ReasonForPurchaseTenderType_ReasonForSelectingTenderTypeId] FOREIGN KEY ([ReasonForSelectingTenderTypeId]) REFERENCES [LookUps].[ReasonForPurchaseTenderType] ([Id]),
    CONSTRAINT [FK_Announcement_TenderType_TenderTypeId] FOREIGN KEY ([TenderTypeId]) REFERENCES [LookUps].[TenderType] ([TenderTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Announcement_StatusId]
    ON [Announcement].[Announcement]([StatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Announcement_TenderTypeId]
    ON [Announcement].[Announcement]([TenderTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Announcement_ReasonForSelectingTenderTypeId]
    ON [Announcement].[Announcement]([ReasonForSelectingTenderTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Announcement_AgencyCode]
    ON [Announcement].[Announcement]([AgencyCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Announcement_BranchId]
    ON [Announcement].[Announcement]([BranchId] ASC);

