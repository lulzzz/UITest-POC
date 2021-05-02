--------------------------------Reda 24-2-2021---------------------------------------
Go

EXEC sp_addextendedproperty 'MS_Description', N'Describe the relation between Tender and Maintenance Runnig Work', 'SCHEMA', N'Tender', 'TABLE', N'TenderMentainanceRunnigWork';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Describe the relation between Tender and Country', 'SCHEMA', N'Tender', 'TABLE', N'TenderCountry';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Describe the Change request on Tender', 'SCHEMA', N'Tender', 'TABLE', N'TenderChangeRequest';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Describe the relation between Tender and Areas', 'SCHEMA', N'Tender', 'TABLE', N'TenderArea';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Describe the relation between Tender and Agencies', 'SCHEMA', N'Tender', 'TABLE', N'TenderAgreementAgency';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Describe the Tender activities', 'SCHEMA', N'Tender', 'TABLE', N'TenderActivity';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Describe the Atachment for tender', 'SCHEMA', N'Tender', 'TABLE', N'Attachment';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Contains Quantity table Items ', 'SCHEMA', N'Offer', 'TABLE', N'SupplierTenderQuantityTableItemJson';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Contains the user settings for every notification', 'SCHEMA', N'Notification', 'TABLE', N'UserNotificationSetting';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Contains the Data of Notifications', 'SCHEMA', N'Notification', 'TABLE', N'Notification';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'MobileAlert';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceTokenNotification';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceToken';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Define the Status of Second Stage Negotiation like [Approved , Supplier Agree , ETC..]', 'SCHEMA', N'LookUps', 'TABLE', N'SupplierSecondNegotiationStatus';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Describe the Notifications Templates and involved roles', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Define the diffrent categories of notification ', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationCategory';

GO

EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Lookups', 'TABLE', N'OfferPresentationWay';
EXEC sp_addextendedproperty 'MS_Description', N'Define the way that supplier can apply offer if it was on one file or two files', 'SCHEMA', N'Lookups', 'TABLE', N'OfferPresentationWay';

GO

EXEC sp_addextendedproperty 'MS_Description', N'look up for all cities', 'SCHEMA', N'Lookups', 'TABLE', N'City';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Contains the Invitations for uregistered suppliers', 'SCHEMA', N'Invitation', 'TABLE', N'UnRegisteredSuppliersInvitation';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Descripes the Auditing Data to track users Actions', 'SCHEMA', N'dbo', 'TABLE', N'_UserAudit';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Define the Quantity Table for the Negotiation', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'NegotiationSupplierQuantityTable';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Contain Data for Users for each Branch', 'SCHEMA', N'Branch', 'TABLE', N'BranchUser';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Contain the relation between Branches and Committees', 'SCHEMA', N'Branch', 'TABLE', N'BranchCommittee';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Contain the Branch address data', 'SCHEMA', N'Branch', 'TABLE', N'BranchAddresse';

GO

EXEC sp_addextendedproperty 'MS_Description', N'Contain the Brnach main data', 'SCHEMA', N'Branch', 'TABLE', N'Branch';

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Verification].[VerificationType]') AND [c].[name] = N'VerificationTypeName');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Verification].[VerificationType] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Verification].[VerificationType] ALTER COLUMN [VerificationTypeName] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define name of verification type', 'SCHEMA', N'Verification', 'TABLE', N'VerificationType', 'COLUMN', N'VerificationTypeName';

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Verification].[VerificationType]') AND [c].[name] = N'VerificationTypeId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Verification].[VerificationType] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Verification].[VerificationType] ALTER COLUMN [VerificationTypeId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define identity of verification type', 'SCHEMA', N'Verification', 'TABLE', N'VerificationType', 'COLUMN', N'VerificationTypeId';

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Verification].[VerificationCode]') AND [c].[name] = N'VerificationTypeId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Verification].[VerificationCode] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Verification].[VerificationCode] ALTER COLUMN [VerificationTypeId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'it''s a foreign  key that described type of verification code', 'SCHEMA', N'Verification', 'TABLE', N'VerificationCode', 'COLUMN', N'VerificationTypeId';

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Verification].[VerificationCode]') AND [c].[name] = N'VerificationCodeNo');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Verification].[VerificationCode] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Verification].[VerificationCode] ALTER COLUMN [VerificationCodeNo] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'describe verfication code number', 'SCHEMA', N'Verification', 'TABLE', N'VerificationCode', 'COLUMN', N'VerificationCodeNo';

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Verification].[VerificationCode]') AND [c].[name] = N'UserId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Verification].[VerificationCode] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Verification].[VerificationCode] ALTER COLUMN [UserId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'refer to who received verfication code', 'SCHEMA', N'Verification', 'TABLE', N'VerificationCode', 'COLUMN', N'UserId';

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Verification].[VerificationCode]') AND [c].[name] = N'UpdatedBy');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Verification].[VerificationCode] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Verification].[VerificationCode] ALTER COLUMN [UpdatedBy] nvarchar(256) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Determine who updated  verification code', 'SCHEMA', N'Verification', 'TABLE', N'VerificationCode', 'COLUMN', N'UpdatedBy';

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Verification].[VerificationCode]') AND [c].[name] = N'UpdatedAt');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Verification].[VerificationCode] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Verification].[VerificationCode] ALTER COLUMN [UpdatedAt] datetime2 NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define updated date of  verification code', 'SCHEMA', N'Verification', 'TABLE', N'VerificationCode', 'COLUMN', N'UpdatedAt';

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Verification].[VerificationCode]') AND [c].[name] = N'IsActive');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Verification].[VerificationCode] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Verification].[VerificationCode] ALTER COLUMN [IsActive] bit NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define  verification code  is active or not', 'SCHEMA', N'Verification', 'TABLE', N'VerificationCode', 'COLUMN', N'IsActive';

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Verification].[VerificationCode]') AND [c].[name] = N'ExpiredDate');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Verification].[VerificationCode] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Verification].[VerificationCode] ALTER COLUMN [ExpiredDate] datetime2 NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define expiry date of verfication code', 'SCHEMA', N'Verification', 'TABLE', N'VerificationCode', 'COLUMN', N'ExpiredDate';

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Verification].[VerificationCode]') AND [c].[name] = N'CreatedBy');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Verification].[VerificationCode] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Verification].[VerificationCode] ALTER COLUMN [CreatedBy] nvarchar(256) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Determine who cretead verification code', 'SCHEMA', N'Verification', 'TABLE', N'VerificationCode', 'COLUMN', N'CreatedBy';

GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Verification].[VerificationCode]') AND [c].[name] = N'CreatedAt');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Verification].[VerificationCode] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Verification].[VerificationCode] ALTER COLUMN [CreatedAt] datetime2 NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define created date of  verification code', 'SCHEMA', N'Verification', 'TABLE', N'VerificationCode', 'COLUMN', N'CreatedAt';

GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Verification].[VerificationCode]') AND [c].[name] = N'VerificationCodeId');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Verification].[VerificationCode] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Verification].[VerificationCode] ALTER COLUMN [VerificationCodeId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define identity of verification code', 'SCHEMA', N'Verification', 'TABLE', N'VerificationCode', 'COLUMN', N'VerificationCodeId';

GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[VacationsDate]') AND [c].[name] = N'VacationName');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[VacationsDate] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Tender].[VacationsDate] ALTER COLUMN [VacationName] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define name of vacation ', 'SCHEMA', N'Tender', 'TABLE', N'VacationsDate', 'COLUMN', N'VacationName';

GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[VacationsDate]') AND [c].[name] = N'VacationDate');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[VacationsDate] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Tender].[VacationsDate] ALTER COLUMN [VacationDate] datetime2 NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define date of vacation ', 'SCHEMA', N'Tender', 'TABLE', N'VacationsDate', 'COLUMN', N'VacationDate';

GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[VacationsDate]') AND [c].[name] = N'VacationId');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[VacationsDate] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Tender].[VacationsDate] ALTER COLUMN [VacationId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define identity of vacation', 'SCHEMA', N'Tender', 'TABLE', N'VacationsDate', 'COLUMN', N'VacationId';

GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderMentainanceRunnigWork]') AND [c].[name] = N'TenderId');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderMentainanceRunnigWork] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Tender].[TenderMentainanceRunnigWork] ALTER COLUMN [TenderId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Tender', 'SCHEMA', N'Tender', 'TABLE', N'TenderMentainanceRunnigWork', 'COLUMN', N'TenderId';

GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderMentainanceRunnigWork]') AND [c].[name] = N'MaintenanceRunningWorkId');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderMentainanceRunnigWork] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Tender].[TenderMentainanceRunnigWork] ALTER COLUMN [MaintenanceRunningWorkId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Maintenance Runnig Work', 'SCHEMA', N'Tender', 'TABLE', N'TenderMentainanceRunnigWork', 'COLUMN', N'MaintenanceRunningWorkId';

GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderMentainanceRunnigWork]') AND [c].[name] = N'Id');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderMentainanceRunnigWork] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Tender].[TenderMentainanceRunnigWork] ALTER COLUMN [Id] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the unique udentifier for Tender Maintenance Runnig Work', 'SCHEMA', N'Tender', 'TABLE', N'TenderMentainanceRunnigWork', 'COLUMN', N'Id';

GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderCountry]') AND [c].[name] = N'TenderId');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderCountry] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [Tender].[TenderCountry] ALTER COLUMN [TenderId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Tender', 'SCHEMA', N'Tender', 'TABLE', N'TenderCountry', 'COLUMN', N'TenderId';

GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderCountry]') AND [c].[name] = N'CountryId');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderCountry] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [Tender].[TenderCountry] ALTER COLUMN [CountryId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Country', 'SCHEMA', N'Tender', 'TABLE', N'TenderCountry', 'COLUMN', N'CountryId';

GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderCountry]') AND [c].[name] = N'Id');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderCountry] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [Tender].[TenderCountry] ALTER COLUMN [Id] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the unique udentifier for Tender Country', 'SCHEMA', N'Tender', 'TABLE', N'TenderCountry', 'COLUMN', N'Id';

GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderChangeRequest]') AND [c].[name] = N'TenderId');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderChangeRequest] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [Tender].[TenderChangeRequest] ALTER COLUMN [TenderId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the Related Tender', 'SCHEMA', N'Tender', 'TABLE', N'TenderChangeRequest', 'COLUMN', N'TenderId';

GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderChangeRequest]') AND [c].[name] = N'RequestedByRoleName');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderChangeRequest] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [Tender].[TenderChangeRequest] ALTER COLUMN [RequestedByRoleName] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the user role who created the Request', 'SCHEMA', N'Tender', 'TABLE', N'TenderChangeRequest', 'COLUMN', N'RequestedByRoleName';

GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderChangeRequest]') AND [c].[name] = N'RejectionReason');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderChangeRequest] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [Tender].[TenderChangeRequest] ALTER COLUMN [RejectionReason] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the Rejection Reason if the request was rejected', 'SCHEMA', N'Tender', 'TABLE', N'TenderChangeRequest', 'COLUMN', N'RejectionReason';

GO

DECLARE @var24 sysname;
SELECT @var24 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderChangeRequest]') AND [c].[name] = N'HasAlternativeOffer');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderChangeRequest] DROP CONSTRAINT [' + @var24 + '];');
ALTER TABLE [Tender].[TenderChangeRequest] ALTER COLUMN [HasAlternativeOffer] bit NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define if the Tender allow alternative offer, used if the request was Change in Quantity Table', 'SCHEMA', N'Tender', 'TABLE', N'TenderChangeRequest', 'COLUMN', N'HasAlternativeOffer';

GO

DECLARE @var25 sysname;
SELECT @var25 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderChangeRequest]') AND [c].[name] = N'ChangeRequestTypeId');
IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderChangeRequest] DROP CONSTRAINT [' + @var25 + '];');
ALTER TABLE [Tender].[TenderChangeRequest] ALTER COLUMN [ChangeRequestTypeId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the type of change if it was [Change Dates, Change in files ,  Cancelation Request ....ETC]', 'SCHEMA', N'Tender', 'TABLE', N'TenderChangeRequest', 'COLUMN', N'ChangeRequestTypeId';

GO

DECLARE @var26 sysname;
SELECT @var26 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderChangeRequest]') AND [c].[name] = N'ChangeRequestStatusId');
IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderChangeRequest] DROP CONSTRAINT [' + @var26 + '];');
ALTER TABLE [Tender].[TenderChangeRequest] ALTER COLUMN [ChangeRequestStatusId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the status of the Request', 'SCHEMA', N'Tender', 'TABLE', N'TenderChangeRequest', 'COLUMN', N'ChangeRequestStatusId';

GO

DECLARE @var27 sysname;
SELECT @var27 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderChangeRequest]') AND [c].[name] = N'CancelationReasonId');
IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderChangeRequest] DROP CONSTRAINT [' + @var27 + '];');
ALTER TABLE [Tender].[TenderChangeRequest] ALTER COLUMN [CancelationReasonId] int NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the reason why the  Cancelation Request created', 'SCHEMA', N'Tender', 'TABLE', N'TenderChangeRequest', 'COLUMN', N'CancelationReasonId';

GO

DECLARE @var28 sysname;
SELECT @var28 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderChangeRequest]') AND [c].[name] = N'CancelationReasonDescription');
IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderChangeRequest] DROP CONSTRAINT [' + @var28 + '];');
ALTER TABLE [Tender].[TenderChangeRequest] ALTER COLUMN [CancelationReasonDescription] nvarchar(1000) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the Cancelation Request Reason Description', 'SCHEMA', N'Tender', 'TABLE', N'TenderChangeRequest', 'COLUMN', N'CancelationReasonDescription';

GO

DECLARE @var29 sysname;
SELECT @var29 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderChangeRequest]') AND [c].[name] = N'TenderChangeRequestId');
IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderChangeRequest] DROP CONSTRAINT [' + @var29 + '];');
ALTER TABLE [Tender].[TenderChangeRequest] ALTER COLUMN [TenderChangeRequestId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the unique udentifier for Tender Change Request', 'SCHEMA', N'Tender', 'TABLE', N'TenderChangeRequest', 'COLUMN', N'TenderChangeRequestId';

GO

DECLARE @var30 sysname;
SELECT @var30 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderArea]') AND [c].[name] = N'TenderId');
IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderArea] DROP CONSTRAINT [' + @var30 + '];');
ALTER TABLE [Tender].[TenderArea] ALTER COLUMN [TenderId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Tender', 'SCHEMA', N'Tender', 'TABLE', N'TenderArea', 'COLUMN', N'TenderId';

GO

DECLARE @var31 sysname;
SELECT @var31 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderArea]') AND [c].[name] = N'AreaId');
IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderArea] DROP CONSTRAINT [' + @var31 + '];');
ALTER TABLE [Tender].[TenderArea] ALTER COLUMN [AreaId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Area', 'SCHEMA', N'Tender', 'TABLE', N'TenderArea', 'COLUMN', N'AreaId';

GO

DECLARE @var32 sysname;
SELECT @var32 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderArea]') AND [c].[name] = N'Id');
IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderArea] DROP CONSTRAINT [' + @var32 + '];');
ALTER TABLE [Tender].[TenderArea] ALTER COLUMN [Id] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the unique udentifier for Tender area', 'SCHEMA', N'Tender', 'TABLE', N'TenderArea', 'COLUMN', N'Id';

