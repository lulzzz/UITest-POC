


/****** Object:  Sequence [dbo].[ReferenceNumberSequence]    Script Date: 9/7/2019 5:11:01 AM ******/
CREATE SEQUENCE [dbo].[ReferenceNumberSequence] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 9223372036854775807
 CACHE 
GO


/****** Object:  Table [dbo].[_Config]    Script Date: 9/7/2019 5:12:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_Config](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK__Config] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[_Config] ON 
GO
INSERT [dbo].[_Config] ([Id], [Key], [Value]) VALUES (1, N'LastReferenceNumberDate', N'Jul  1 2019 11:05AM')
GO
SET IDENTITY_INSERT [dbo].[_Config] OFF
GO





 /****** Object:  StoredProcedure [dbo].[SP_GenerateReferenceNumber]    Script Date: 9/7/2019 5:12:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 

CREATE  PROCEDURE [dbo].[SP_GenerateReferenceNumber]
@ProcessId INT

AS

   --For Test
   --DECLARE @ProcessId INT = 2

   BEGIN TRAN;

   DECLARE @LastProcessYear INT
   DECLARE @LastProcessMonth INT
   DECLARE @SequenceNo INT
   DECLARE @RefNo NVARCHAR(12)

   --SELECT @LastProcessYear = YEAR(MAX(p.CreatedAt)) , @LastProcessMonth= MONTH(MAX(p.CreatedAt)) FROM [Workflow].ProcessInstance p
   SELECT @LastProcessYear = YEAR(MAX(CAST(GETDATE() AS date))) , @LastProcessMonth= MONTH(MAX(CAST(GETDATE() AS date))) FROM _config c
   WHERE c.[Key] = 'LastReferenceNumberDate'


   IF(YEAR(GETDATE()) <> @LastProcessYear OR MONTH(GETDATE()) <> @LastProcessMonth )
   BEGIN
       ALTER SEQUENCE ReferenceNumberSequence  RESTART

       UPDATE _config
       SET [Value] = GETDATE()
       WHERE [Key] = 'LastReferenceNumberDate'
   END


   SELECT @SequenceNo = NEXT VALUE FOR ReferenceNumberSequence;
   SELECT @RefNo = RIGHT('00' + YEAR(GETDATE()), 2)
                   + RIGHT('0' + RTRIM(MONTH(GETDATE())), 2)
                   + RIGHT('00' + CAST(@ProcessId AS VARCHAR(3)), 2)
                   + RIGHT('000000' +  CAST(@SequenceNo AS VARCHAR(6)) ,6);

   COMMIT;

   SELECT  @RefNo as ReferenceNumberSequence --, @SequenceNo ,  LEN(@RefNo)
   --var refNo = await _repository.ExcuteScalarAsync<string>("[SP_GenerateReferenceNumber] @ProcessId", new { ProcessId = processId });
 
GO


