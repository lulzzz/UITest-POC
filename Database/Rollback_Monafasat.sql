BEGIN TRANSACTION;

BEGIN TRY 
	Delete [Settings].[Foo_dummy] Where ID=1;
	
	Delete [Settings].[Foo_dummy] Where ID=2;
	
	Delete [Settings].[Foo_dummy] Where ID=3;
	
 END TRY  
 BEGIN CATCH 
     SELECT   
         ERROR_NUMBER() AS ErrorNumber  
        ,ERROR_SEVERITY() AS ErrorSeverity  
        ,ERROR_STATE() AS ErrorState  
        ,ERROR_PROCEDURE() AS ErrorProcedure  
        ,ERROR_LINE() AS ErrorLine  
        ,ERROR_MESSAGE() AS ErrorMessage;  
  
    IF @@TRANCOUNT > 0  
        ROLLBACK TRANSACTION; 
 END CATCH; 
 
IF @@TRANCOUNT > 0  
    COMMIT TRANSACTION;  
GO

DROP TABLE [Settings].[Foo_dummy];
GO 