GO

DECLARE @var33 sysname;
SELECT @var33 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderAgreementAgency]') AND [c].[name] = N'TenderId');
IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderAgreementAgency] DROP CONSTRAINT [' + @var33 + '];');
ALTER TABLE [Tender].[TenderAgreementAgency] ALTER COLUMN [TenderId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Tender', 'SCHEMA', N'Tender', 'TABLE', N'TenderAgreementAgency', 'COLUMN', N'TenderId';

GO

DECLARE @var34 sysname;
SELECT @var34 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderAgreementAgency]') AND [c].[name] = N'AgencyCode');
IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderAgreementAgency] DROP CONSTRAINT [' + @var34 + '];');
ALTER TABLE [Tender].[TenderAgreementAgency] ALTER COLUMN [AgencyCode] nvarchar(20) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Agency', 'SCHEMA', N'Tender', 'TABLE', N'TenderAgreementAgency', 'COLUMN', N'AgencyCode';

GO

DECLARE @var35 sysname;
SELECT @var35 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderAgreementAgency]') AND [c].[name] = N'TenderAgreementAgencyId');
IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderAgreementAgency] DROP CONSTRAINT [' + @var35 + '];');
ALTER TABLE [Tender].[TenderAgreementAgency] ALTER COLUMN [TenderAgreementAgencyId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the unique udentifier for Tender Agreement Agency', 'SCHEMA', N'Tender', 'TABLE', N'TenderAgreementAgency', 'COLUMN', N'TenderAgreementAgencyId';

GO

DECLARE @var36 sysname;
SELECT @var36 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderActivity]') AND [c].[name] = N'TenderId');
IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderActivity] DROP CONSTRAINT [' + @var36 + '];');
ALTER TABLE [Tender].[TenderActivity] ALTER COLUMN [TenderId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Tender', 'SCHEMA', N'Tender', 'TABLE', N'TenderActivity', 'COLUMN', N'TenderId';

GO

DECLARE @var37 sysname;
SELECT @var37 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderActivity]') AND [c].[name] = N'ActivityId');
IF @var37 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderActivity] DROP CONSTRAINT [' + @var37 + '];');
ALTER TABLE [Tender].[TenderActivity] ALTER COLUMN [ActivityId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Activity', 'SCHEMA', N'Tender', 'TABLE', N'TenderActivity', 'COLUMN', N'ActivityId';

GO

DECLARE @var38 sysname;
SELECT @var38 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[TenderActivity]') AND [c].[name] = N'TenderActivityId');
IF @var38 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[TenderActivity] DROP CONSTRAINT [' + @var38 + '];');
ALTER TABLE [Tender].[TenderActivity] ALTER COLUMN [TenderActivityId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the unique udentifier for Tender Activity', 'SCHEMA', N'Tender', 'TABLE', N'TenderActivity', 'COLUMN', N'TenderActivityId';

GO

DECLARE @var39 sysname;
SELECT @var39 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'VROCommitteeId');
IF @var39 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var39 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [VROCommitteeId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'VROCommitteeId';
EXEC sp_addextendedproperty 'MS_Description', N'The committe of VRO for opening, checking and awarding Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'VROCommitteeId';

GO

DECLARE @var40 sysname;
SELECT @var40 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'TenderUnitStatusId');
IF @var40 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var40 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [TenderUnitStatusId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'TenderUnitStatusId';
EXEC sp_addextendedproperty 'MS_Description', N'The status of Tender at unit review', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'TenderUnitStatusId';

GO

DECLARE @var41 sysname;
SELECT @var41 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'TenderTypeOtherSelectionReason');
IF @var41 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var41 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [TenderTypeOtherSelectionReason] nvarchar(1024) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'TenderTypeOtherSelectionReason';
EXEC sp_addextendedproperty 'MS_Description', N'Define the reason of selecting direct purchase or limited Tender that not exist in reasons list', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'TenderTypeOtherSelectionReason';

GO

DECLARE @var42 sysname;
SELECT @var42 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'TenderTypeId');
IF @var42 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var42 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [TenderTypeId] int NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'TenderTypeId';
EXEC sp_addextendedproperty 'MS_Description', N'Type of Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'TenderTypeId';

GO

DECLARE @var43 sysname;
SELECT @var43 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'TenderStatusId');
IF @var43 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var43 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [TenderStatusId] int NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'TenderStatusId';
EXEC sp_addextendedproperty 'MS_Description', N'Status of Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'TenderStatusId';

GO

DECLARE @var44 sysname;
SELECT @var44 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'TenderFirstStageId');
IF @var44 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var44 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [TenderFirstStageId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'TenderFirstStageId';
EXEC sp_addextendedproperty 'MS_Description', N'The identity of Tender of type first stage', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'TenderFirstStageId';

GO

DECLARE @var45 sysname;
SELECT @var45 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'TenderConditionsTemplateId');
IF @var45 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var45 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [TenderConditionsTemplateId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'TenderConditionsTemplateId';
EXEC sp_addextendedproperty 'MS_Description', N'The Activity template of Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'TenderConditionsTemplateId';

GO

DECLARE @var46 sysname;
SELECT @var46 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'TenderAwardingType');
IF @var46 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var46 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [TenderAwardingType] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'TenderAwardingType';
EXEC sp_addextendedproperty 'MS_Description', N'Define the awarding type of Tender partial or total awarding', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'TenderAwardingType';

GO

DECLARE @var47 sysname;
SELECT @var47 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'SupplierNeedSample');
IF @var47 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var47 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [SupplierNeedSample] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'SupplierNeedSample';
EXEC sp_addextendedproperty 'MS_Description', N'Boolean detrmine tf the supplier need samples of the Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'SupplierNeedSample';

GO

DECLARE @var48 sysname;
SELECT @var48 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'SubmitionDate');
IF @var48 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var48 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [SubmitionDate] datetime2 NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'SubmitionDate';
EXEC sp_addextendedproperty 'MS_Description', N'Define the publish/approval date of Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'SubmitionDate';

GO

DECLARE @var49 sysname;
SELECT @var49 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'SpendingCategoryId');
IF @var49 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var49 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [SpendingCategoryId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'SpendingCategoryId';
EXEC sp_addextendedproperty 'MS_Description', N'Define the spending Category of the Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'SpendingCategoryId';

GO

DECLARE @var50 sysname;
SELECT @var50 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'SamplesDeliveryAddress');
IF @var50 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var50 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [SamplesDeliveryAddress] nvarchar(2048) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'SamplesDeliveryAddress';
EXEC sp_addextendedproperty 'MS_Description', N'Define the address of samples deleviry of the Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'SamplesDeliveryAddress';

GO

DECLARE @var51 sysname;
SELECT @var51 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'ReasonForPurchaseTenderTypeId');
IF @var51 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var51 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [ReasonForPurchaseTenderTypeId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'ReasonForPurchaseTenderTypeId';
EXEC sp_addextendedproperty 'MS_Description', N'The reason of selecting direct purchase Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'ReasonForPurchaseTenderTypeId';

GO

DECLARE @var52 sysname;
SELECT @var52 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'ReasonForLimitedTenderTypeId');
IF @var52 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var52 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [ReasonForLimitedTenderTypeId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'ReasonForLimitedTenderTypeId';
EXEC sp_addextendedproperty 'MS_Description', N'The reason of selecting limited Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'ReasonForLimitedTenderTypeId';

GO

DECLARE @var53 sysname;
SELECT @var53 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'PreQualificationId');
IF @var53 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var53 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [PreQualificationId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'PreQualificationId';
EXEC sp_addextendedproperty 'MS_Description', N'The identity that identify pre qualification on the Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'PreQualificationId';

GO

DECLARE @var54 sysname;
SELECT @var54 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'PreAnnouncementId');
IF @var54 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var54 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [PreAnnouncementId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'PreAnnouncementId';
EXEC sp_addextendedproperty 'MS_Description', N'Tha pre-announcement related to Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'PreAnnouncementId';

GO

DECLARE @var55 sysname;
SELECT @var55 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'PostQualificationTenderId');
IF @var55 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var55 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [PostQualificationTenderId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'PostQualificationTenderId';
EXEC sp_addextendedproperty 'MS_Description', N'The identity that identify post qualification on the Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'PostQualificationTenderId';

GO

DECLARE @var56 sysname;
SELECT @var56 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'OffersOpeningAddressId');
IF @var56 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var56 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [OffersOpeningAddressId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'OffersOpeningAddressId';
EXEC sp_addextendedproperty 'MS_Description', N'The Identity that identify address of open Tender offers', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'OffersOpeningAddressId';

GO

DECLARE @var57 sysname;
SELECT @var57 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'OffersCheckingCommitteeId');
IF @var57 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var57 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [OffersCheckingCommitteeId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'OffersCheckingCommitteeId';
EXEC sp_addextendedproperty 'MS_Description', N'The Checking committee for checking and awarding Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'OffersCheckingCommitteeId';

GO

DECLARE @var58 sysname;
SELECT @var58 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'LastEnqueriesDate');
IF @var58 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var58 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [LastEnqueriesDate] datetime2 NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'LastEnqueriesDate';
EXEC sp_addextendedproperty 'MS_Description', N'Define the last date that supplier can enquiry or ask questions for Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'LastEnqueriesDate';

GO

DECLARE @var59 sysname;
SELECT @var59 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'IsSendToEmarketPlace');
IF @var59 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var59 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [IsSendToEmarketPlace] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'IsSendToEmarketPlace';
EXEC sp_addextendedproperty 'MS_Description', N'Flag determine if the awarded Tender is sent to e-market place or not', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'IsSendToEmarketPlace';

GO

DECLARE @var60 sysname;
SELECT @var60 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'IsNotificationSentForStoppingPeriod');
IF @var60 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var60 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [IsNotificationSentForStoppingPeriod] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'IsNotificationSentForStoppingPeriod';
EXEC sp_addextendedproperty 'MS_Description', N'Flag determine if a notification sent after finishing the stoping period of Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'IsNotificationSentForStoppingPeriod';

GO

DECLARE @var61 sysname;
SELECT @var61 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'IsLowBudgetDirectPurchase');
IF @var61 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var61 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [IsLowBudgetDirectPurchase] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'IsLowBudgetDirectPurchase';
EXEC sp_addextendedproperty 'MS_Description', N'determine if the Tender is low budget or not if the estimatinn value less than 30000 sar', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'IsLowBudgetDirectPurchase';

GO

DECLARE @var62 sysname;
SELECT @var62 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'InvitationTypeId');
IF @var62 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var62 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [InvitationTypeId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'InvitationTypeId';
EXEC sp_addextendedproperty 'MS_Description', N'Type of invitation on Tender public or private', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'InvitationTypeId';

GO

DECLARE @var63 sysname;
SELECT @var63 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'InsideKSA');
IF @var63 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var63 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [InsideKSA] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'InsideKSA';
EXEC sp_addextendedproperty 'MS_Description', N'Define the location of Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'InsideKSA';

GO

DECLARE @var64 sysname;
SELECT @var64 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'HasGuarantee');
IF @var64 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var64 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [HasGuarantee] bit NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'HasGuarantee';
EXEC sp_addextendedproperty 'MS_Description', N'Boolean determine if the Tender requires a final guarantee or not', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'HasGuarantee';

GO

DECLARE @var65 sysname;
SELECT @var65 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'EstimatedValue');
IF @var65 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var65 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [EstimatedValue] decimal(18,2) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'EstimatedValue';
EXEC sp_addextendedproperty 'MS_Description', N'Define the estimation value of Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'EstimatedValue';

GO

DECLARE @var66 sysname;
SELECT @var66 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'DirectPurchaseCommitteeId');
IF @var66 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var66 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [DirectPurchaseCommitteeId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'DirectPurchaseCommitteeId';
EXEC sp_addextendedproperty 'MS_Description', N'The direct purchase committee for checking and awarding Tender from type direct purchase', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'DirectPurchaseCommitteeId';

GO

DECLARE @var67 sysname;
SELECT @var67 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'Details');
IF @var67 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var67 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [Details] nvarchar(max) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'Details';
EXEC sp_addextendedproperty 'MS_Description', N'Define more information about Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'Details';

GO

DECLARE @var68 sysname;
SELECT @var68 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'CreatedByTypeId');
IF @var68 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var68 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [CreatedByTypeId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'CreatedByTypeId';
EXEC sp_addextendedproperty 'MS_Description', N'determine the Tender created by vro or agency or agency related by vro', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'CreatedByTypeId';

GO

DECLARE @var69 sysname;
SELECT @var69 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'BranchId');
IF @var69 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var69 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [BranchId] int NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'BranchId';
EXEC sp_addextendedproperty 'MS_Description', N'The branch that create a Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'BranchId';

GO

DECLARE @var70 sysname;
SELECT @var70 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'AwardingStoppingPeriod');
IF @var70 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var70 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [AwardingStoppingPeriod] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'AwardingStoppingPeriod';
EXEC sp_addextendedproperty 'MS_Description', N'Define the period that suppliers can add plaint on Tender after awarding between 5 and 10 days', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'AwardingStoppingPeriod';

GO

DECLARE @var71 sysname;
SELECT @var71 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'AnnouncementTemplateId');
IF @var71 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var71 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [AnnouncementTemplateId] int NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'AnnouncementTemplateId';
EXEC sp_addextendedproperty 'MS_Description', N'Tha announcement list of suppliers related to Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'AnnouncementTemplateId';

GO

DECLARE @var72 sysname;
SELECT @var72 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'AgencyCode');
IF @var72 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var72 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [AgencyCode] nvarchar(20) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'AgencyCode';
EXEC sp_addextendedproperty 'MS_Description', N'The Agency code of agency that create a Tender', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'AgencyCode';

GO

DECLARE @var73 sysname;
SELECT @var73 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Tender]') AND [c].[name] = N'ActivityDescription');
IF @var73 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Tender] DROP CONSTRAINT [' + @var73 + '];');
ALTER TABLE [Tender].[Tender] ALTER COLUMN [ActivityDescription] nvarchar(2000) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'ActivityDescription';
EXEC sp_addextendedproperty 'MS_Description', N'Define the description of Tender activiites', 'SCHEMA', N'Tender', 'TABLE', N'Tender', 'COLUMN', N'ActivityDescription';

GO

DECLARE @var74 sysname;
SELECT @var74 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[ConditionsBooklet]') AND [c].[name] = N'TenderId');
IF @var74 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[ConditionsBooklet] DROP CONSTRAINT [' + @var74 + '];');
ALTER TABLE [Tender].[ConditionsBooklet] ALTER COLUMN [TenderId] int NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Tender', 'TABLE', N'ConditionsBooklet', 'COLUMN', N'TenderId';
EXEC sp_addextendedproperty 'MS_Description', N'it''s a forigne key that described Tender related to condtion booklet', 'SCHEMA', N'Tender', 'TABLE', N'ConditionsBooklet', 'COLUMN', N'TenderId';

GO

DECLARE @var75 sysname;
SELECT @var75 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Attachment]') AND [c].[name] = N'TenderId');
IF @var75 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Attachment] DROP CONSTRAINT [' + @var75 + '];');
ALTER TABLE [Tender].[Attachment] ALTER COLUMN [TenderId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Tender', 'SCHEMA', N'Tender', 'TABLE', N'Attachment', 'COLUMN', N'TenderId';

GO

DECLARE @var76 sysname;
SELECT @var76 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Attachment]') AND [c].[name] = N'ReviewStatusId');
IF @var76 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Attachment] DROP CONSTRAINT [' + @var76 + '];');
ALTER TABLE [Tender].[Attachment] ALTER COLUMN [ReviewStatusId] int NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define status of review ', 'SCHEMA', N'Tender', 'TABLE', N'Attachment', 'COLUMN', N'ReviewStatusId';

