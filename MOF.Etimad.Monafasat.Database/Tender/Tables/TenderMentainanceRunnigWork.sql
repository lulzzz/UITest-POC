CREATE TABLE [Tender].[TenderMentainanceRunnigWork] (
    [CreatedAt]                DATETIME2 (7)  NOT NULL,
    [CreatedBy]                NVARCHAR (256) NULL,
    [UpdatedAt]                DATETIME2 (7)  NULL,
    [UpdatedBy]                NVARCHAR (256) NULL,
    [IsActive]                 BIT            NULL,
    [Id]                       INT            IDENTITY (1, 1) NOT NULL,
    [MaintenanceRunningWorkId] INT            NOT NULL,
    [TenderId]                 INT            NOT NULL,
    CONSTRAINT [PK_TenderMentainanceRunnigWork] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenderMentainanceRunnigWork_MaintenanceRunningWork_MaintenanceRunningWorkId] FOREIGN KEY ([MaintenanceRunningWorkId]) REFERENCES [LookUps].[MaintenanceRunningWork] ([MaintenanceRunningWorkId]),
    CONSTRAINT [FK_TenderMentainanceRunnigWork_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderMentainanceRunnigWork_MaintenanceRunningWorkId]
    ON [Tender].[TenderMentainanceRunnigWork]([MaintenanceRunningWorkId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderMentainanceRunnigWork_TenderId]
    ON [Tender].[TenderMentainanceRunnigWork]([TenderId] ASC);

