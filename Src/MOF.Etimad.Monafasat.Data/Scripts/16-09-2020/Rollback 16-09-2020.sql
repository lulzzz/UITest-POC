

UPDATE [LookUps].[TenderStatus] SET NameAr = N'مرحلة التقييم المالي' WHERE TenderStatusId = 31

DELETE FROM [LookUps].[TenderStatus] where [TenderStatusId] = 85
DELETE FROM [LookUps].[TenderStatus] where [TenderStatusId] = 86
DELETE FROM [LookUps].[TenderStatus] where [TenderStatusId] = 87
DELETE FROM [LookUps].[TenderStatus] where [TenderStatusId] = 78
DELETE FROM [LookUps].[TenderStatus] where [TenderStatusId] = 80
DELETE FROM [LookUps].[TenderStatus] where [TenderStatusId] = 82
DELETE FROM [LookUps].[TenderStatus] where [TenderStatusId] = 84
DELETE FROM [LookUps].[TenderStatus] where [TenderStatusId] = 90
GO

DELETE FROM  LookUps.TenderAction where  TenderActionId = 302
DELETE FROM  LookUps.TenderAction where  TenderActionId = 303
DELETE FROM  LookUps.TenderAction where  TenderActionId = 304
DELETE FROM  LookUps.TenderAction where  TenderActionId = 305
DELETE FROM  LookUps.TenderAction where  TenderActionId = 306
DELETE FROM  LookUps.TenderAction where  TenderActionId = 307
DELETE FROM  LOOKUPS.NOTIFICATIONOPERATIONCODE WHERE NOTIFICATIONOPERATIONCODEID  IN (7900,8000,8002,8004,8006,8008,8010,8012)
DELETE FROM  LOOKUPS.NOTIFICATIONOPERATIONCODE WHERE NOTIFICATIONOPERATIONCODEID = 555 
delete from LookUps.TenderStatus where  TenderStatusId = 92

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = N'20200927092437_added_FinancialCheckingDateSet';

GO
 begin transaction   
 update Tender.Tender   set TenderStatusId = 18   where  SubmitionDate >= '2020-04-24' and OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId = 90
 
commit 
-- rollback  
go

 --roll out
 --1 -- update from current technical open to new technical open
 begin transaction   
 update Tender.Tender   set TenderStatusId =  6  where  SubmitionDate >= '2020-04-24' and OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId =78
 update Tender.Tender   set TenderStatusId =  7  where  SubmitionDate >= '2020-04-24' and OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId =80
 update Tender.Tender   set TenderStatusId =  8  where  SubmitionDate >= '2020-04-24' and OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId =82
 update Tender.Tender   set TenderStatusId =  9  where  SubmitionDate >= '2020-04-24' and OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId =84
 commit 