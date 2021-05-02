CREATE TABLE [dbo].[_UserAudit] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (MAX) NULL,
    [Timestamp]  DATETIME2 (7)  NOT NULL,
    [UserName]   NVARCHAR (MAX) NULL,
    [FullName]   NVARCHAR (MAX) NULL,
    [Process]    NVARCHAR (MAX) NULL,
    [ProcessId]  NVARCHAR (MAX) NULL,
    [AuditEvent] NVARCHAR (MAX) NULL,
    [IpAddress]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK__UserAudit] PRIMARY KEY CLUSTERED ([Id] ASC)
);