GO

DECLARE @var77 sysname;
SELECT @var77 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Attachment]') AND [c].[name] = N'RejectionReason');
IF @var77 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Attachment] DROP CONSTRAINT [' + @var77 + '];');
ALTER TABLE [Tender].[Attachment] ALTER COLUMN [RejectionReason] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Rejection reason if there were change request on the attachment', 'SCHEMA', N'Tender', 'TABLE', N'Attachment', 'COLUMN', N'RejectionReason';

GO

DECLARE @var78 sysname;
SELECT @var78 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Attachment]') AND [c].[name] = N'Name');
IF @var78 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Attachment] DROP CONSTRAINT [' + @var78 + '];');
ALTER TABLE [Tender].[Attachment] ALTER COLUMN [Name] nvarchar(200) NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'The name of the file attached', 'SCHEMA', N'Tender', 'TABLE', N'Attachment', 'COLUMN', N'Name';

GO

DECLARE @var79 sysname;
SELECT @var79 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Attachment]') AND [c].[name] = N'FileNetReferenceId');
IF @var79 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Attachment] DROP CONSTRAINT [' + @var79 + '];');
ALTER TABLE [Tender].[Attachment] ALTER COLUMN [FileNetReferenceId] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the file net reference Id ', 'SCHEMA', N'Tender', 'TABLE', N'Attachment', 'COLUMN', N'FileNetReferenceId';

GO

DECLARE @var80 sysname;
SELECT @var80 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Attachment]') AND [c].[name] = N'ChangeStatusId');
IF @var80 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Attachment] DROP CONSTRAINT [' + @var80 + '];');
ALTER TABLE [Tender].[Attachment] ALTER COLUMN [ChangeStatusId] int NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the the status of change request', 'SCHEMA', N'Tender', 'TABLE', N'Attachment', 'COLUMN', N'ChangeStatusId';

GO

DECLARE @var81 sysname;
SELECT @var81 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Attachment]') AND [c].[name] = N'AttachmentTypeId');
IF @var81 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Attachment] DROP CONSTRAINT [' + @var81 + '];');
ALTER TABLE [Tender].[Attachment] ALTER COLUMN [AttachmentTypeId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'The category of this attachment [Condition bocklet  ETC.... ]', 'SCHEMA', N'Tender', 'TABLE', N'Attachment', 'COLUMN', N'AttachmentTypeId';

GO

DECLARE @var82 sysname;
SELECT @var82 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tender].[Attachment]') AND [c].[name] = N'TenderAttachmentId');
IF @var82 IS NOT NULL EXEC(N'ALTER TABLE [Tender].[Attachment] DROP CONSTRAINT [' + @var82 + '];');
ALTER TABLE [Tender].[Attachment] ALTER COLUMN [TenderAttachmentId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the unique udentifier for Tender Attachment', 'SCHEMA', N'Tender', 'TABLE', N'Attachment', 'COLUMN', N'TenderAttachmentId';

GO

DECLARE @var83 sysname;
SELECT @var83 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierTenderQuantityTableItemJson]') AND [c].[name] = N'SupplierTenderQuantityTableItems');
IF @var83 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierTenderQuantityTableItemJson] DROP CONSTRAINT [' + @var83 + '];');
ALTER TABLE [Offer].[SupplierTenderQuantityTableItemJson] ALTER COLUMN [SupplierTenderQuantityTableItems] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the Quantity Table Items', 'SCHEMA', N'Offer', 'TABLE', N'SupplierTenderQuantityTableItemJson', 'COLUMN', N'SupplierTenderQuantityTableItems';

GO

DECLARE @var84 sysname;
SELECT @var84 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierTenderQuantityTableItemJson]') AND [c].[name] = N'SupplierTenderQuantityTableId');
IF @var84 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierTenderQuantityTableItemJson] DROP CONSTRAINT [' + @var84 + '];');
ALTER TABLE [Offer].[SupplierTenderQuantityTableItemJson] ALTER COLUMN [SupplierTenderQuantityTableId] bigint NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Quatity Table', 'SCHEMA', N'Offer', 'TABLE', N'SupplierTenderQuantityTableItemJson', 'COLUMN', N'SupplierTenderQuantityTableId';

GO

DECLARE @var85 sysname;
SELECT @var85 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[SupplierTenderQuantityTableItemJson]') AND [c].[name] = N'Id');
IF @var85 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[SupplierTenderQuantityTableItemJson] DROP CONSTRAINT [' + @var85 + '];');
ALTER TABLE [Offer].[SupplierTenderQuantityTableItemJson] ALTER COLUMN [Id] bigint NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define Unique identifer Of Supplier  Quantity Table items', 'SCHEMA', N'Offer', 'TABLE', N'SupplierTenderQuantityTableItemJson', 'COLUMN', N'Id';

GO

DECLARE @var86 sysname;
SELECT @var86 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Offer].[Offer]') AND [c].[name] = N'PartialOfferAwardingValue');
IF @var86 IS NOT NULL EXEC(N'ALTER TABLE [Offer].[Offer] DROP CONSTRAINT [' + @var86 + '];');
ALTER TABLE [Offer].[Offer] ALTER COLUMN [PartialOfferAwardingValue] decimal(18,2) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Offer', 'TABLE', N'Offer', 'COLUMN', N'PartialOfferAwardingValue';
EXEC sp_addextendedproperty 'MS_Description', N'Define the partial value of awarding if the Tender partialy awarded', 'SCHEMA', N'Offer', 'TABLE', N'Offer', 'COLUMN', N'PartialOfferAwardingValue';

GO

