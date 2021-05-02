
/****** Object:  Table [dbo].[_Log]    Script Date: 28/08/2017 5:36:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[_Log]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[_Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MachineName] [nvarchar](200) NULL,
	[SiteName] [nvarchar](200) NOT NULL,
	[Logged] [datetime] NOT NULL,
	[Level] [varchar](5) NOT NULL,
	[UserName] [nvarchar](200) NULL,
	[Message] [nvarchar](MAX) NOT NULL,
	[Logger] [nvarchar](300) NULL,
	[RelatedObjects] [nvarchar](max) NULL,
	[ServerName] [nvarchar](200) NULL,
	[Port] [varchar](100) NULL,
	[Url] [nvarchar](2000) NULL,
	[Https] [bit] NULL,
	[ServerAddress] [nvarchar](100) NULL,
	[RemoteAddress] [nvarchar](100) NULL,
	[Callsite] [nvarchar](300) NULL,
	[Exception] [nvarchar](max) NULL,
	[TraceIdentifier] [nvarchar](50) NULL,
 CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddLogEntry]    Script Date: 28/08/2017 5:36:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_AddLogEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_AddLogEntry] AS' 
END
GO


ALTER PROCEDURE [dbo].[SP_AddLogEntry] (
  @machineName nvarchar(200),
  @siteName nvarchar(200),
  @logged datetime,
  @level varchar(5),
  @userName nvarchar(200),
  @message nvarchar(max),
  @logger nvarchar(300),
  @relatedObjects nvarchar(max),
  @serverName nvarchar(200),
  @port varchar(100),
  @url nvarchar(2000),
  @https bit,
  @serverAddress nvarchar(100),
  @remoteAddress nvarchar(100),
  @callSite nvarchar(300),
  @exception nvarchar(max),
  @traceIdentifier nvarchar(50)
) AS
BEGIN


  INSERT INTO [dbo].[_Log] (
    [MachineName],
    [SiteName],
    [Logged],
    [Level],
    [UserName],
    [Message],
    [Logger],
    [RelatedObjects],
    [ServerName],
    [Port],
    [Url],
    [Https],
    [ServerAddress],
    [RemoteAddress],
    [CallSite],
    [Exception],
	[TraceIdentifier]
  ) VALUES (
    @machineName,
    @siteName,
    @logged,
    @level,
    @userName,
    @message,
    @logger,
    @relatedObjects,
    @serverName,
    @port,
    @url,
    @https,
    @serverAddress,
    @remoteAddress,
    @callSite,
    @exception,
	@traceIdentifier
  );
END
GO