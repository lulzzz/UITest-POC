 --roll out
 --1 -- update from current technical open to new technical open
 begin transaction  firstSection
 update Tender.Tender   set TenderStatusId = 78   where  SubmitionDate >= '2020-04-24' and OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId =6
 update Tender.Tender   set TenderStatusId = 80   where  SubmitionDate >= '2020-04-24' and OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId =7
 update Tender.Tender   set TenderStatusId = 82   where  SubmitionDate >= '2020-04-24' and OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId =8
 update Tender.Tender   set TenderStatusId = 84   where  SubmitionDate >= '2020-04-24' and OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId =9
 commit 
 -- rollback  firstSection 
  --2 -- remove financial (open and check) data
  begin transaction  secondSection
  delete  from [SupplierBankGuaranteeDetail] where OfferId in (
  select offer.offerId from Offer.Offer offer join Tender.Tender t on offer.tenderid  = t.tenderid  where 
  SubmitionDate >= '2020-04-24' and t.OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId in (31,
32,
34,
46) 
)
   update    Offer.Offer   set offer.IsBankGuaranteeAttached = null , 
offer.IsFinantialOfferLetterCopyAttached  = null , offer.IsFinaintialOfferLetterAttached = null  ,offer.FinantialOfferLetterDate = null ,offer.FinantialOfferLetterNumber = null 
 where offer.tenderid  in ( select  t.tenderid    from tender .Tender t where 
  SubmitionDate >= '2020-04-24' and t.OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId in (31,
32,
34,
46) )
-- rollback 
commit
 --3 -- update financial (check) to be in fianacial (open)

  begin transaction  thirdSection
 update Tender.Tender   set TenderStatusId = 90   where  SubmitionDate >= '2020-04-24' and OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId =18
 update Tender.Tender   set TenderStatusId = 46   where  SubmitionDate >= '2020-04-24' and OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId =31
 update Tender.Tender   set TenderStatusId = 46   where  SubmitionDate >= '2020-04-24' and OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId =32
 update Tender.Tender   set TenderStatusId = 46   where  SubmitionDate >= '2020-04-24' and OfferPresentationWayId = 2 and  TenderTypeId <> 2  and  TenderStatusId =34
commit 
-- rollback  
go