DECLARE @var87 sysname;
SELECT @var87 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[UserNotificationSetting]') AND [c].[name] = N'UserRoleId');
IF @var87 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[UserNotificationSetting] DROP CONSTRAINT [' + @var87 + '];');
ALTER TABLE [Notification].[UserNotificationSetting] ALTER COLUMN [UserRoleId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the role of the user who will recieve the Notification', 'SCHEMA', N'Notification', 'TABLE', N'UserNotificationSetting', 'COLUMN', N'UserRoleId';

GO

DECLARE @var88 sysname;
SELECT @var88 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[UserNotificationSetting]') AND [c].[name] = N'UserProfileId');
IF @var88 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[UserNotificationSetting] DROP CONSTRAINT [' + @var88 + '];');
ALTER TABLE [Notification].[UserNotificationSetting] ALTER COLUMN [UserProfileId] int NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related user if he was Governate user ', 'SCHEMA', N'Notification', 'TABLE', N'UserNotificationSetting', 'COLUMN', N'UserProfileId';

GO

DECLARE @var89 sysname;
SELECT @var89 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[UserNotificationSetting]') AND [c].[name] = N'Sms');
IF @var89 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[UserNotificationSetting] DROP CONSTRAINT [' + @var89 + '];');
ALTER TABLE [Notification].[UserNotificationSetting] ALTER COLUMN [Sms] bit NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define if the user needs to recieve SMS or Not', 'SCHEMA', N'Notification', 'TABLE', N'UserNotificationSetting', 'COLUMN', N'Sms';

GO

DECLARE @var90 sysname;
SELECT @var90 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[UserNotificationSetting]') AND [c].[name] = N'OperationCode');
IF @var90 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[UserNotificationSetting] DROP CONSTRAINT [' + @var90 + '];');
ALTER TABLE [Notification].[UserNotificationSetting] ALTER COLUMN [OperationCode] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the Notification Code', 'SCHEMA', N'Notification', 'TABLE', N'UserNotificationSetting', 'COLUMN', N'OperationCode';

GO

DECLARE @var91 sysname;
SELECT @var91 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[UserNotificationSetting]') AND [c].[name] = N'NotificationCodeId');
IF @var91 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[UserNotificationSetting] DROP CONSTRAINT [' + @var91 + '];');
ALTER TABLE [Notification].[UserNotificationSetting] ALTER COLUMN [NotificationCodeId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the Notification Code ID', 'SCHEMA', N'Notification', 'TABLE', N'UserNotificationSetting', 'COLUMN', N'NotificationCodeId';

GO

DECLARE @var92 sysname;
SELECT @var92 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[UserNotificationSetting]') AND [c].[name] = N'IsArabic');
IF @var92 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[UserNotificationSetting] DROP CONSTRAINT [' + @var92 + '];');
ALTER TABLE [Notification].[UserNotificationSetting] ALTER COLUMN [IsArabic] bit NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define if the user configured Notification to be in arabic Language ', 'SCHEMA', N'Notification', 'TABLE', N'UserNotificationSetting', 'COLUMN', N'IsArabic';

GO

DECLARE @var93 sysname;
SELECT @var93 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[UserNotificationSetting]') AND [c].[name] = N'Email');
IF @var93 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[UserNotificationSetting] DROP CONSTRAINT [' + @var93 + '];');
ALTER TABLE [Notification].[UserNotificationSetting] ALTER COLUMN [Email] bit NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define if the user needs to recieve EMAIL or Not', 'SCHEMA', N'Notification', 'TABLE', N'UserNotificationSetting', 'COLUMN', N'Email';

GO

DECLARE @var94 sysname;
SELECT @var94 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[UserNotificationSetting]') AND [c].[name] = N'Cr');
IF @var94 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[UserNotificationSetting] DROP CONSTRAINT [' + @var94 + '];');
ALTER TABLE [Notification].[UserNotificationSetting] ALTER COLUMN [Cr] nvarchar(20) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Refere to the Supplier Commercial Registeration Number who will recieve the Notification', 'SCHEMA', N'Notification', 'TABLE', N'UserNotificationSetting', 'COLUMN', N'Cr';

GO

DECLARE @var95 sysname;
SELECT @var95 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[UserNotificationSetting]') AND [c].[name] = N'Id');
IF @var95 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[UserNotificationSetting] DROP CONSTRAINT [' + @var95 + '];');
ALTER TABLE [Notification].[UserNotificationSetting] ALTER COLUMN [Id] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define Unique identifer Of  User Notification Setting', 'SCHEMA', N'Notification', 'TABLE', N'UserNotificationSetting', 'COLUMN', N'Id';

GO

DECLARE @var96 sysname;
SELECT @var96 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[Notification]') AND [c].[name] = N'sendAt');
IF @var96 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[Notification] DROP CONSTRAINT [' + @var96 + '];');
ALTER TABLE [Notification].[Notification] ALTER COLUMN [sendAt] datetime2 NULL;
EXEC sp_addextendedproperty 'MS_Description', N'the date of sending the notification', 'SCHEMA', N'Notification', 'TABLE', N'Notification', 'COLUMN', N'sendAt';

GO

DECLARE @var97 sysname;
SELECT @var97 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[Notification]') AND [c].[name] = N'UserId');
IF @var97 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[Notification] DROP CONSTRAINT [' + @var97 + '];');
ALTER TABLE [Notification].[Notification] ALTER COLUMN [UserId] int NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the user id who recieve the notification', 'SCHEMA', N'Notification', 'TABLE', N'Notification', 'COLUMN', N'UserId';

GO

DECLARE @var98 sysname;
SELECT @var98 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[Notification]') AND [c].[name] = N'NotificationSettingId');
IF @var98 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[Notification] DROP CONSTRAINT [' + @var98 + '];');
ALTER TABLE [Notification].[Notification] ALTER COLUMN [NotificationSettingId] int NULL;
EXEC sp_addextendedproperty 'MS_Description', N'the related setting of the setting', 'SCHEMA', N'Notification', 'TABLE', N'Notification', 'COLUMN', N'NotificationSettingId';

GO

DECLARE @var99 sysname;
SELECT @var99 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[Notification]') AND [c].[name] = N'NotifacationStatusId');
IF @var99 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[Notification] DROP CONSTRAINT [' + @var99 + '];');
ALTER TABLE [Notification].[Notification] ALTER COLUMN [NotifacationStatusId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the status of Notification if sent or not or Faild', 'SCHEMA', N'Notification', 'TABLE', N'Notification', 'COLUMN', N'NotifacationStatusId';

GO

DECLARE @var100 sysname;
SELECT @var100 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[Notification]') AND [c].[name] = N'Link');
IF @var100 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[Notification] DROP CONSTRAINT [' + @var100 + '];');
ALTER TABLE [Notification].[Notification] ALTER COLUMN [Link] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'link for reciever to access direct the related page of monafasat', 'SCHEMA', N'Notification', 'TABLE', N'Notification', 'COLUMN', N'Link';

GO

DECLARE @var101 sysname;
SELECT @var101 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[Notification]') AND [c].[name] = N'Key');
IF @var101 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[Notification] DROP CONSTRAINT [' + @var101 + '];');
ALTER TABLE [Notification].[Notification] ALTER COLUMN [Key] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'compination of string ''Tender'' and the Id of Tender ', 'SCHEMA', N'Notification', 'TABLE', N'Notification', 'COLUMN', N'Key';

GO

DECLARE @var102 sysname;
SELECT @var102 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[Notification]') AND [c].[name] = N'CR');
IF @var102 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[Notification] DROP CONSTRAINT [' + @var102 + '];');
ALTER TABLE [Notification].[Notification] ALTER COLUMN [CR] nvarchar(20) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the Supplier id who recieve the notification', 'SCHEMA', N'Notification', 'TABLE', N'Notification', 'COLUMN', N'CR';

GO

DECLARE @var103 sysname;
SELECT @var103 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notification].[Notification]') AND [c].[name] = N'Id');
IF @var103 IS NOT NULL EXEC(N'ALTER TABLE [Notification].[Notification] DROP CONSTRAINT [' + @var103 + '];');
ALTER TABLE [Notification].[Notification] ALTER COLUMN [Id] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define Unique identifer Of Notification', 'SCHEMA', N'Notification', 'TABLE', N'Notification', 'COLUMN', N'Id';

GO

DECLARE @var104 sysname;
SELECT @var104 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[MobileAlert]') AND [c].[name] = N'MessageStatusId');
IF @var104 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[MobileAlert] DROP CONSTRAINT [' + @var104 + '];');
ALTER TABLE [Mobile].[MobileAlert] ALTER COLUMN [MessageStatusId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'MobileAlert', 'COLUMN', N'MessageStatusId';

GO

DECLARE @var105 sysname;
SELECT @var105 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[MobileAlert]') AND [c].[name] = N'MessageId');
IF @var105 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[MobileAlert] DROP CONSTRAINT [' + @var105 + '];');
ALTER TABLE [Mobile].[MobileAlert] ALTER COLUMN [MessageId] int NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'MobileAlert', 'COLUMN', N'MessageId';

GO

DECLARE @var106 sysname;
SELECT @var106 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[MobileAlert]') AND [c].[name] = N'Message');
IF @var106 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[MobileAlert] DROP CONSTRAINT [' + @var106 + '];');
ALTER TABLE [Mobile].[MobileAlert] ALTER COLUMN [Message] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'MobileAlert', 'COLUMN', N'Message';

GO

DECLARE @var107 sysname;
SELECT @var107 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[MobileAlert]') AND [c].[name] = N'GroupCode');
IF @var107 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[MobileAlert] DROP CONSTRAINT [' + @var107 + '];');
ALTER TABLE [Mobile].[MobileAlert] ALTER COLUMN [GroupCode] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'MobileAlert', 'COLUMN', N'GroupCode';

GO

DECLARE @var108 sysname;
SELECT @var108 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[MobileAlert]') AND [c].[name] = N'DeviceId');
IF @var108 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[MobileAlert] DROP CONSTRAINT [' + @var108 + '];');
ALTER TABLE [Mobile].[MobileAlert] ALTER COLUMN [DeviceId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'MobileAlert', 'COLUMN', N'DeviceId';

GO

DECLARE @var109 sysname;
SELECT @var109 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[MobileAlert]') AND [c].[name] = N'MobileAlertId');
IF @var109 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[MobileAlert] DROP CONSTRAINT [' + @var109 + '];');
ALTER TABLE [Mobile].[MobileAlert] ALTER COLUMN [MobileAlertId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'MobileAlert', 'COLUMN', N'MobileAlertId';

GO

DECLARE @var110 sysname;
SELECT @var110 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[DeviceTokenNotification]') AND [c].[name] = N'Status');
IF @var110 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[DeviceTokenNotification] DROP CONSTRAINT [' + @var110 + '];');
ALTER TABLE [Mobile].[DeviceTokenNotification] ALTER COLUMN [Status] bit NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceTokenNotification', 'COLUMN', N'Status';

GO

DECLARE @var111 sysname;
SELECT @var111 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[DeviceTokenNotification]') AND [c].[name] = N'DeviceId');
IF @var111 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[DeviceTokenNotification] DROP CONSTRAINT [' + @var111 + '];');
ALTER TABLE [Mobile].[DeviceTokenNotification] ALTER COLUMN [DeviceId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceTokenNotification', 'COLUMN', N'DeviceId';

GO

DECLARE @var112 sysname;
SELECT @var112 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[DeviceTokenNotification]') AND [c].[name] = N'ActivityId');
IF @var112 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[DeviceTokenNotification] DROP CONSTRAINT [' + @var112 + '];');
ALTER TABLE [Mobile].[DeviceTokenNotification] ALTER COLUMN [ActivityId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceTokenNotification', 'COLUMN', N'ActivityId';

GO

DECLARE @var113 sysname;
SELECT @var113 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[DeviceTokenNotification]') AND [c].[name] = N'DeviceTokenNotificationId');
IF @var113 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[DeviceTokenNotification] DROP CONSTRAINT [' + @var113 + '];');
ALTER TABLE [Mobile].[DeviceTokenNotification] ALTER COLUMN [DeviceTokenNotificationId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceTokenNotification', 'COLUMN', N'DeviceTokenNotificationId';

GO

DECLARE @var114 sysname;
SELECT @var114 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[DeviceToken]') AND [c].[name] = N'UserProfileId');
IF @var114 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[DeviceToken] DROP CONSTRAINT [' + @var114 + '];');
ALTER TABLE [Mobile].[DeviceToken] ALTER COLUMN [UserProfileId] int NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceToken', 'COLUMN', N'UserProfileId';

GO

DECLARE @var115 sysname;
SELECT @var115 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[DeviceToken]') AND [c].[name] = N'UserDeviceId');
IF @var115 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[DeviceToken] DROP CONSTRAINT [' + @var115 + '];');
ALTER TABLE [Mobile].[DeviceToken] ALTER COLUMN [UserDeviceId] nvarchar(60) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceToken', 'COLUMN', N'UserDeviceId';

GO

DECLARE @var116 sysname;
SELECT @var116 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[DeviceToken]') AND [c].[name] = N'TimeStamp');
IF @var116 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[DeviceToken] DROP CONSTRAINT [' + @var116 + '];');
ALTER TABLE [Mobile].[DeviceToken] ALTER COLUMN [TimeStamp] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceToken', 'COLUMN', N'TimeStamp';

GO

DECLARE @var117 sysname;
SELECT @var117 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[DeviceToken]') AND [c].[name] = N'SourceIP');
IF @var117 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[DeviceToken] DROP CONSTRAINT [' + @var117 + '];');
ALTER TABLE [Mobile].[DeviceToken] ALTER COLUMN [SourceIP] nvarchar(20) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceToken', 'COLUMN', N'SourceIP';

GO

DECLARE @var118 sysname;
SELECT @var118 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[DeviceToken]') AND [c].[name] = N'Model');
IF @var118 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[DeviceToken] DROP CONSTRAINT [' + @var118 + '];');
ALTER TABLE [Mobile].[DeviceToken] ALTER COLUMN [Model] nvarchar(30) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceToken', 'COLUMN', N'Model';

GO

DECLARE @var119 sysname;
SELECT @var119 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[DeviceToken]') AND [c].[name] = N'DeviceVersion');
IF @var119 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[DeviceToken] DROP CONSTRAINT [' + @var119 + '];');
ALTER TABLE [Mobile].[DeviceToken] ALTER COLUMN [DeviceVersion] nvarchar(15) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceToken', 'COLUMN', N'DeviceVersion';

GO

DECLARE @var120 sysname;
SELECT @var120 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[DeviceToken]') AND [c].[name] = N'DeviceTokenValue');
IF @var120 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[DeviceToken] DROP CONSTRAINT [' + @var120 + '];');
ALTER TABLE [Mobile].[DeviceToken] ALTER COLUMN [DeviceTokenValue] nvarchar(500) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceToken', 'COLUMN', N'DeviceTokenValue';

GO

DECLARE @var121 sysname;
SELECT @var121 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[DeviceToken]') AND [c].[name] = N'DeviceName');
IF @var121 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[DeviceToken] DROP CONSTRAINT [' + @var121 + '];');
ALTER TABLE [Mobile].[DeviceToken] ALTER COLUMN [DeviceName] nvarchar(100) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceToken', 'COLUMN', N'DeviceName';

GO

DECLARE @var122 sysname;
SELECT @var122 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mobile].[DeviceToken]') AND [c].[name] = N'DeviceId');
IF @var122 IS NOT NULL EXEC(N'ALTER TABLE [Mobile].[DeviceToken] DROP CONSTRAINT [' + @var122 + '];');
ALTER TABLE [Mobile].[DeviceToken] ALTER COLUMN [DeviceId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Not Used', 'SCHEMA', N'Mobile', 'TABLE', N'DeviceToken', 'COLUMN', N'DeviceId';

GO

DECLARE @var123 sysname;
SELECT @var123 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[VendorCertificate]') AND [c].[name] = N'NameEn');
IF @var123 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[VendorCertificate] DROP CONSTRAINT [' + @var123 + '];');
ALTER TABLE [LookUps].[VendorCertificate] ALTER COLUMN [NameEn] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define english name of vendor certificate', 'SCHEMA', N'LookUps', 'TABLE', N'VendorCertificate', 'COLUMN', N'NameEn';

GO

DECLARE @var124 sysname;
SELECT @var124 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[VendorCertificate]') AND [c].[name] = N'NameAr');
IF @var124 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[VendorCertificate] DROP CONSTRAINT [' + @var124 + '];');
ALTER TABLE [LookUps].[VendorCertificate] ALTER COLUMN [NameAr] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define arabic name of vendor certificate', 'SCHEMA', N'LookUps', 'TABLE', N'VendorCertificate', 'COLUMN', N'NameAr';

GO

DECLARE @var125 sysname;
SELECT @var125 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[VendorCertificate]') AND [c].[name] = N'VendorCertificateId');
IF @var125 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[VendorCertificate] DROP CONSTRAINT [' + @var125 + '];');
ALTER TABLE [LookUps].[VendorCertificate] ALTER COLUMN [VendorCertificateId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define identity of vendor certificate', 'SCHEMA', N'LookUps', 'TABLE', N'VendorCertificate', 'COLUMN', N'VendorCertificateId';

GO

DECLARE @var126 sysname;
SELECT @var126 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[UserRole]') AND [c].[name] = N'Name');
IF @var126 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[UserRole] DROP CONSTRAINT [' + @var126 + '];');
ALTER TABLE [LookUps].[UserRole] ALTER COLUMN [Name] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define name of role', 'SCHEMA', N'LookUps', 'TABLE', N'UserRole', 'COLUMN', N'Name';

GO

DECLARE @var127 sysname;
SELECT @var127 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[UserRole]') AND [c].[name] = N'IsCombinedRole');
IF @var127 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[UserRole] DROP CONSTRAINT [' + @var127 + '];');
ALTER TABLE [LookUps].[UserRole] ALTER COLUMN [IsCombinedRole] bit NOT NULL;
ALTER TABLE [LookUps].[UserRole] ADD DEFAULT CAST(0 AS bit) FOR [IsCombinedRole];
EXEC sp_addextendedproperty 'MS_Description', N'describe notification for low budget module', 'SCHEMA', N'LookUps', 'TABLE', N'UserRole', 'COLUMN', N'IsCombinedRole';

GO

DECLARE @var128 sysname;
SELECT @var128 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[UserRole]') AND [c].[name] = N'DisplayNameEn');
IF @var128 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[UserRole] DROP CONSTRAINT [' + @var128 + '];');
ALTER TABLE [LookUps].[UserRole] ALTER COLUMN [DisplayNameEn] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define english name of role', 'SCHEMA', N'LookUps', 'TABLE', N'UserRole', 'COLUMN', N'DisplayNameEn';

GO

DECLARE @var129 sysname;
SELECT @var129 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[UserRole]') AND [c].[name] = N'DisplayNameAr');
IF @var129 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[UserRole] DROP CONSTRAINT [' + @var129 + '];');
ALTER TABLE [LookUps].[UserRole] ALTER COLUMN [DisplayNameAr] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define arabic name of role', 'SCHEMA', N'LookUps', 'TABLE', N'UserRole', 'COLUMN', N'DisplayNameAr';

GO

DECLARE @var130 sysname;
SELECT @var130 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[UserRole]') AND [c].[name] = N'UserRoleId');
IF @var130 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[UserRole] DROP CONSTRAINT [' + @var130 + '];');
ALTER TABLE [LookUps].[UserRole] ALTER COLUMN [UserRoleId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define identity of user role', 'SCHEMA', N'LookUps', 'TABLE', N'UserRole', 'COLUMN', N'UserRoleId';

GO

DECLARE @var131 sysname;
SELECT @var131 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[TenderUnitUpdateType]') AND [c].[name] = N'Name');
IF @var131 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[TenderUnitUpdateType] DROP CONSTRAINT [' + @var131 + '];');
ALTER TABLE [LookUps].[TenderUnitUpdateType] ALTER COLUMN [Name] nvarchar(1024) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'LookUps', 'TABLE', N'TenderUnitUpdateType', 'COLUMN', N'Name';
EXEC sp_addextendedproperty 'MS_Description', N'Define the name of Tender unit type', 'SCHEMA', N'LookUps', 'TABLE', N'TenderUnitUpdateType', 'COLUMN', N'Name';

GO

DECLARE @var132 sysname;
SELECT @var132 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[TenderUnitUpdateType]') AND [c].[name] = N'TenderUnitUpdateTypeId');
IF @var132 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[TenderUnitUpdateType] DROP CONSTRAINT [' + @var132 + '];');
ALTER TABLE [LookUps].[TenderUnitUpdateType] ALTER COLUMN [TenderUnitUpdateTypeId] int NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'LookUps', 'TABLE', N'TenderUnitUpdateType', 'COLUMN', N'TenderUnitUpdateTypeId';
EXEC sp_addextendedproperty 'MS_Description', N'Define identity of Tender unit type', 'SCHEMA', N'LookUps', 'TABLE', N'TenderUnitUpdateType', 'COLUMN', N'TenderUnitUpdateTypeId';

GO

DECLARE @var133 sysname;
SELECT @var133 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[TenderUnitStatus]') AND [c].[name] = N'Name');
IF @var133 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[TenderUnitStatus] DROP CONSTRAINT [' + @var133 + '];');
ALTER TABLE [LookUps].[TenderUnitStatus] ALTER COLUMN [Name] nvarchar(100) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'LookUps', 'TABLE', N'TenderUnitStatus', 'COLUMN', N'Name';
EXEC sp_addextendedproperty 'MS_Description', N'Define the name of Tender unit status', 'SCHEMA', N'LookUps', 'TABLE', N'TenderUnitStatus', 'COLUMN', N'Name';

GO

DECLARE @var134 sysname;
SELECT @var134 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[TenderUnitStatus]') AND [c].[name] = N'TenderUnitStatusId');
IF @var134 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[TenderUnitStatus] DROP CONSTRAINT [' + @var134 + '];');
ALTER TABLE [LookUps].[TenderUnitStatus] ALTER COLUMN [TenderUnitStatusId] int NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'LookUps', 'TABLE', N'TenderUnitStatus', 'COLUMN', N'TenderUnitStatusId';
EXEC sp_addextendedproperty 'MS_Description', N'Define identity of Tender unit status', 'SCHEMA', N'LookUps', 'TABLE', N'TenderUnitStatus', 'COLUMN', N'TenderUnitStatusId';

GO

DECLARE @var135 sysname;
SELECT @var135 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[TenderType]') AND [c].[name] = N'NameEn');
IF @var135 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[TenderType] DROP CONSTRAINT [' + @var135 + '];');
ALTER TABLE [LookUps].[TenderType] ALTER COLUMN [NameEn] nvarchar(500) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'LookUps', 'TABLE', N'TenderType', 'COLUMN', N'NameEn';
EXEC sp_addextendedproperty 'MS_Description', N'Define the english name of Tender type', 'SCHEMA', N'LookUps', 'TABLE', N'TenderType', 'COLUMN', N'NameEn';

GO

DECLARE @var136 sysname;
SELECT @var136 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[TenderType]') AND [c].[name] = N'NameAr');
IF @var136 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[TenderType] DROP CONSTRAINT [' + @var136 + '];');
ALTER TABLE [LookUps].[TenderType] ALTER COLUMN [NameAr] nvarchar(500) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'LookUps', 'TABLE', N'TenderType', 'COLUMN', N'NameAr';
EXEC sp_addextendedproperty 'MS_Description', N'Define the arabic name of Tender type', 'SCHEMA', N'LookUps', 'TABLE', N'TenderType', 'COLUMN', N'NameAr';

GO

DECLARE @var137 sysname;
SELECT @var137 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[TenderType]') AND [c].[name] = N'InvitationCost');
IF @var137 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[TenderType] DROP CONSTRAINT [' + @var137 + '];');
ALTER TABLE [LookUps].[TenderType] ALTER COLUMN [InvitationCost] decimal(18,2) NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'LookUps', 'TABLE', N'TenderType', 'COLUMN', N'InvitationCost';
EXEC sp_addextendedproperty 'MS_Description', N'Define the Invitation Cost of Tender type', 'SCHEMA', N'LookUps', 'TABLE', N'TenderType', 'COLUMN', N'InvitationCost';

GO

DECLARE @var138 sysname;
SELECT @var138 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[TenderType]') AND [c].[name] = N'BuyingCost');
IF @var138 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[TenderType] DROP CONSTRAINT [' + @var138 + '];');
ALTER TABLE [LookUps].[TenderType] ALTER COLUMN [BuyingCost] decimal(18,2) NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'LookUps', 'TABLE', N'TenderType', 'COLUMN', N'BuyingCost';
EXEC sp_addextendedproperty 'MS_Description', N'Define the cost of buying  of Tender type', 'SCHEMA', N'LookUps', 'TABLE', N'TenderType', 'COLUMN', N'BuyingCost';

GO

DECLARE @var139 sysname;
SELECT @var139 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[TenderType]') AND [c].[name] = N'TenderTypeId');
IF @var139 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[TenderType] DROP CONSTRAINT [' + @var139 + '];');
ALTER TABLE [LookUps].[TenderType] ALTER COLUMN [TenderTypeId] int NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'LookUps', 'TABLE', N'TenderType', 'COLUMN', N'TenderTypeId';
EXEC sp_addextendedproperty 'MS_Description', N'Define identity of Tender type', 'SCHEMA', N'LookUps', 'TABLE', N'TenderType', 'COLUMN', N'TenderTypeId';

GO

DECLARE @var140 sysname;
SELECT @var140 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[TenderStatus]') AND [c].[name] = N'NameEn');
IF @var140 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[TenderStatus] DROP CONSTRAINT [' + @var140 + '];');
ALTER TABLE [LookUps].[TenderStatus] ALTER COLUMN [NameEn] nvarchar(100) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'LookUps', 'TABLE', N'TenderStatus', 'COLUMN', N'NameEn';
EXEC sp_addextendedproperty 'MS_Description', N'Define the english name of Tender status', 'SCHEMA', N'LookUps', 'TABLE', N'TenderStatus', 'COLUMN', N'NameEn';

GO

DECLARE @var141 sysname;
SELECT @var141 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[TenderStatus]') AND [c].[name] = N'NameAr');
IF @var141 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[TenderStatus] DROP CONSTRAINT [' + @var141 + '];');
ALTER TABLE [LookUps].[TenderStatus] ALTER COLUMN [NameAr] nvarchar(100) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'LookUps', 'TABLE', N'TenderStatus', 'COLUMN', N'NameAr';
EXEC sp_addextendedproperty 'MS_Description', N'Define the arabic name of Tender status', 'SCHEMA', N'LookUps', 'TABLE', N'TenderStatus', 'COLUMN', N'NameAr';

GO

DECLARE @var142 sysname;
SELECT @var142 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[TenderStatus]') AND [c].[name] = N'TenderStatusId');
IF @var142 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[TenderStatus] DROP CONSTRAINT [' + @var142 + '];');
ALTER TABLE [LookUps].[TenderStatus] ALTER COLUMN [TenderStatusId] int NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'LookUps', 'TABLE', N'TenderStatus', 'COLUMN', N'TenderStatusId';
EXEC sp_addextendedproperty 'MS_Description', N'Define identity of Tender status', 'SCHEMA', N'LookUps', 'TABLE', N'TenderStatus', 'COLUMN', N'TenderStatusId';

GO

DECLARE @var143 sysname;
SELECT @var143 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[SupplierSecondNegotiationStatus]') AND [c].[name] = N'Name');
IF @var143 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[SupplierSecondNegotiationStatus] DROP CONSTRAINT [' + @var143 + '];');
ALTER TABLE [LookUps].[SupplierSecondNegotiationStatus] ALTER COLUMN [Name] nvarchar(100) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the  Name Of Second Stage Negotiation Status', 'SCHEMA', N'LookUps', 'TABLE', N'SupplierSecondNegotiationStatus', 'COLUMN', N'Name';

GO

DECLARE @var144 sysname;
SELECT @var144 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[SupplierSecondNegotiationStatus]') AND [c].[name] = N'SupplierNegotiaitionStatusId');
IF @var144 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[SupplierSecondNegotiationStatus] DROP CONSTRAINT [' + @var144 + '];');
ALTER TABLE [LookUps].[SupplierSecondNegotiationStatus] ALTER COLUMN [SupplierNegotiaitionStatusId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define Unique identifer Of  Second Stage Negotiation Status', 'SCHEMA', N'LookUps', 'TABLE', N'SupplierSecondNegotiationStatus', 'COLUMN', N'SupplierNegotiaitionStatusId';

GO

DECLARE @var145 sysname;
SELECT @var145 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'UserRoleId');
IF @var145 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var145 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [UserRoleId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related user role  that will recieve the notification', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'UserRoleId';

GO

DECLARE @var146 sysname;
SELECT @var146 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'SmsTemplateEn');
IF @var146 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var146 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [SmsTemplateEn] nvarchar(2000) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'The SMS English Template', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'SmsTemplateEn';

GO

DECLARE @var147 sysname;
SELECT @var147 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'SmsTemplateAr');
IF @var147 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var147 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [SmsTemplateAr] nvarchar(2000) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'The SMS arabic Template', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'SmsTemplateAr';

GO

DECLARE @var148 sysname;
SELECT @var148 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'PanelTemplateEn');
IF @var148 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var148 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [PanelTemplateEn] nvarchar(1000) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'The English Panel Subject for noification ', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'PanelTemplateEn';

GO

DECLARE @var149 sysname;
SELECT @var149 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'PanelTemplateAr');
IF @var149 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var149 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [PanelTemplateAr] nvarchar(1000) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'The Arabic Panel Subject for noification ', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'PanelTemplateAr';

GO

DECLARE @var150 sysname;
SELECT @var150 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'OperationCode');
IF @var150 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var150 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [OperationCode] nvarchar(100) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'its  unique Text the represent the notification template', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'OperationCode';

GO

DECLARE @var151 sysname;
SELECT @var151 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'NotificationCategoryId');
IF @var151 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var151 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [NotificationCategoryId] int NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define if the Category of notificatopn item like [operations on Tender , negotiation ETC..]', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'NotificationCategoryId';

GO

DECLARE @var152 sysname;
SELECT @var152 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'EnglishName');
IF @var152 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var152 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [EnglishName] nvarchar(2000) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define notification template english Name', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'EnglishName';

GO

DECLARE @var153 sysname;
SELECT @var153 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'EmailSubjectTemplateEn');
IF @var153 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var153 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [EmailSubjectTemplateEn] nvarchar(2000) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'The English EMail Body for noification ', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'EmailSubjectTemplateEn';

GO

DECLARE @var154 sysname;
SELECT @var154 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'EmailSubjectTemplateAr');
IF @var154 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var154 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [EmailSubjectTemplateAr] nvarchar(2000) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'The Arabic EMail Subject for noification ', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'EmailSubjectTemplateAr';

GO

DECLARE @var155 sysname;
SELECT @var155 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'EmailBodyTemplateAr');
IF @var155 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var155 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [EmailBodyTemplateAr] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'The Arabic EMail Subject for noification ', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'EmailBodyTemplateAr';

GO

DECLARE @var156 sysname;
SELECT @var156 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'DefaultSMS');
IF @var156 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var156 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [DefaultSMS] bit NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define if the reciever role will recieve SMS by default  or not', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'DefaultSMS';

