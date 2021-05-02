
 -- Cell Count For Tender Quantity Table  
Create Proc SP_TenderQuantityTableCellCount
@TableId bigint 

as
begin

select top 1 CAST(count(*) as bigint) as ItemCount

from Tender.TenderQuantitiyItemsJson qt 
CROSS APPLY OPENJSON(qt.TenderQuantityTableItems) WITH(ItemNumber nvarchar(30))
where qt.TenderQuantityTableId = @TableId
group by qt.TenderQuantityTableId,ItemNumber

end 
