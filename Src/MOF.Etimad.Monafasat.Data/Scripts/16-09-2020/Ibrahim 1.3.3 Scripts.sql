 --rollout
INSERT INTO [LookUps].[TenderStatus]
           ([TenderStatusId]
           ,[NameAr]
           ,[NameEn])
     VALUES
           (78
           ,N'مرحلة فتح العروض الفنية'
           ,null)
         
		 , (80
           ,N'بإنتظار إعتماد تقرير فتح العروض الفنية'
           ,null)
          
		  
		  , (82
           ,N'تم فتح العروض الفنية'
           ,null)
		   
		   , (84
           ,N'تم رفض تقرير فتح العروض الفنية'
           ,null)
GO



insert into  LookUps.TenderAction  (TenderActionId, NameAr, NameEn ) values (302	,N'تاكيد مرحلة فتح العروض الفنية',	'OpenTechnicalEnvelope')

 go

 --rollback
 delete from  LookUps.TenderAction where  TenderActionId = 302

delete from [LookUps].[TenderStatus] where [TenderStatusId]  in (78,80,82,84)