GO

DECLARE @var157 sysname;
SELECT @var157 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'DefaultEmail');
IF @var157 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var157 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [DefaultEmail] bit NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define if the reciever role will recieve Email by default  or not', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'DefaultEmail';

GO

DECLARE @var158 sysname;
SELECT @var158 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'CanEditSMS');
IF @var158 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var158 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [CanEditSMS] bit NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define if the reciever role can change Default setting for recieving SMS or not', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'CanEditSMS';

GO

DECLARE @var159 sysname;
SELECT @var159 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'CanEditEmail');
IF @var159 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var159 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [CanEditEmail] bit NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define if the reciever role can change Default setting for recieving Email or not', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'CanEditEmail';

GO

DECLARE @var160 sysname;
SELECT @var160 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'ArabicName');
IF @var160 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var160 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [ArabicName] nvarchar(2000) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define notification template arabic Name', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'ArabicName';

GO

DECLARE @var161 sysname;
SELECT @var161 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationOperationCode]') AND [c].[name] = N'NotificationOperationCodeId');
IF @var161 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationOperationCode] DROP CONSTRAINT [' + @var161 + '];');
ALTER TABLE [LookUps].[NotificationOperationCode] ALTER COLUMN [NotificationOperationCodeId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define Unique identifer Of Notification Operation Code', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationOperationCode', 'COLUMN', N'NotificationOperationCodeId';

GO

DECLARE @var162 sysname;
SELECT @var162 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationCategory]') AND [c].[name] = N'EnglishName');
IF @var162 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationCategory] DROP CONSTRAINT [' + @var162 + '];');
ALTER TABLE [LookUps].[NotificationCategory] ALTER COLUMN [EnglishName] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the English Name Of Notification Category', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationCategory', 'COLUMN', N'EnglishName';

GO

DECLARE @var163 sysname;
SELECT @var163 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationCategory]') AND [c].[name] = N'ArabicName');
IF @var163 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationCategory] DROP CONSTRAINT [' + @var163 + '];');
ALTER TABLE [LookUps].[NotificationCategory] ALTER COLUMN [ArabicName] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the arabic Name Of Notification Category', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationCategory', 'COLUMN', N'ArabicName';

GO

DECLARE @var164 sysname;
SELECT @var164 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotificationCategory]') AND [c].[name] = N'Id');
IF @var164 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotificationCategory] DROP CONSTRAINT [' + @var164 + '];');
ALTER TABLE [LookUps].[NotificationCategory] ALTER COLUMN [Id] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define Unique identifer Of  Notification Category', 'SCHEMA', N'LookUps', 'TABLE', N'NotificationCategory', 'COLUMN', N'Id';

GO

DECLARE @var165 sysname;
SELECT @var165 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[NotifacationStatusEntity]') AND [c].[name] = N'NotifacationStatusEntityId');
IF @var165 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[NotifacationStatusEntity] DROP CONSTRAINT [' + @var165 + '];');
ALTER TABLE [LookUps].[NotifacationStatusEntity] ALTER COLUMN [NotifacationStatusEntityId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define Unique identifer Of Notification Status lookup [مرسل,لم يتم الارسال , فشل فى الارسال , مقروءه , غير مقروءه]', 'SCHEMA', N'LookUps', 'TABLE', N'NotifacationStatusEntity', 'COLUMN', N'NotifacationStatusEntityId';

GO

DECLARE @var166 sysname;
SELECT @var166 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[AgreementType]') AND [c].[name] = N'NameEn');
IF @var166 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[AgreementType] DROP CONSTRAINT [' + @var166 + '];');
ALTER TABLE [LookUps].[AgreementType] ALTER COLUMN [NameEn] nvarchar(500) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the arabic name of agreement type', 'SCHEMA', N'LookUps', 'TABLE', N'AgreementType', 'COLUMN', N'NameEn';

GO

DECLARE @var167 sysname;
SELECT @var167 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[AgreementType]') AND [c].[name] = N'NameAr');
IF @var167 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[AgreementType] DROP CONSTRAINT [' + @var167 + '];');
ALTER TABLE [LookUps].[AgreementType] ALTER COLUMN [NameAr] nvarchar(500) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the arabic name of agreement type', 'SCHEMA', N'LookUps', 'TABLE', N'AgreementType', 'COLUMN', N'NameAr';

GO

DECLARE @var168 sysname;
SELECT @var168 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LookUps].[AgreementType]') AND [c].[name] = N'AgreementTypeId');
IF @var168 IS NOT NULL EXEC(N'ALTER TABLE [LookUps].[AgreementType] DROP CONSTRAINT [' + @var168 + '];');
ALTER TABLE [LookUps].[AgreementType] ALTER COLUMN [AgreementTypeId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the identity of agreement type', 'SCHEMA', N'LookUps', 'TABLE', N'AgreementType', 'COLUMN', N'AgreementTypeId';

GO

ALTER TABLE [LookUps].[Activity] ADD [RelatedActivityId] int NULL;

GO

DECLARE @var169 sysname;
SELECT @var169 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lookups].[YearQuarter]') AND [c].[name] = N'NameEn');
IF @var169 IS NOT NULL EXEC(N'ALTER TABLE [Lookups].[YearQuarter] DROP CONSTRAINT [' + @var169 + '];');
ALTER TABLE [Lookups].[YearQuarter] ALTER COLUMN [NameEn] nvarchar(100) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define english name of year quarter', 'SCHEMA', N'Lookups', 'TABLE', N'YearQuarter', 'COLUMN', N'NameEn';

GO

DECLARE @var170 sysname;
SELECT @var170 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lookups].[YearQuarter]') AND [c].[name] = N'NameAr');
IF @var170 IS NOT NULL EXEC(N'ALTER TABLE [Lookups].[YearQuarter] DROP CONSTRAINT [' + @var170 + '];');
ALTER TABLE [Lookups].[YearQuarter] ALTER COLUMN [NameAr] nvarchar(100) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define arabic name of year quarter', 'SCHEMA', N'Lookups', 'TABLE', N'YearQuarter', 'COLUMN', N'NameAr';

GO

DECLARE @var171 sysname;
SELECT @var171 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lookups].[YearQuarter]') AND [c].[name] = N'YearQuarterId');
IF @var171 IS NOT NULL EXEC(N'ALTER TABLE [Lookups].[YearQuarter] DROP CONSTRAINT [' + @var171 + '];');
ALTER TABLE [Lookups].[YearQuarter] ALTER COLUMN [YearQuarterId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define identity of year quarter', 'SCHEMA', N'Lookups', 'TABLE', N'YearQuarter', 'COLUMN', N'YearQuarterId';

GO

DECLARE @var172 sysname;
SELECT @var172 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lookups].[TenderFeesType]') AND [c].[name] = N'NameEnglish');
IF @var172 IS NOT NULL EXEC(N'ALTER TABLE [Lookups].[TenderFeesType] DROP CONSTRAINT [' + @var172 + '];');
ALTER TABLE [Lookups].[TenderFeesType] ALTER COLUMN [NameEnglish] nvarchar(100) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Lookups', 'TABLE', N'TenderFeesType', 'COLUMN', N'NameEnglish';
EXEC sp_addextendedproperty 'MS_Description', N'Define english name of Tender fees type', 'SCHEMA', N'Lookups', 'TABLE', N'TenderFeesType', 'COLUMN', N'NameEnglish';

GO

DECLARE @var173 sysname;
SELECT @var173 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lookups].[TenderFeesType]') AND [c].[name] = N'NameArabic');
IF @var173 IS NOT NULL EXEC(N'ALTER TABLE [Lookups].[TenderFeesType] DROP CONSTRAINT [' + @var173 + '];');
ALTER TABLE [Lookups].[TenderFeesType] ALTER COLUMN [NameArabic] nvarchar(100) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Lookups', 'TABLE', N'TenderFeesType', 'COLUMN', N'NameArabic';
EXEC sp_addextendedproperty 'MS_Description', N'Define arabic name of Tender fees type', 'SCHEMA', N'Lookups', 'TABLE', N'TenderFeesType', 'COLUMN', N'NameArabic';

GO

DECLARE @var174 sysname;
SELECT @var174 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lookups].[TenderFeesType]') AND [c].[name] = N'TenderFeesTypeId');
IF @var174 IS NOT NULL EXEC(N'ALTER TABLE [Lookups].[TenderFeesType] DROP CONSTRAINT [' + @var174 + '];');
ALTER TABLE [Lookups].[TenderFeesType] ALTER COLUMN [TenderFeesTypeId] int NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Lookups', 'TABLE', N'TenderFeesType', 'COLUMN', N'TenderFeesTypeId';
EXEC sp_addextendedproperty 'MS_Description', N'Define identity of Tender fees type', 'SCHEMA', N'Lookups', 'TABLE', N'TenderFeesType', 'COLUMN', N'TenderFeesTypeId';

