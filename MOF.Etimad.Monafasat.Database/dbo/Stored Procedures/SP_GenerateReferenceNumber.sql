
 

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
 
