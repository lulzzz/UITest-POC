CREATE TABLE [LookUps].[MaintenanceRunningWork] (
    [MaintenanceRunningWorkId] INT             NOT NULL,
    [NameEn]                   NVARCHAR (1024) NULL,
    [NameAr]                   NVARCHAR (1024) NOT NULL,
    CONSTRAINT [PK_MaintenanceRunningWork] PRIMARY KEY CLUSTERED ([MaintenanceRunningWorkId] ASC)
);

