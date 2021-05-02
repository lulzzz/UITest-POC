CREATE TABLE [dbo].[_AuditLog] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [TransactionId] NVARCHAR (50)  NULL,
    [AuditType]     NVARCHAR (1)   NULL,
    [TableName]     NVARCHAR (200) NULL,
    [TableSchema]   NVARCHAR (200) NULL,
    [UserName]      NVARCHAR (250) NULL,
    [TimeStamp]     DATETIME2 (7)  NOT NULL,
    [PrimaryKey]    NVARCHAR (50)  NULL,
    [OldData]       NVARCHAR (MAX) NULL,
    [NewData]       NVARCHAR (MAX) NULL,
    CONSTRAINT [PK__AuditLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