GO

DECLARE @var175 sysname;
SELECT @var175 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lookups].[OfferPresentationWay]') AND [c].[name] = N'Name');
IF @var175 IS NOT NULL EXEC(N'ALTER TABLE [Lookups].[OfferPresentationWay] DROP CONSTRAINT [' + @var175 + '];');
ALTER TABLE [Lookups].[OfferPresentationWay] ALTER COLUMN [Name] nvarchar(1024) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'Lookups', 'TABLE', N'OfferPresentationWay', 'COLUMN', N'Name';
EXEC sp_addextendedproperty 'MS_Description', N'Define the Name Of Notification Status', 'SCHEMA', N'Lookups', 'TABLE', N'OfferPresentationWay', 'COLUMN', N'Name';

GO

DECLARE @var176 sysname;
SELECT @var176 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lookups].[City]') AND [c].[name] = N'NameEnglish');
IF @var176 IS NOT NULL EXEC(N'ALTER TABLE [Lookups].[City] DROP CONSTRAINT [' + @var176 + '];');
ALTER TABLE [Lookups].[City] ALTER COLUMN [NameEnglish] nvarchar(100) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define English name of City', 'SCHEMA', N'Lookups', 'TABLE', N'City', 'COLUMN', N'NameEnglish';

GO

DECLARE @var177 sysname;
SELECT @var177 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lookups].[City]') AND [c].[name] = N'NameArabic');
IF @var177 IS NOT NULL EXEC(N'ALTER TABLE [Lookups].[City] DROP CONSTRAINT [' + @var177 + '];');
ALTER TABLE [Lookups].[City] ALTER COLUMN [NameArabic] nvarchar(100) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define arabic name of City', 'SCHEMA', N'Lookups', 'TABLE', N'City', 'COLUMN', N'NameArabic';

GO

DECLARE @var178 sysname;
SELECT @var178 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lookups].[City]') AND [c].[name] = N'CityID');
IF @var178 IS NOT NULL EXEC(N'ALTER TABLE [Lookups].[City] DROP CONSTRAINT [' + @var178 + '];');
ALTER TABLE [Lookups].[City] ALTER COLUMN [CityID] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define a unique identifer of City', 'SCHEMA', N'Lookups', 'TABLE', N'City', 'COLUMN', N'CityID';

GO

DECLARE @var179 sysname;
SELECT @var179 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[UnRegisteredSuppliersInvitation]') AND [c].[name] = N'TenderId');
IF @var179 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] DROP CONSTRAINT [' + @var179 + '];');
ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] ALTER COLUMN [TenderId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Tender for the invitations', 'SCHEMA', N'Invitation', 'TABLE', N'UnRegisteredSuppliersInvitation', 'COLUMN', N'TenderId';

GO

DECLARE @var180 sysname;
SELECT @var180 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[UnRegisteredSuppliersInvitation]') AND [c].[name] = N'MobileNo');
IF @var180 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] DROP CONSTRAINT [' + @var180 + '];');
ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] ALTER COLUMN [MobileNo] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the supplier Mobile Number', 'SCHEMA', N'Invitation', 'TABLE', N'UnRegisteredSuppliersInvitation', 'COLUMN', N'MobileNo';

GO

DECLARE @var181 sysname;
SELECT @var181 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[UnRegisteredSuppliersInvitation]') AND [c].[name] = N'InvitationTypeId');
IF @var181 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] DROP CONSTRAINT [' + @var181 + '];');
ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] ALTER COLUMN [InvitationTypeId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the invitaion Type if it was by email or mobile ETC...', 'SCHEMA', N'Invitation', 'TABLE', N'UnRegisteredSuppliersInvitation', 'COLUMN', N'InvitationTypeId';

GO

DECLARE @var182 sysname;
SELECT @var182 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[UnRegisteredSuppliersInvitation]') AND [c].[name] = N'InvitationStatusId');
IF @var182 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] DROP CONSTRAINT [' + @var182 + '];');
ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] ALTER COLUMN [InvitationStatusId] int NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the status of invitation id sent aor not and if accepted by supplier or Not', 'SCHEMA', N'Invitation', 'TABLE', N'UnRegisteredSuppliersInvitation', 'COLUMN', N'InvitationStatusId';

GO

DECLARE @var183 sysname;
SELECT @var183 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[UnRegisteredSuppliersInvitation]') AND [c].[name] = N'Email');
IF @var183 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] DROP CONSTRAINT [' + @var183 + '];');
ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] ALTER COLUMN [Email] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the supplier email that he will recieve the invitaion on it ', 'SCHEMA', N'Invitation', 'TABLE', N'UnRegisteredSuppliersInvitation', 'COLUMN', N'Email';

GO

DECLARE @var184 sysname;
SELECT @var184 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[UnRegisteredSuppliersInvitation]') AND [c].[name] = N'Description');
IF @var184 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] DROP CONSTRAINT [' + @var184 + '];');
ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] ALTER COLUMN [Description] nvarchar(2056) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the description written with the invitaion  ', 'SCHEMA', N'Invitation', 'TABLE', N'UnRegisteredSuppliersInvitation', 'COLUMN', N'Description';

GO

DECLARE @var185 sysname;
SELECT @var185 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[UnRegisteredSuppliersInvitation]') AND [c].[name] = N'CrNumber');
IF @var185 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] DROP CONSTRAINT [' + @var185 + '];');
ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] ALTER COLUMN [CrNumber] nvarchar(50) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the supplier Commercial Registeration Number', 'SCHEMA', N'Invitation', 'TABLE', N'UnRegisteredSuppliersInvitation', 'COLUMN', N'CrNumber';

GO

DECLARE @var186 sysname;
SELECT @var186 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[UnRegisteredSuppliersInvitation]') AND [c].[name] = N'Id');
IF @var186 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] DROP CONSTRAINT [' + @var186 + '];');
ALTER TABLE [Invitation].[UnRegisteredSuppliersInvitation] ALTER COLUMN [Id] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define Unique identifer Of UnRegistered Suppliers Invitation', 'SCHEMA', N'Invitation', 'TABLE', N'UnRegisteredSuppliersInvitation', 'COLUMN', N'Id';

GO

DECLARE @var187 sysname;
SELECT @var187 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'WithdrawalDate');
IF @var187 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var187 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [WithdrawalDate] datetime2 NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define withdrawal date of invitation', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'WithdrawalDate';

GO

DECLARE @var188 sysname;
SELECT @var188 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'UpdatedBy');
IF @var188 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var188 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [UpdatedBy] nvarchar(256) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Determine who updated invitation', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'UpdatedBy';

GO

DECLARE @var189 sysname;
SELECT @var189 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'UpdatedAt');
IF @var189 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var189 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [UpdatedAt] datetime2 NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define updated date of invitation', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'UpdatedAt';

GO

DECLARE @var190 sysname;
SELECT @var190 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'TenderId');
IF @var190 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var190 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [TenderId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'it''s a foreign key described Tender that related to  invitation', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'TenderId';

GO

DECLARE @var191 sysname;
SELECT @var191 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'SupplierType');
IF @var191 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var191 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [SupplierType] int NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define supplier type  of invitation', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'SupplierType';

GO

DECLARE @var192 sysname;
SELECT @var192 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'SubmissionDate');
IF @var192 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var192 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [SubmissionDate] datetime2 NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define submission date of invitation', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'SubmissionDate';

GO

DECLARE @var193 sysname;
SELECT @var193 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'StatusId');
IF @var193 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var193 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [StatusId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'it''s a foreign  key that described status of invitation', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'StatusId';

GO

DECLARE @var194 sysname;
SELECT @var194 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'SendingDate');
IF @var194 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var194 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [SendingDate] datetime2 NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define sending date of invitation', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'SendingDate';

GO

DECLARE @var195 sysname;
SELECT @var195 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'RejectionReason');
IF @var195 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var195 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [RejectionReason] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define reject reason of invitation', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'RejectionReason';

GO

DECLARE @var196 sysname;
SELECT @var196 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'IsActive');
IF @var196 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var196 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [IsActive] bit NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define invitation is active or not', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'IsActive';

GO

DECLARE @var197 sysname;
SELECT @var197 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'InvitedByQualification');
IF @var197 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var197 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [InvitedByQualification] bit NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define supplier invited by qualification or not', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'InvitedByQualification';

GO

DECLARE @var198 sysname;
SELECT @var198 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'InvitationTypeId');
IF @var198 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var198 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [InvitationTypeId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'it''s a foreign  key that described type of invitation', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'InvitationTypeId';

GO

DECLARE @var199 sysname;
SELECT @var199 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'CreatedBy');
IF @var199 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var199 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [CreatedBy] nvarchar(256) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Determine who cretead invitation', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'CreatedBy';

GO

DECLARE @var200 sysname;
SELECT @var200 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'CreatedAt');
IF @var200 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var200 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [CreatedAt] datetime2 NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define created date of invitation', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'CreatedAt';

GO

DECLARE @var201 sysname;
SELECT @var201 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'CommericalRegisterNo');
IF @var201 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var201 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [CommericalRegisterNo] nvarchar(20) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define supplier commerical register number', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'CommericalRegisterNo';

GO

