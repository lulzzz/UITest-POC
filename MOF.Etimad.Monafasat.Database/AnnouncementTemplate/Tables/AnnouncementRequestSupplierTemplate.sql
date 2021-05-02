CREATE TABLE [AnnouncementTemplate].[AnnouncementRequestSupplierTemplate] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [CreatedAt]       DATETIME2 (7)   NOT NULL,
    [CreatedBy]       NVARCHAR (256)  NULL,
    [UpdatedAt]       DATETIME2 (7)   NULL,
    [UpdatedBy]       NVARCHAR (256)  NULL,
    [IsActive]        BIT             NULL,
    [StatusId]        INT             NOT NULL,
    [AnnouncementId]  INT             NOT NULL,
    [Cr]              NVARCHAR (20)   NULL,
    [Notes]           NVARCHAR (2000) NULL,
    [RejectionReason] NVARCHAR (2000) NULL,
    [DeleteReason]    NVARCHAR (2000) NULL,
    CONSTRAINT [PK_AnnouncementRequestSupplierTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnouncementRequestSupplierTemplate_AnnouncementJoinRequestStatusSupplierTemplate_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[AnnouncementJoinRequestStatusSupplierTemplate] ([Id]),
    CONSTRAINT [FK_AnnouncementRequestSupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [AnnouncementTemplate].[AnnouncementSupplierTemplate] ([AnnouncementId]),
    CONSTRAINT [FK_AnnouncementRequestSupplierTemplate_Supplier_Cr] FOREIGN KEY ([Cr]) REFERENCES [IDM].[Supplier] ([SelectedCr])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementRequestSupplierTemplate_AnnouncementId]
    ON [AnnouncementTemplate].[AnnouncementRequestSupplierTemplate]([AnnouncementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementRequestSupplierTemplate_Cr]
    ON [AnnouncementTemplate].[AnnouncementRequestSupplierTemplate]([Cr] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementRequestSupplierTemplate_StatusId]
    ON [AnnouncementTemplate].[AnnouncementRequestSupplierTemplate]([StatusId] ASC);

