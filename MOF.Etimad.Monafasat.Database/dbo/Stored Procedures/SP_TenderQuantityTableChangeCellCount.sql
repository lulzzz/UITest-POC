

--Cell Count For quantity table changes
Create Proc SP_TenderQuantityTableChangeCellCount
@TableId bigint 

as
begin 

select top 1 CAST(count(*) as bigint) as ItemCount

from Tender.TenderQuantitiyItemsChangeJson qt 
CROSS APPLY OPENJSON(qt.TenderQuantityTableItemChanges) WITH(ItemNumber nvarchar(30))
where qt.TenderQuantityTableChangeId = @TableId
group by qt.TenderQuantityTableChangeId, ItemNumber

end