DECLARE @var202 sysname;
SELECT @var202 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invitation].[Invitation]') AND [c].[name] = N'InvitationId');
IF @var202 IS NOT NULL EXEC(N'ALTER TABLE [Invitation].[Invitation] DROP CONSTRAINT [' + @var202 + '];');
ALTER TABLE [Invitation].[Invitation] ALTER COLUMN [InvitationId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define identity of invitation', 'SCHEMA', N'Invitation', 'TABLE', N'Invitation', 'COLUMN', N'InvitationId';

GO

DECLARE @var203 sysname;
SELECT @var203 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[IDM].[UserProfile]') AND [c].[name] = N'UserName');
IF @var203 IS NOT NULL EXEC(N'ALTER TABLE [IDM].[UserProfile] DROP CONSTRAINT [' + @var203 + '];');
ALTER TABLE [IDM].[UserProfile] ALTER COLUMN [UserName] nvarchar(20) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define user name of user profile', 'SCHEMA', N'IDM', 'TABLE', N'UserProfile', 'COLUMN', N'UserName';

GO

DECLARE @var204 sysname;
SELECT @var204 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[IDM].[UserProfile]') AND [c].[name] = N'UpdatedBy');
IF @var204 IS NOT NULL EXEC(N'ALTER TABLE [IDM].[UserProfile] DROP CONSTRAINT [' + @var204 + '];');
ALTER TABLE [IDM].[UserProfile] ALTER COLUMN [UpdatedBy] nvarchar(256) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Determine who updated user profile', 'SCHEMA', N'IDM', 'TABLE', N'UserProfile', 'COLUMN', N'UpdatedBy';

GO

DECLARE @var205 sysname;
SELECT @var205 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[IDM].[UserProfile]') AND [c].[name] = N'UpdatedAt');
IF @var205 IS NOT NULL EXEC(N'ALTER TABLE [IDM].[UserProfile] DROP CONSTRAINT [' + @var205 + '];');
ALTER TABLE [IDM].[UserProfile] ALTER COLUMN [UpdatedAt] datetime2 NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define updated date of user profile', 'SCHEMA', N'IDM', 'TABLE', N'UserProfile', 'COLUMN', N'UpdatedAt';

GO

DECLARE @var206 sysname;
SELECT @var206 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[IDM].[UserProfile]') AND [c].[name] = N'RowVersion');
IF @var206 IS NOT NULL EXEC(N'ALTER TABLE [IDM].[UserProfile] DROP CONSTRAINT [' + @var206 + '];');
ALTER TABLE [IDM].[UserProfile] ALTER COLUMN [RowVersion] varbinary(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define row version of user profile', 'SCHEMA', N'IDM', 'TABLE', N'UserProfile', 'COLUMN', N'RowVersion';

GO

DECLARE @var207 sysname;
SELECT @var207 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[IDM].[UserProfile]') AND [c].[name] = N'Mobile');
IF @var207 IS NOT NULL EXEC(N'ALTER TABLE [IDM].[UserProfile] DROP CONSTRAINT [' + @var207 + '];');
ALTER TABLE [IDM].[UserProfile] ALTER COLUMN [Mobile] nvarchar(20) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define mobile of user profile', 'SCHEMA', N'IDM', 'TABLE', N'UserProfile', 'COLUMN', N'Mobile';

GO

DECLARE @var208 sysname;
SELECT @var208 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[IDM].[UserProfile]') AND [c].[name] = N'IsActive');
IF @var208 IS NOT NULL EXEC(N'ALTER TABLE [IDM].[UserProfile] DROP CONSTRAINT [' + @var208 + '];');
ALTER TABLE [IDM].[UserProfile] ALTER COLUMN [IsActive] bit NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define user profile is active or not', 'SCHEMA', N'IDM', 'TABLE', N'UserProfile', 'COLUMN', N'IsActive';

GO

DECLARE @var209 sysname;
SELECT @var209 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[IDM].[UserProfile]') AND [c].[name] = N'FullName');
IF @var209 IS NOT NULL EXEC(N'ALTER TABLE [IDM].[UserProfile] DROP CONSTRAINT [' + @var209 + '];');
ALTER TABLE [IDM].[UserProfile] ALTER COLUMN [FullName] nvarchar(200) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define full name of user profile', 'SCHEMA', N'IDM', 'TABLE', N'UserProfile', 'COLUMN', N'FullName';

GO

DECLARE @var210 sysname;
SELECT @var210 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[IDM].[UserProfile]') AND [c].[name] = N'Email');
IF @var210 IS NOT NULL EXEC(N'ALTER TABLE [IDM].[UserProfile] DROP CONSTRAINT [' + @var210 + '];');
ALTER TABLE [IDM].[UserProfile] ALTER COLUMN [Email] nvarchar(100) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define email of user profile', 'SCHEMA', N'IDM', 'TABLE', N'UserProfile', 'COLUMN', N'Email';

GO

DECLARE @var211 sysname;
SELECT @var211 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[IDM].[UserProfile]') AND [c].[name] = N'DefaultUserRole');
IF @var211 IS NOT NULL EXEC(N'ALTER TABLE [IDM].[UserProfile] DROP CONSTRAINT [' + @var211 + '];');
ALTER TABLE [IDM].[UserProfile] ALTER COLUMN [DefaultUserRole] nvarchar(200) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define default user role of user profile', 'SCHEMA', N'IDM', 'TABLE', N'UserProfile', 'COLUMN', N'DefaultUserRole';

GO

DECLARE @var212 sysname;
SELECT @var212 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[IDM].[UserProfile]') AND [c].[name] = N'CreatedBy');
IF @var212 IS NOT NULL EXEC(N'ALTER TABLE [IDM].[UserProfile] DROP CONSTRAINT [' + @var212 + '];');
ALTER TABLE [IDM].[UserProfile] ALTER COLUMN [CreatedBy] nvarchar(256) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Determine who cretead user profile', 'SCHEMA', N'IDM', 'TABLE', N'UserProfile', 'COLUMN', N'CreatedBy';

GO

DECLARE @var213 sysname;
SELECT @var213 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[IDM].[UserProfile]') AND [c].[name] = N'CreatedAt');
IF @var213 IS NOT NULL EXEC(N'ALTER TABLE [IDM].[UserProfile] DROP CONSTRAINT [' + @var213 + '];');
ALTER TABLE [IDM].[UserProfile] ALTER COLUMN [CreatedAt] datetime2 NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define created date of user profile', 'SCHEMA', N'IDM', 'TABLE', N'UserProfile', 'COLUMN', N'CreatedAt';

GO

DECLARE @var214 sysname;
SELECT @var214 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[IDM].[UserProfile]') AND [c].[name] = N'Id');
IF @var214 IS NOT NULL EXEC(N'ALTER TABLE [IDM].[UserProfile] DROP CONSTRAINT [' + @var214 + '];');
ALTER TABLE [IDM].[UserProfile] ALTER COLUMN [Id] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'define identity of user profile', 'SCHEMA', N'IDM', 'TABLE', N'UserProfile', 'COLUMN', N'Id';

GO

DECLARE @var215 sysname;
SELECT @var215 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[_UserAudit]') AND [c].[name] = N'UserName');
IF @var215 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[_UserAudit] DROP CONSTRAINT [' + @var215 + '];');
ALTER TABLE [dbo].[_UserAudit] ALTER COLUMN [UserName] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define User Name of the user who take the action ', 'SCHEMA', N'dbo', 'TABLE', N'_UserAudit', 'COLUMN', N'UserName';

GO

DECLARE @var216 sysname;
SELECT @var216 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[_UserAudit]') AND [c].[name] = N'UserId');
IF @var216 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[_UserAudit] DROP CONSTRAINT [' + @var216 + '];');
ALTER TABLE [dbo].[_UserAudit] ALTER COLUMN [UserId] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the user id who making the action', 'SCHEMA', N'dbo', 'TABLE', N'_UserAudit', 'COLUMN', N'UserId';

GO

DECLARE @var217 sysname;
SELECT @var217 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[_UserAudit]') AND [c].[name] = N'Timestamp');
IF @var217 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[_UserAudit] DROP CONSTRAINT [' + @var217 + '];');
ALTER TABLE [dbo].[_UserAudit] ALTER COLUMN [Timestamp] datetime2 NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define time that action was done on ', 'SCHEMA', N'dbo', 'TABLE', N'_UserAudit', 'COLUMN', N'Timestamp';

GO

DECLARE @var218 sysname;
SELECT @var218 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[_UserAudit]') AND [c].[name] = N'ProcessId');
IF @var218 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[_UserAudit] DROP CONSTRAINT [' + @var218 + '];');
ALTER TABLE [dbo].[_UserAudit] ALTER COLUMN [ProcessId] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Unique Number to the process', 'SCHEMA', N'dbo', 'TABLE', N'_UserAudit', 'COLUMN', N'ProcessId';

GO

DECLARE @var219 sysname;
SELECT @var219 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[_UserAudit]') AND [c].[name] = N'Process');
IF @var219 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[_UserAudit] DROP CONSTRAINT [' + @var219 + '];');
ALTER TABLE [dbo].[_UserAudit] ALTER COLUMN [Process] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the Process status Success or Fail', 'SCHEMA', N'dbo', 'TABLE', N'_UserAudit', 'COLUMN', N'Process';

GO

DECLARE @var220 sysname;
SELECT @var220 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[_UserAudit]') AND [c].[name] = N'IpAddress');
IF @var220 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[_UserAudit] DROP CONSTRAINT [' + @var220 + '];');
ALTER TABLE [dbo].[_UserAudit] ALTER COLUMN [IpAddress] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define ip Address of Device that the user using ', 'SCHEMA', N'dbo', 'TABLE', N'_UserAudit', 'COLUMN', N'IpAddress';

GO

DECLARE @var221 sysname;
SELECT @var221 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[_UserAudit]') AND [c].[name] = N'FullName');
IF @var221 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[_UserAudit] DROP CONSTRAINT [' + @var221 + '];');
ALTER TABLE [dbo].[_UserAudit] ALTER COLUMN [FullName] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define User Full Name of the user who take the action', 'SCHEMA', N'dbo', 'TABLE', N'_UserAudit', 'COLUMN', N'FullName';

GO

DECLARE @var222 sysname;
SELECT @var222 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[_UserAudit]') AND [c].[name] = N'AuditEvent');
IF @var222 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[_UserAudit] DROP CONSTRAINT [' + @var222 + '];');
ALTER TABLE [dbo].[_UserAudit] ALTER COLUMN [AuditEvent] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the type of Action if Edit or Add or Delete', 'SCHEMA', N'dbo', 'TABLE', N'_UserAudit', 'COLUMN', N'AuditEvent';

GO

DECLARE @var223 sysname;
SELECT @var223 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[_UserAudit]') AND [c].[name] = N'Id');
IF @var223 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[_UserAudit] DROP CONSTRAINT [' + @var223 + '];');
ALTER TABLE [dbo].[_UserAudit] ALTER COLUMN [Id] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define Unique identifer Of  User Action Auditting', 'SCHEMA', N'dbo', 'TABLE', N'_UserAudit', 'COLUMN', N'Id';

GO

DECLARE @var224 sysname;
SELECT @var224 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[NegotiationSupplierQuantityTable]') AND [c].[name] = N'refNegotiationSecondStage');
IF @var224 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[NegotiationSupplierQuantityTable] DROP CONSTRAINT [' + @var224 + '];');
ALTER TABLE [CommunicationRequest].[NegotiationSupplierQuantityTable] ALTER COLUMN [refNegotiationSecondStage] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Refere to the related Negotioation Request ', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'NegotiationSupplierQuantityTable', 'COLUMN', N'refNegotiationSecondStage';

GO

DECLARE @var225 sysname;
SELECT @var225 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[NegotiationSupplierQuantityTable]') AND [c].[name] = N'SupplierQuantityTableId');
IF @var225 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[NegotiationSupplierQuantityTable] DROP CONSTRAINT [' + @var225 + '];');
ALTER TABLE [CommunicationRequest].[NegotiationSupplierQuantityTable] ALTER COLUMN [SupplierQuantityTableId] bigint NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Refer to the Original Supplier Quantity Table that Filled by supplier', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'NegotiationSupplierQuantityTable', 'COLUMN', N'SupplierQuantityTableId';

GO

DECLARE @var226 sysname;
SELECT @var226 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[NegotiationSupplierQuantityTable]') AND [c].[name] = N'Name');
IF @var226 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[NegotiationSupplierQuantityTable] DROP CONSTRAINT [' + @var226 + '];');
ALTER TABLE [CommunicationRequest].[NegotiationSupplierQuantityTable] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the Name Of Quantity Table ', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'NegotiationSupplierQuantityTable', 'COLUMN', N'Name';

GO

DECLARE @var227 sysname;
SELECT @var227 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[NegotiationSupplierQuantityTable]') AND [c].[name] = N'Id');
IF @var227 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[NegotiationSupplierQuantityTable] DROP CONSTRAINT [' + @var227 + '];');
ALTER TABLE [CommunicationRequest].[NegotiationSupplierQuantityTable] ALTER COLUMN [Id] bigint NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define Unique identifer Of Quantity Table ', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'NegotiationSupplierQuantityTable', 'COLUMN', N'Id';

GO

DECLARE @var228 sysname;
SELECT @var228 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[ExtendOffersValiditySupplier]') AND [c].[name] = N'SupplierExtendOfferValidityStatusId');
IF @var228 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] DROP CONSTRAINT [' + @var228 + '];');
ALTER TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] ALTER COLUMN [SupplierExtendOfferValidityStatusId] int NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the status of extend offers validity supplier based on supplier action on request', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'ExtendOffersValiditySupplier', 'COLUMN', N'SupplierExtendOfferValidityStatusId';

GO

DECLARE @var229 sysname;
SELECT @var229 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[ExtendOffersValiditySupplier]') AND [c].[name] = N'SupplierCR');
IF @var229 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] DROP CONSTRAINT [' + @var229 + '];');
ALTER TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] ALTER COLUMN [SupplierCR] nvarchar(20) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the supplier cr that extend offers validity sent to', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'ExtendOffersValiditySupplier', 'COLUMN', N'SupplierCR';

GO

DECLARE @var230 sysname;
SELECT @var230 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[ExtendOffersValiditySupplier]') AND [c].[name] = N'PeriodStartDateTime');
IF @var230 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] DROP CONSTRAINT [' + @var230 + '];');
ALTER TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] ALTER COLUMN [PeriodStartDateTime] datetime2 NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the the start date of extend offers validity period', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'ExtendOffersValiditySupplier', 'COLUMN', N'PeriodStartDateTime';

GO

DECLARE @var231 sysname;
SELECT @var231 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[ExtendOffersValiditySupplier]') AND [c].[name] = N'OfferId');
IF @var231 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] DROP CONSTRAINT [' + @var231 + '];');
ALTER TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] ALTER COLUMN [OfferId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related  supplier offer for extend offers validity', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'ExtendOffersValiditySupplier', 'COLUMN', N'OfferId';

GO

DECLARE @var232 sysname;
SELECT @var232 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[ExtendOffersValiditySupplier]') AND [c].[name] = N'IsReported');
IF @var232 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] DROP CONSTRAINT [' + @var232 + '];');
ALTER TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] ALTER COLUMN [IsReported] bit NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define if the request is reported or not', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'ExtendOffersValiditySupplier', 'COLUMN', N'IsReported';

GO

DECLARE @var233 sysname;
SELECT @var233 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[ExtendOffersValiditySupplier]') AND [c].[name] = N'ExtendOffersValidityId');
IF @var233 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] DROP CONSTRAINT [' + @var233 + '];');
ALTER TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] ALTER COLUMN [ExtendOffersValidityId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the parent extend offers validity for extend offers validity supplier request', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'ExtendOffersValiditySupplier', 'COLUMN', N'ExtendOffersValidityId';

GO

DECLARE @var234 sysname;
SELECT @var234 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[ExtendOffersValiditySupplier]') AND [c].[name] = N'Id');
IF @var234 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] DROP CONSTRAINT [' + @var234 + '];');
ALTER TABLE [CommunicationRequest].[ExtendOffersValiditySupplier] ALTER COLUMN [Id] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define a unique identifer of extend offers validity supplier', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'ExtendOffersValiditySupplier', 'COLUMN', N'Id';

GO

DECLARE @var235 sysname;
SELECT @var235 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[ExtendOffersValidity]') AND [c].[name] = N'ReplyReceivingDurationTime');
IF @var235 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[ExtendOffersValidity] DROP CONSTRAINT [' + @var235 + '];');
ALTER TABLE [CommunicationRequest].[ExtendOffersValidity] ALTER COLUMN [ReplyReceivingDurationTime] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the time to reply the extend offers validity request', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'ExtendOffersValidity', 'COLUMN', N'ReplyReceivingDurationTime';

GO

DECLARE @var236 sysname;
SELECT @var236 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[ExtendOffersValidity]') AND [c].[name] = N'ReplyReceivingDurationDays');
IF @var236 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[ExtendOffersValidity] DROP CONSTRAINT [' + @var236 + '];');
ALTER TABLE [CommunicationRequest].[ExtendOffersValidity] ALTER COLUMN [ReplyReceivingDurationDays] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define number of days to allow suppliers to reply the extend offers validity request', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'ExtendOffersValidity', 'COLUMN', N'ReplyReceivingDurationDays';

GO

DECLARE @var237 sysname;
SELECT @var237 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[ExtendOffersValidity]') AND [c].[name] = N'OffersDuration');
IF @var237 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[ExtendOffersValidity] DROP CONSTRAINT [' + @var237 + '];');
ALTER TABLE [CommunicationRequest].[ExtendOffersValidity] ALTER COLUMN [OffersDuration] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the duration in days to end expire the extend offers validity request', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'ExtendOffersValidity', 'COLUMN', N'OffersDuration';

GO

DECLARE @var238 sysname;
SELECT @var238 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[ExtendOffersValidity]') AND [c].[name] = N'NewOffersExpiryDate');
IF @var238 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[ExtendOffersValidity] DROP CONSTRAINT [' + @var238 + '];');
ALTER TABLE [CommunicationRequest].[ExtendOffersValidity] ALTER COLUMN [NewOffersExpiryDate] datetime2 NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the expiration date of extend offers validity request', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'ExtendOffersValidity', 'COLUMN', N'NewOffersExpiryDate';

GO

DECLARE @var239 sysname;
SELECT @var239 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[ExtendOffersValidity]') AND [c].[name] = N'ExtendOffersReason');
IF @var239 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[ExtendOffersValidity] DROP CONSTRAINT [' + @var239 + '];');
ALTER TABLE [CommunicationRequest].[ExtendOffersValidity] ALTER COLUMN [ExtendOffersReason] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the reason of extend offers validity request', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'ExtendOffersValidity', 'COLUMN', N'ExtendOffersReason';

GO

DECLARE @var240 sysname;
SELECT @var240 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[ExtendOffersValidity]') AND [c].[name] = N'AgencyCommunicationRequestId');
IF @var240 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[ExtendOffersValidity] DROP CONSTRAINT [' + @var240 + '];');
ALTER TABLE [CommunicationRequest].[ExtendOffersValidity] ALTER COLUMN [AgencyCommunicationRequestId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the parent commmunication request for extend offers validity request', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'ExtendOffersValidity', 'COLUMN', N'AgencyCommunicationRequestId';

GO

DECLARE @var241 sysname;
SELECT @var241 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[ExtendOffersValidity]') AND [c].[name] = N'ExtendOffersValidityId');
IF @var241 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[ExtendOffersValidity] DROP CONSTRAINT [' + @var241 + '];');
ALTER TABLE [CommunicationRequest].[ExtendOffersValidity] ALTER COLUMN [ExtendOffersValidityId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define a unique identifer of extend offers validity request', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'ExtendOffersValidity', 'COLUMN', N'ExtendOffersValidityId';

GO

DECLARE @var242 sysname;
SELECT @var242 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationRequest].[AgencyCommunicationRequest]') AND [c].[name] = N'TenderId');
IF @var242 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationRequest].[AgencyCommunicationRequest] DROP CONSTRAINT [' + @var242 + '];');
ALTER TABLE [CommunicationRequest].[AgencyCommunicationRequest] ALTER COLUMN [TenderId] int NOT NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'AgencyCommunicationRequest', 'COLUMN', N'TenderId';
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Tender of agency communication request', 'SCHEMA', N'CommunicationRequest', 'TABLE', N'AgencyCommunicationRequest', 'COLUMN', N'TenderId';

GO

DECLARE @var243 sysname;
SELECT @var243 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationAgency].[ExtendOffersValidityAttachment]') AND [c].[name] = N'Name');
IF @var243 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationAgency].[ExtendOffersValidityAttachment] DROP CONSTRAINT [' + @var243 + '];');
ALTER TABLE [CommunicationAgency].[ExtendOffersValidityAttachment] ALTER COLUMN [Name] nvarchar(200) NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the name of file attached', 'SCHEMA', N'CommunicationAgency', 'TABLE', N'ExtendOffersValidityAttachment', 'COLUMN', N'Name';

GO

