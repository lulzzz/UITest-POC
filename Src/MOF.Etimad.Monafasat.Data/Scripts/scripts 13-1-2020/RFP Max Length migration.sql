DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplateTechnicalOutput]') AND [c].[name] = N'OutputStage');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplateTechnicalOutput] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplateTechnicalOutput] ALTER COLUMN [OutputStage] nvarchar(1000) NOT NULL;

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplateTechnicalOutput]') AND [c].[name] = N'OutputName');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplateTechnicalOutput] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplateTechnicalOutput] ALTER COLUMN [OutputName] nvarchar(1000) NOT NULL;

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplateTechnicalOutput]') AND [c].[name] = N'OutputDescriptions');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplateTechnicalOutput] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplateTechnicalOutput] ALTER COLUMN [OutputDescriptions] nvarchar(2000) NOT NULL;

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplateTechnicalOutput]') AND [c].[name] = N'OutputDeliveryTime');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplateTechnicalOutput] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplateTechnicalOutput] ALTER COLUMN [OutputDeliveryTime] nvarchar(100) NOT NULL;
 
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplateTechnicalDelrations]') AND [c].[name] = N'Term');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplateTechnicalDelrations] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplateTechnicalDelrations] ALTER COLUMN [Term] nvarchar(2000) NOT NULL;

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplateTechnicalDelrations]') AND [c].[name] = N'Decleration');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplateTechnicalDelrations] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplateTechnicalDelrations] ALTER COLUMN [Decleration] nvarchar(2000) NOT NULL;

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'WritePrice');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [WritePrice] nvarchar(max) NULL;

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'WorksProgram');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [WorksProgram] nvarchar(max) NULL;

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'WorkforceSpecifications');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [WorkforceSpecifications] nvarchar(max) NULL;

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'WorkLocationDetails');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [WorkLocationDetails] nvarchar(max) NULL;

GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'TenderFragmentation');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [TenderFragmentation] nvarchar(max) NULL;

GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'ServicesAndWorkImplementationsMethod');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [ServicesAndWorkImplementationsMethod] nvarchar(max) NULL;

GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'SafetySpecifications');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [SafetySpecifications] nvarchar(max) NULL;

GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'QualitySpecifications');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [QualitySpecifications] nvarchar(max) NULL;

GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'ProjectsScope');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [ProjectsScope] nvarchar(max) NULL;

GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'MaterialsSpecifications');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [MaterialsSpecifications] nvarchar(max) NULL;

GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'EquipmentsSpecifications');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [EquipmentsSpecifications] nvarchar(max) NULL;

GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'Description');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [Description] nvarchar(max) NULL;

GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'ConfirimJoiningTheTender');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [ConfirimJoiningTheTender] nvarchar(max) NULL;

GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'AlternativeEmailForCommuncation');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [AlternativeEmailForCommuncation] nvarchar(500) NULL;

GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'AgentJob');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [AgentJob] nvarchar(1000) NULL;

GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'AgentEmail');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [AgentEmail] nvarchar(500) NULL;

GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderConditionsTemplate]') AND [c].[name] = N'AgencyDecalration');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderConditionsTemplate] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [Tender].[TenderConditionsTemplate] ALTER COLUMN [AgencyDecalration] nvarchar(2000) NULL;

GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-14T10:56:13.4022970+03:00'
WHERE [TenderTypeId] = 1;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-14T10:56:13.4024099+03:00'
WHERE [TenderTypeId] = 2;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-14T10:56:13.4024122+03:00'
WHERE [TenderTypeId] = 3;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-14T10:56:13.4024123+03:00'
WHERE [TenderTypeId] = 4;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-14T10:56:13.4024164+03:00'
WHERE [TenderTypeId] = 5;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-14T10:56:13.4024165+03:00'
WHERE [TenderTypeId] = 6;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-14T10:56:13.4024167+03:00'
WHERE [TenderTypeId] = 7;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-14T10:56:13.4024168+03:00'
WHERE [TenderTypeId] = 8;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-14T10:56:13.4024169+03:00'
WHERE [TenderTypeId] = 9;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-14T10:56:13.4024171+03:00'
WHERE [TenderTypeId] = 10;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-14T10:56:13.4024172+03:00'
WHERE [TenderTypeId] = 11;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-14T10:56:13.4024173+03:00'
WHERE [TenderTypeId] = 12;
SELECT @@ROWCOUNT;


GO

UPDATE [LookUps].[TenderType] SET [CreatedAt] = '2020-01-14T10:56:13.4024174+03:00'
WHERE [TenderTypeId] = 13;
SELECT @@ROWCOUNT;


GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200114075615_RFPMaxLength', N'3.1.0');

GO

