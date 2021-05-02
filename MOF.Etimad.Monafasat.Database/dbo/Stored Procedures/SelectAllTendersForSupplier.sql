CREATE PROCEDURE [dbo].[SelectAllTendersForSupplier]( 
@Cr nvarchar(30),
@TenderTypeId int null,
@PublishDate nvarchar(max) ,
@FromLastOfferPresentationDate nvarchar(max) ,
@ToLastOfferPresentationDate nvarchar(max) ,
@AgencyCode nvarchar(50) ,
@ReferenceNumber nvarchar(30) ,
@TenderName nvarchar(200),
@BookletRang int null,
@TenderAreasIds nvarchar(max),
@blockedAgences nvarchar(max),
@TenderActivityId int null,
@TenderSubActivityId int null,
@TenderCategoryId int null,
@supplierTender bit,
@page INT,
 @size INT,
 @sort nvarchar(50) ,
 @totalrow INT  OUTPUT
)
AS 
declare @query as nvarchar(max);  

 declare @startRange as nvarchar(10);
 declare @endtRange as nvarchar(10);

 declare @blockedagencesCount int;
declare @AreasCount int;

 DECLARE @offset INT
    DECLARE @newsize INT 


    IF(@page=0)
      BEGIN
        SET @offset = @page
        SET @newsize = @size
       END
    ELSE 
      BEGIN
        SET @offset = @page*@size
        SET @newsize = @size-1
      END
    SET NOCOUNT ON


create table #TenderAreasIds (
Id int
)
insert into #TenderAreasIds (Id)
   
 (
  SELECT 
     Split.a.value('.', 'int') AS CVS  
  FROM  
  (
    SELECT CAST ('<M>' + REPLACE(@TenderAreasIds, ',', '</M><M>') + '</M>' AS XML) AS CVS 
  ) AS A CROSS APPLY CVS.nodes ('/M') AS Split(a)
)
create table #blockedAgencesTable (
Code nvarchar(50)
)
insert into #blockedAgencesTable (Code)
   
 (
  SELECT 
     Split.a.value('.', 'nvarchar') AS CVS  
  FROM  
  (
    SELECT CAST ('<M>' + REPLACE(@blockedAgences, ',', '</M><M>') + '</M>' AS XML) AS CVS 
  ) AS A CROSS APPLY CVS.nodes ('/M') AS Split(a)
)
 