DECLARE @var244 sysname;
SELECT @var244 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationAgency].[ExtendOffersValidityAttachment]') AND [c].[name] = N'FileNetReferenceId');
IF @var244 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationAgency].[ExtendOffersValidityAttachment] DROP CONSTRAINT [' + @var244 + '];');
ALTER TABLE [CommunicationAgency].[ExtendOffersValidityAttachment] ALTER COLUMN [FileNetReferenceId] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define The reference number from file Net', 'SCHEMA', N'CommunicationAgency', 'TABLE', N'ExtendOffersValidityAttachment', 'COLUMN', N'FileNetReferenceId';

GO

DECLARE @var245 sysname;
SELECT @var245 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationAgency].[ExtendOffersValidityAttachment]') AND [c].[name] = N'ExtendOffersValiditySupplierId');
IF @var245 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationAgency].[ExtendOffersValidityAttachment] DROP CONSTRAINT [' + @var245 + '];');
ALTER TABLE [CommunicationAgency].[ExtendOffersValidityAttachment] ALTER COLUMN [ExtendOffersValiditySupplierId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the parent extend offers validity for extend offers validity supplier request', 'SCHEMA', N'CommunicationAgency', 'TABLE', N'ExtendOffersValidityAttachment', 'COLUMN', N'ExtendOffersValiditySupplierId';

GO

DECLARE @var246 sysname;
SELECT @var246 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationAgency].[ExtendOffersValidityAttachment]') AND [c].[name] = N'AttachmentTypeId');
IF @var246 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationAgency].[ExtendOffersValidityAttachment] DROP CONSTRAINT [' + @var246 + '];');
ALTER TABLE [CommunicationAgency].[ExtendOffersValidityAttachment] ALTER COLUMN [AttachmentTypeId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'the type of attachment file like [supplier attachment or agency attachment]', 'SCHEMA', N'CommunicationAgency', 'TABLE', N'ExtendOffersValidityAttachment', 'COLUMN', N'AttachmentTypeId';

GO

DECLARE @var247 sysname;
SELECT @var247 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationAgency].[ExtendOffersValidityAttachment]') AND [c].[name] = N'AttachmentId');
IF @var247 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationAgency].[ExtendOffersValidityAttachment] DROP CONSTRAINT [' + @var247 + '];');
ALTER TABLE [CommunicationAgency].[ExtendOffersValidityAttachment] ALTER COLUMN [AttachmentId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the parent extend offers validity for extend offers validity supplier request', 'SCHEMA', N'CommunicationAgency', 'TABLE', N'ExtendOffersValidityAttachment', 'COLUMN', N'AttachmentId';

GO

DECLARE @var248 sysname;
SELECT @var248 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchUser]') AND [c].[name] = N'UserRoleId');
IF @var248 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchUser] DROP CONSTRAINT [' + @var248 + '];');
ALTER TABLE [Branch].[BranchUser] ALTER COLUMN [UserRoleId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define user role inside the branch', 'SCHEMA', N'Branch', 'TABLE', N'BranchUser', 'COLUMN', N'UserRoleId';

GO

DECLARE @var249 sysname;
SELECT @var249 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchUser]') AND [c].[name] = N'UserProfileId');
IF @var249 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchUser] DROP CONSTRAINT [' + @var249 + '];');
ALTER TABLE [Branch].[BranchUser] ALTER COLUMN [UserProfileId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'reference the user full profile', 'SCHEMA', N'Branch', 'TABLE', N'BranchUser', 'COLUMN', N'UserProfileId';

GO

DECLARE @var250 sysname;
SELECT @var250 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchUser]') AND [c].[name] = N'RelatedAgencyCode');
IF @var250 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchUser] DROP CONSTRAINT [' + @var250 + '];');
ALTER TABLE [Branch].[BranchUser] ALTER COLUMN [RelatedAgencyCode] nvarchar(max) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the agency which the branch is related to', 'SCHEMA', N'Branch', 'TABLE', N'BranchUser', 'COLUMN', N'RelatedAgencyCode';

GO

DECLARE @var251 sysname;
SELECT @var251 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchUser]') AND [c].[name] = N'EstimatedValueTo');
IF @var251 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchUser] DROP CONSTRAINT [' + @var251 + '];');
ALTER TABLE [Branch].[BranchUser] ALTER COLUMN [EstimatedValueTo] decimal(18,2) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the biggest estimated value', 'SCHEMA', N'Branch', 'TABLE', N'BranchUser', 'COLUMN', N'EstimatedValueTo';

GO

DECLARE @var252 sysname;
SELECT @var252 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchUser]') AND [c].[name] = N'EstimatedValueFrom');
IF @var252 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchUser] DROP CONSTRAINT [' + @var252 + '];');
ALTER TABLE [Branch].[BranchUser] ALTER COLUMN [EstimatedValueFrom] decimal(18,2) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the smallest estimated value', 'SCHEMA', N'Branch', 'TABLE', N'BranchUser', 'COLUMN', N'EstimatedValueFrom';

GO

DECLARE @var253 sysname;
SELECT @var253 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchUser]') AND [c].[name] = N'BranchId');
IF @var253 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchUser] DROP CONSTRAINT [' + @var253 + '];');
ALTER TABLE [Branch].[BranchUser] ALTER COLUMN [BranchId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Refere to the Branch', 'SCHEMA', N'Branch', 'TABLE', N'BranchUser', 'COLUMN', N'BranchId';

GO

DECLARE @var254 sysname;
SELECT @var254 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchUser]') AND [c].[name] = N'BranchUserId');
IF @var254 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchUser] DROP CONSTRAINT [' + @var254 + '];');
ALTER TABLE [Branch].[BranchUser] ALTER COLUMN [BranchUserId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define a unique identifer of Branch User', 'SCHEMA', N'Branch', 'TABLE', N'BranchUser', 'COLUMN', N'BranchUserId';

GO

DECLARE @var255 sysname;
SELECT @var255 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchCommittee]') AND [c].[name] = N'CommitteeId');
IF @var255 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchCommittee] DROP CONSTRAINT [' + @var255 + '];');
ALTER TABLE [Branch].[BranchCommittee] ALTER COLUMN [CommitteeId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Refere to the Committie', 'SCHEMA', N'Branch', 'TABLE', N'BranchCommittee', 'COLUMN', N'CommitteeId';

GO

DECLARE @var256 sysname;
SELECT @var256 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchCommittee]') AND [c].[name] = N'BranchId');
IF @var256 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchCommittee] DROP CONSTRAINT [' + @var256 + '];');
ALTER TABLE [Branch].[BranchCommittee] ALTER COLUMN [BranchId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Refere to the Branch', 'SCHEMA', N'Branch', 'TABLE', N'BranchCommittee', 'COLUMN', N'BranchId';

GO

DECLARE @var257 sysname;
SELECT @var257 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchCommittee]') AND [c].[name] = N'BranchCommitteeId');
IF @var257 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchCommittee] DROP CONSTRAINT [' + @var257 + '];');
ALTER TABLE [Branch].[BranchCommittee] ALTER COLUMN [BranchCommitteeId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define a unique identifer of Branch Committee', 'SCHEMA', N'Branch', 'TABLE', N'BranchCommittee', 'COLUMN', N'BranchCommitteeId';

GO

DECLARE @var258 sysname;
SELECT @var258 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchAddresse]') AND [c].[name] = N'ZipCode');
IF @var258 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchAddresse] DROP CONSTRAINT [' + @var258 + '];');
ALTER TABLE [Branch].[BranchAddresse] ALTER COLUMN [ZipCode] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define Zip Code of the address', 'SCHEMA', N'Branch', 'TABLE', N'BranchAddresse', 'COLUMN', N'ZipCode';

GO

DECLARE @var259 sysname;
SELECT @var259 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchAddresse]') AND [c].[name] = N'PostalCode');
IF @var259 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchAddresse] DROP CONSTRAINT [' + @var259 + '];');
ALTER TABLE [Branch].[BranchAddresse] ALTER COLUMN [PostalCode] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define Postal Code of the address', 'SCHEMA', N'Branch', 'TABLE', N'BranchAddresse', 'COLUMN', N'PostalCode';

GO

DECLARE @var260 sysname;
SELECT @var260 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchAddresse]') AND [c].[name] = N'Position');
IF @var260 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchAddresse] DROP CONSTRAINT [' + @var260 + '];');
ALTER TABLE [Branch].[BranchAddresse] ALTER COLUMN [Position] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the Position of the address', 'SCHEMA', N'Branch', 'TABLE', N'BranchAddresse', 'COLUMN', N'Position';

GO

DECLARE @var261 sysname;
SELECT @var261 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchAddresse]') AND [c].[name] = N'Phone2');
IF @var261 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchAddresse] DROP CONSTRAINT [' + @var261 + '];');
ALTER TABLE [Branch].[BranchAddresse] ALTER COLUMN [Phone2] nvarchar(100) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define second phone number of the address', 'SCHEMA', N'Branch', 'TABLE', N'BranchAddresse', 'COLUMN', N'Phone2';

GO

DECLARE @var262 sysname;
SELECT @var262 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchAddresse]') AND [c].[name] = N'Phone');
IF @var262 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchAddresse] DROP CONSTRAINT [' + @var262 + '];');
ALTER TABLE [Branch].[BranchAddresse] ALTER COLUMN [Phone] nvarchar(100) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define phone number of the address', 'SCHEMA', N'Branch', 'TABLE', N'BranchAddresse', 'COLUMN', N'Phone';

GO

DECLARE @var263 sysname;
SELECT @var263 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchAddresse]') AND [c].[name] = N'Fax2');
IF @var263 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchAddresse] DROP CONSTRAINT [' + @var263 + '];');
ALTER TABLE [Branch].[BranchAddresse] ALTER COLUMN [Fax2] nvarchar(100) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define second FAX number of the address', 'SCHEMA', N'Branch', 'TABLE', N'BranchAddresse', 'COLUMN', N'Fax2';

GO

DECLARE @var264 sysname;
SELECT @var264 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchAddresse]') AND [c].[name] = N'Fax');
IF @var264 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchAddresse] DROP CONSTRAINT [' + @var264 + '];');
ALTER TABLE [Branch].[BranchAddresse] ALTER COLUMN [Fax] nvarchar(100) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define FAX number of the address', 'SCHEMA', N'Branch', 'TABLE', N'BranchAddresse', 'COLUMN', N'Fax';

GO

DECLARE @var265 sysname;
SELECT @var265 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchAddresse]') AND [c].[name] = N'Description');
IF @var265 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchAddresse] DROP CONSTRAINT [' + @var265 + '];');
ALTER TABLE [Branch].[BranchAddresse] ALTER COLUMN [Description] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define Description of the address', 'SCHEMA', N'Branch', 'TABLE', N'BranchAddresse', 'COLUMN', N'Description';

GO

DECLARE @var266 sysname;
SELECT @var266 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchAddresse]') AND [c].[name] = N'CityCode');
IF @var266 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchAddresse] DROP CONSTRAINT [' + @var266 + '];');
ALTER TABLE [Branch].[BranchAddresse] ALTER COLUMN [CityCode] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define City Code of the address', 'SCHEMA', N'Branch', 'TABLE', N'BranchAddresse', 'COLUMN', N'CityCode';

GO

DECLARE @var267 sysname;
SELECT @var267 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchAddresse]') AND [c].[name] = N'AddressTypeId');
IF @var267 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchAddresse] DROP CONSTRAINT [' + @var267 + '];');
ALTER TABLE [Branch].[BranchAddresse] ALTER COLUMN [AddressTypeId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the type of address', 'SCHEMA', N'Branch', 'TABLE', N'BranchAddresse', 'COLUMN', N'AddressTypeId';

GO

DECLARE @var268 sysname;
SELECT @var268 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchAddresse]') AND [c].[name] = N'AddressName');
IF @var268 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchAddresse] DROP CONSTRAINT [' + @var268 + '];');
ALTER TABLE [Branch].[BranchAddresse] ALTER COLUMN [AddressName] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the branch Address name', 'SCHEMA', N'Branch', 'TABLE', N'BranchAddresse', 'COLUMN', N'AddressName';

GO

DECLARE @var269 sysname;
SELECT @var269 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchAddresse]') AND [c].[name] = N'Address');
IF @var269 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchAddresse] DROP CONSTRAINT [' + @var269 + '];');
ALTER TABLE [Branch].[BranchAddresse] ALTER COLUMN [Address] nvarchar(1024) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define The detailed loction  of the Branch Address', 'SCHEMA', N'Branch', 'TABLE', N'BranchAddresse', 'COLUMN', N'Address';

GO

DECLARE @var270 sysname;
SELECT @var270 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[BranchAddresse]') AND [c].[name] = N'BranchAddressId');
IF @var270 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[BranchAddresse] DROP CONSTRAINT [' + @var270 + '];');
ALTER TABLE [Branch].[BranchAddresse] ALTER COLUMN [BranchAddressId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define a unique identifer of Branch Address', 'SCHEMA', N'Branch', 'TABLE', N'BranchAddresse', 'COLUMN', N'BranchAddressId';

GO

DECLARE @var271 sysname;
SELECT @var271 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[Branch]') AND [c].[name] = N'BranchName');
IF @var271 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[Branch] DROP CONSTRAINT [' + @var271 + '];');
ALTER TABLE [Branch].[Branch] ALTER COLUMN [BranchName] nvarchar(1024) NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the branch name', 'SCHEMA', N'Branch', 'TABLE', N'Branch', 'COLUMN', N'BranchName';

GO

DECLARE @var272 sysname;
SELECT @var272 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[Branch]') AND [c].[name] = N'AgencyCode');
IF @var272 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[Branch] DROP CONSTRAINT [' + @var272 + '];');
ALTER TABLE [Branch].[Branch] ALTER COLUMN [AgencyCode] nvarchar(20) NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define the related Agency', 'SCHEMA', N'Branch', 'TABLE', N'Branch', 'COLUMN', N'AgencyCode';

GO

DECLARE @var273 sysname;
SELECT @var273 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Branch].[Branch]') AND [c].[name] = N'BranchId');
IF @var273 IS NOT NULL EXEC(N'ALTER TABLE [Branch].[Branch] DROP CONSTRAINT [' + @var273 + '];');
ALTER TABLE [Branch].[Branch] ALTER COLUMN [BranchId] int NOT NULL;
EXEC sp_addextendedproperty 'MS_Description', N'Define a unique identifer of Branch', 'SCHEMA', N'Branch', 'TABLE', N'Branch', 'COLUMN', N'BranchId';

GO

DECLARE @var274 sysname;
SELECT @var274 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AnnouncementTemplate].[AnnouncementSupplierTemplate]') AND [c].[name] = N'TenderTypesId');
IF @var274 IS NOT NULL EXEC(N'ALTER TABLE [AnnouncementTemplate].[AnnouncementSupplierTemplate] DROP CONSTRAINT [' + @var274 + '];');
ALTER TABLE [AnnouncementTemplate].[AnnouncementSupplierTemplate] ALTER COLUMN [TenderTypesId] nvarchar(max) NULL;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', N'AnnouncementTemplate', 'TABLE', N'AnnouncementSupplierTemplate', 'COLUMN', N'TenderTypesId';
EXEC sp_addextendedproperty 'MS_Description', N'Define type of Tender', 'SCHEMA', N'AnnouncementTemplate', 'TABLE', N'AnnouncementSupplierTemplate', 'COLUMN', N'TenderTypesId';

GO
