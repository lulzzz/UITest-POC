CREATE TABLE [dbo].[_Log] (
    [ID]              INT             IDENTITY (1, 1) NOT NULL,
    [MachineName]     NVARCHAR (200)  NULL,
    [SiteName]        NVARCHAR (200)  NOT NULL,
    [Logged]          DATETIME        NOT NULL,
    [Level]           VARCHAR (5)     NOT NULL,
    [UserName]        NVARCHAR (200)  NULL,
    [Message]         NVARCHAR (MAX)  NOT NULL,
    [Logger]          NVARCHAR (300)  NULL,
    [RelatedObjects]  NVARCHAR (MAX)  NULL,
    [ServerName]      NVARCHAR (200)  NULL,
    [Port]            VARCHAR (100)   NULL,
    [Url]             NVARCHAR (2000) NULL,
    [Https]           BIT             NULL,
    [ServerAddress]   NVARCHAR (100)  NULL,
    [RemoteAddress]   NVARCHAR (100)  NULL,
    [Callsite]        NVARCHAR (300)  NULL,
    [Exception]       NVARCHAR (MAX)  NULL,
    [TraceIdentifier] NVARCHAR (50)   NULL,
    CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED ([ID] ASC)
);