set  @blockedagencesCount =  (select count(code) from #blockedAgencesTable);
 set @AreasCount =(select  count(Id) from #TenderAreasIds);
--select * from  #blockedAgencesTable
--select * from #TenderAreasIds
create table #tempTenders (
tenderid bigint,AgencyCode nvarchar(100),
TenderName   nvarchar(200),SubmitionDate   nvarchar(200),ReferenceNumber   nvarchar(200),LastOfferPresentationDate   nvarchar(200) ,OffersOpeningDate   nvarchar(200)  ,OffersCheckingDate
  nvarchar(200) ,ConditionsBookletPrice float


)
 IF(@page=0)
      BEGIN
        SET @offset = @page
        SET @newsize = @size
       END
    ELSE 
      BEGIN
        SET @offset = @page*@size
        SET @newsize = @size-1
      END
    SET NOCOUNT ON



 IF ( @BookletRang  = 0) 
 BEGIN
            SET  @startRange = 0;
            SET  @endtRange = 0;
        END        
  ELSE IF (  @BookletRang  = 1 )
  BEGIN
            SET @startRange = 1;
             SET    @endtRange = 1000;
       END             
            
            ELSE  IF (  @BookletRang  = 2   )
          BEGIN          SET @startRange = '1001';
                    SET  @endtRange = '10000'
               END     
                 ELSE IF (  @BookletRang  = 3  )
                  BEGIN SET   @startRange = '10001';
                  SET   @endtRange = '20000';
                     END
                 ELSE IF (  @BookletRang  = 4  )
                 BEGIN SET    @startRange = '20001';
                 SET    @endtRange = '40000';
                     END
                 ELSE IF (  @BookletRang  = 5  )
                  BEGIN SET   @startRange = '40001';
                   SET  @endtRange = '50000';
                    END
                 ELSE IF (  @BookletRang  = 6  )
                  BEGIN
                  SET   @startRange = '50001';
                  SET   @endtRange = '1000000000';
                  END ;
  --select @startRange,@endtRange;
    create table #areaTendersId (tenderid bigint)
    create table #supplierINVTendersId (tenderid bigint)
    create table #supplierAcceptedINVTendersId (tenderid bigint)
    create table #supplierOffersTendersId (tenderid bigint)
    create table #supplierCPTendersId (tenderid bigint)  
    create table #supplierGeneralTable (tenderid bigint)  
    create table #supplierGeneralTableAll (tenderid bigint)  

      insert into #supplierGeneralTableAll select distinct  tenderid from tender.ConditionsBooklet where CommericalRegisterNo = @Cr 
      insert into #supplierINVTendersId select distinct  inv.tenderid  from Invitation.Invitation inv inner join tender.Tender on inv.TenderId = Tender.TenderId
      where inv.CommericalRegisterNo = @Cr and ((inv.StatusId != 3 and  inv.StatusId !=4) or Tender.InvitationTypeId = 1)

	  insert into #supplierGeneralTableAll select  distinct inv.tenderid  from Invitation.Invitation inv  
      where inv.CommericalRegisterNo = @Cr and inv.StatusId != 3 and  inv.StatusId !=4 and inv.StatusId = 2

       insert into #supplierGeneralTableAll select distinct TenderId from Offer.Offer  offer where CommericalRegisterNo = @Cr and  OfferStatusId != 6

insert into #supplierGeneralTableAll select distinct TenderId from Offer.Offer  offer join offer.OfferSolidarity offsol on offer.OfferId = offsol.OfferId 
      where offsol.CRNumber= @Cr and offsol.StatusId !=4  and offsol.StatusId !=3   and  OfferStatusId != 6
	
       insert into #areaTendersId    select distinct area.TenderId  from Tender.TenderArea area inner join  #TenderAreasIds on area.Id = #TenderAreasIds.Id
	  where area .IsActive = 1 


	  insert into #supplierGeneralTable select distinct tenderid from   #supplierGeneralTableAll
set @query = 'select  tender.tenderid, tender.AgencyCode,tender.TenderName,tender.SubmitionDate,tender.ReferenceNumber,tender.LastOfferPresentationDate ,tender.OffersOpeningDate , tender.OffersCheckingDate
, tender.ConditionsBookletPrice FROM Tender.Tender tender ';
IF(@TenderActivityId != 0 and @TenderActivityId is not null)
set @query = @query + ' INNER JOIN TENDER.tenderactivities act  on act.tenderid = tender.tenderid where act.activityid= '+@TenderActivityId;
IF(@TenderSubActivityId != 0 and @TenderSubActivityId is not null)
set @query = @query + ' INNER JOIN TENDER.tenderactivities act  on act.tenderid = tender.tenderid where act.activityid= '+@TenderSubActivityId;
 set @query = @query +'WHERE 1=1  and tender.isactive = 1 '
 if(@BookletRang is not null)
set @query = @query + ' AND tender.ConditionsBookletPrice > '+ @startRange + ' AND  tender.ConditionsBookletPrice < '+@endtRange

set @query =  @query + ' and tender.TenderTypeId != 3 and tendertypeid != 8 and tender.TenderStatusId != 1  and tender.TenderStatusId != 21  
 and tender.TenderStatusId != 3 and tender.TenderStatusId != 17  
 and tender.isactive = 1   and tender.SubmitionDate is not null';

if(@ReferenceNumber is not  null and  @ReferenceNumber != ''   )
begin 
Set @query = @query + ' and tender.referencenumber = '+@ReferenceNumber; 
end
--x.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && x.LastOfferPresentationDate <= DateTime.Now
if(@TenderCategoryId != 0  and  @TenderCategoryId != 3  )
begin 
Set @query = @query + ' and tender.TenderStatusId != 1 and  tender.LastOfferPresentationDate <= GETDATE() '; 
end
if(@AgencyCode is NOT null and @AgencyCode != ''   )
begin 
Set @query = @query + ' and tender.agencycode = '''+@AgencyCode+''''; 
end


if(@FromLastOfferPresentationDate is  NOT null )
begin 
Set @query = @query + ' and tender.LastOfferPresentationDate >= '''+ @FromLastOfferPresentationDate+'''';
end
if(@ToLastOfferPresentationDate is  NOT null )
begin 
Set @query = @query + ' and tender.LastOfferPresentationDate <= '''+ @ToLastOfferPresentationDate+'''';
end
if(@PublishDate is NOT null)
begin 
Set @query = @query + ' and tender.SubmitionDate >= '''+ @PublishDate+'''';
end
if(@TenderName is NOT null and  @TenderName != ''   )
begin 
Set @query = @query + ' and tender.tendername like ''%'+@TenderName+'%''';
end
if(@TenderTypeId != 0 and @TenderTypeId != 111 and @TenderTypeId !=112  )
begin  
Set @query = @query + ' and tender.tendertypeid ='+convert(varchar(20), @TenderTypeId);
end
if(@TenderTypeId != 0 and @TenderTypeId = 111  )
begin 
Set @query = @query + ' and (tender.tendertypeid = 10  or tender.TenderTypeId =  2)';
end
if(@TenderTypeId != 0 and @TenderTypeId = 112  )
begin 
Set @query = @query + ' and (tender.tendertypeid = 9  or tender.TenderTypeId =  1)';
end 

Set @query = @query + ' ORDER BY SubmitionDate';
 PRINT(@query)
INSERT into #tempTenders execute ( @query)  
 --SET @totalrow = (SELECT COUNT(*) FROM  #tempTenders)
 --SELECT * FROM #tempTenders;
  
 if    (@supplierTender = 0)
 begin
 if ((select count(*) from #areaTendersId) > 0 )
 begin 
 SET @totalrow = ( SELECT count( * ) FROM #tempTenders  tender 
  join #supplierINVTendersId inv on inv.tenderid = tender.tenderid
  inner join #areaTendersId area on area.tenderid = tender.tenderid
  left join  #supplierGeneralTable acceptedINV on acceptedINV.tenderid = tender.tenderid
  --left join  #supplierGeneralTable cp on cp.tenderid = tender.tenderid
  --left join  #supplierGeneralTable offer on offer.tenderid = tender.tenderid
  where  1=1 
   and acceptedINV.tenderid is null 
  --and  cp.tenderid is null 
  --and     offer.tenderid is null
  and tender.AgencyCode not in(select code from #blockedAgencesTable))
  SELECT * FROM #tempTenders  tender 
  join #supplierINVTendersId inv on inv.tenderid = tender.tenderid
  inner join #areaTendersId area on area.tenderid = tender.tenderid
  left join  #supplierGeneralTable acceptedINV on acceptedINV.tenderid = tender.tenderid
  --left join  #supplierGeneralTable cp on cp.tenderid = tender.tenderid
  --left join  #supplierGeneralTable offer on offer.tenderid = tender.tenderid
  where  1=1 
   and acceptedINV.tenderid is null 
  --and  cp.tenderid is null 
  --and     offer.tenderid is null
  and tender.AgencyCode not in(select code from #blockedAgencesTable)

  order by tender .SubmitionDate   OFFSET  @offset ROWS FETCH NEXT @page ROWS ONLY;
  end  
else
begin 

SET @totalrow = ( SELECT COUNT(*) FROM #tempTenders  tender 
  join #supplierINVTendersId inv on inv.tenderid = tender.tenderid 
  left join  #supplierGeneralTable acceptedINV on acceptedINV.tenderid = tender.tenderid
  --left join  #supplierGeneralTable cp on cp.tenderid = tender.tenderid
  --left join  #supplierGeneralTable offer on offer.tenderid = tender.tenderid
  where  1=1 
   and acceptedINV.tenderid is null 
  --and  cp.tenderid is null 
  --and     offer.tenderid is null
  and tender.AgencyCode not in(select code from #blockedAgencesTable)
  )

  SELECT * FROM #tempTenders  tender 
  join #supplierINVTendersId inv on inv.tenderid = tender.tenderid 
  left join  #supplierGeneralTable acceptedINV on acceptedINV.tenderid = tender.tenderid
  --left join  #supplierGeneralTable cp on cp.tenderid = tender.tenderid
  --left join  #supplierGeneralTable offer on offer.tenderid = tender.tenderid
  where  1=1 
   and acceptedINV.tenderid is null 
  --and  cp.tenderid is null 
  --and     offer.tenderid is null
  and tender.AgencyCode not in(select code from #blockedAgencesTable)

  order by tender .SubmitionDate   OFFSET  @offset ROWS FETCH NEXT @page ROWS ONLY;

  end 
  end 
  else
  begin    
--   select * from #supplierINVTendersId
--select * from #supplierGeneralTable
--select * from #supplierGeneralTable
--select * from #supplierGeneralTable 

   if ((select count(*) from #areaTendersId) > 0 )

   begin 
   
    SET @totalrow = ( SELECT count( * ) FROM #tempTenders  tender   inner join #areaTendersId area on area.tenderid = tender.tenderid
  join #supplierINVTendersId inv on inv.tenderid = tender.tenderid
  left join  #supplierGeneralTable acceptedINV on acceptedINV.tenderid = tender.tenderid
  --left join  #supplierGeneralTable cp on cp.tenderid = tender.tenderid
  --left join  #supplierGeneralTable offer on offer.tenderid = tender.tenderid
  where  1=1 
   and (acceptedINV.tenderid is  not  null 
  --or  cp.tenderid is  not  null 
  --or     offer.tenderid is not null
  )and tender.AgencyCode not in(select code from #blockedAgencesTable)) 
   SELECT * FROM #tempTenders  tender 
	  inner join #areaTendersId area on area.tenderid = tender.tenderid
  join #supplierINVTendersId inv on inv.tenderid = tender.tenderid
  left join  #supplierGeneralTable acceptedINV on acceptedINV.tenderid = tender.tenderid
  --left join  #supplierGeneralTable cp on cp.tenderid = tender.tenderid
  --left join  #supplierGeneralTable offer on offer.tenderid = tender.tenderid
  where  1=1 
   and (acceptedINV.tenderid is  not  null 
  --or  cp.tenderid is  not  null 
  --or     offer.tenderid is not null
  )and tender.AgencyCode not in(select code from #blockedAgencesTable)
  order by tender .SubmitionDate   OFFSET  @offset ROWS FETCH NEXT @page ROWS ONLY;
  end 
  else
  begin SET @totalrow = ( SELECT count( * ) FROM #tempTenders  tender    join #supplierINVTendersId inv on inv.tenderid = tender.tenderid
  left join  #supplierGeneralTable acceptedINV on acceptedINV.tenderid = tender.tenderid
  --left join  #supplierGeneralTable cp on cp.tenderid = tender.tenderid
  --left join  #supplierGeneralTable offer on offer.tenderid = tender.tenderid
  where  1=1 
   and (acceptedINV.tenderid is  not  null 
  --or  cp.tenderid is  not  null 
  --or     offer.tenderid is not null
  )and tender.AgencyCode not in(select code from #blockedAgencesTable))
  SELECT * FROM #tempTenders  tender 
	   join #supplierINVTendersId inv on inv.tenderid = tender.tenderid
  left join  #supplierGeneralTable acceptedINV on acceptedINV.tenderid = tender.tenderid
  --left join  #supplierGeneralTable cp on cp.tenderid = tender.tenderid
  --left join  #supplierGeneralTable offer on offer.tenderid = tender.tenderid
  where  1=1 
   and (acceptedINV.tenderid is  not  null 
  --or  cp.tenderid is  not  null 
  --or     offer.tenderid is not null
  )and tender.AgencyCode not in(select code from #blockedAgencesTable)
  order by tender .SubmitionDate   OFFSET  @offset ROWS FETCH NEXT @page ROWS ONLY;
 end 
  end 
  drop table #supplierGeneralTableAll;
  drop table #supplierGeneralTable;
  drop table #supplierINVTendersId; 
  drop table #tempTenders; 
  drop table #areaTendersId;
 /*
 declare  @TOTAL  int
  exec SelectAllTendersForSupplier
   '1010000154', --@Cr
   0, --@TenderTypeId
  '2020-01-09 12:55:27.6480465' , --@PublishDate 
  '01/01/0001',  -- From Presentation Date
  '01/01/2222', -- To Presentation Date 
   '', --@AgencyCode
   '',--@ReferenceNumber
null, --Tender Name 
null,--@BookletRang
  '',--@TenderAreasIds
  '' ,--@blockedAgences
  null,--@TenderActivityId
  null,--@TenderSubActivityId

  null,--@TenderCategoryId
  1--@supplierTender
  ,100,0,'',@TOTAL output


  select @TOTAL
*/