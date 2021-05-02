CREATE TABLE [AnnouncementTemplate].[AnnouncementTemplateJoinRequestHistory] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [CreatedAt]           DATETIME2 (7)   NOT NULL,
    [CreatedBy]           NVARCHAR (256)  NULL,
    [UpdatedAt]           DATETIME2 (7)   NULL,
    [UpdatedBy]           NVARCHAR (256)  NULL,
    [IsActive]            BIT             NULL,
    [UserId]              INT             NOT NULL,
    [RejectionReason]     NVARCHAR (2000) NULL,
    [DeleteReason]        NVARCHAR (2000) NULL,
    [JoinRequestId]       INT             NOT NULL,
    [JoinRequestStatusId] INT             NOT NULL,
    CONSTRAINT [PK_AnnouncementTemplateJoinRequestHistory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnouncementTemplateJoinRequestHistory_AnnouncementJoinRequestStatusSupplierTemplate_JoinRequestStatusId] FOREIGN KEY ([JoinRequestStatusId]) REFERENCES [LookUps].[AnnouncementJoinRequestStatusSupplierTemplate] ([Id]),
    CONSTRAINT [FK_AnnouncementTemplateJoinRequestHistory_AnnouncementRequestSupplierTemplate_JoinRequestId] FOREIGN KEY ([JoinRequestId]) REFERENCES [AnnouncementTemplate].[AnnouncementRequestSupplierTemplate] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementTemplateJoinRequestHistory_JoinRequestId]
    ON [AnnouncementTemplate].[AnnouncementTemplateJoinRequestHistory]([JoinRequestId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementTemplateJoinRequestHistory_JoinRequestStatusId]
    ON [AnnouncementTemplate].[AnnouncementTemplateJoinRequestHistory]([JoinRequestStatusId] ASC);

