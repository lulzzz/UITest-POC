
 Create Proc SP_QuantityTableCellCount
@TableId bigint 

as
begin

SELECT top 1 cast(qt.SupplierTenderQuantityTableId as bigint) as TableId,CAST(count(*) as bigint)  as ItemCount,CAST( ItemNumber as bigint) as ItemNumber
 FROM Offer.SupplierTenderQuantityTableItemJson qt
CROSS APPLY OPENJSON(qt.SupplierTenderQuantityTableItems) WITH(ItemNumber nvarchar(30))
where qt.SupplierTenderQuantityTableId = @TableId
group by qt.SupplierTenderQuantityTableId,ItemNumber

end
