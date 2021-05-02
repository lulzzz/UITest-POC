CREATE TABLE [AnnouncementTemplate].[AnnouncementMaintenanceRunnigWorkSupplierTemplate] (
    [Id]                       INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]                DATETIME2 (7)  NOT NULL,
    [CreatedBy]                NVARCHAR (256) NULL,
    [UpdatedAt]                DATETIME2 (7)  NULL,
    [UpdatedBy]                NVARCHAR (256) NULL,
    [IsActive]                 BIT            NULL,
    [MaintenanceRunningWorkId] INT            NOT NULL,
    [AnnouncementId]           INT            NOT NULL,
    CONSTRAINT [PK_AnnouncementMaintenanceRunnigWorkSupplierTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnouncementMaintenanceRunnigWorkSupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId] FOREIGN KEY ([AnnouncementId]) REFERENCES [AnnouncementTemplate].[AnnouncementSupplierTemplate] ([AnnouncementId]),
    CONSTRAINT [FK_AnnouncementMaintenanceRunnigWorkSupplierTemplate_MaintenanceRunningWork_MaintenanceRunningWorkId] FOREIGN KEY ([MaintenanceRunningWorkId]) REFERENCES [LookUps].[MaintenanceRunningWork] ([MaintenanceRunningWorkId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementMaintenanceRunnigWorkSupplierTemplate_AnnouncementId]
    ON [AnnouncementTemplate].[AnnouncementMaintenanceRunnigWorkSupplierTemplate]([AnnouncementId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AnnouncementMaintenanceRunnigWorkSupplierTemplate_MaintenanceRunningWorkId]
    ON [AnnouncementTemplate].[AnnouncementMaintenanceRunnigWorkSupplierTemplate]([MaintenanceRunningWorkId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define Identity of announcement maintenance runnig work supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementMaintenanceRunnigWorkSupplierTemplate', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define created date for announcement maintenance runnig work', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementMaintenanceRunnigWorkSupplierTemplate', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who cretead announcement maintenance runnig work', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementMaintenanceRunnigWorkSupplierTemplate', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define updated date for announcement maintenance runnig work', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementMaintenanceRunnigWorkSupplierTemplate', @level2type = N'COLUMN', @level2name = N'UpdatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determine who updated announcement maintenance runnig work', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementMaintenanceRunnigWorkSupplierTemplate', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define announcement maintenance runnig work is active or not', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementMaintenanceRunnigWorkSupplierTemplate', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define forigne key of maintenance running work', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementMaintenanceRunnigWorkSupplierTemplate', @level2type = N'COLUMN', @level2name = N'MaintenanceRunningWorkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define forigne key  of announcement supplier template', @level0type = N'SCHEMA', @level0name = N'AnnouncementTemplate', @level1type = N'TABLE', @level1name = N'AnnouncementMaintenanceRunnigWorkSupplierTemplate', @level2type = N'COLUMN', @level2name = N'AnnouncementId';

