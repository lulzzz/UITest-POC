

CREATE PROCEDURE [dbo].[SP_AddLogEntry] (
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
