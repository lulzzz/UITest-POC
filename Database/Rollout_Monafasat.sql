CREATE TABLE [Settings].[Foo_dummy] (
    [Id] int NOT NULL IDENTITY,
	[CustomerName] VARCHAR(100) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(256) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(256) NULL,
    [IsActive] bit NULL,
    CONSTRAINT [PK_NationalProductSettings_dummy] PRIMARY KEY ([Id])
);

GO

ALTER TABLE [Settings].[Foo_dummy] ADD [IsAddedFroDemo] bit NULL;

GO

BEGIN TRANSACTION;

BEGIN TRY 

	 insert into [Settings].[Foo_dummy] (CustomerName, createdAt) values ('Monafasat CI/CD pipeline', '2020-06-22 16:38:42.1999567');
	 
	 insert into [Settings].[Foo_dummy] (CustomerName, createdAt) values ('Monafasat CI/CD demo', '2020-06-22 16:38:43.1999567');
	 
	 
	 insert into [Settings].[Foo_dummy] (CustomerName, createdAt) values ('Monafasat CI/CD auto demo', '%qwerty');

	 
	 update [Settings].[Foo_dummy] set CustomerName = CustomerName +' Updated' where ID=3;
	
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