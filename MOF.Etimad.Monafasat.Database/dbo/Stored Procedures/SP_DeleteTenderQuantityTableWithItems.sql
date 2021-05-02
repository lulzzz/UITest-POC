		  CREATE PROCEDURE [dbo].[SP_DeleteTenderQuantityTableWithItems] @TenderId int

                    AS
						declare @result bit

                    BEGIN 
					
				 		 
					-- delete from tender.TenderQuantitiyItemsChangeJson where TenderQuantityTableChangeId in
					--(  select  Id from tender.TenderQuantityTableChanges where TenderQuantitiesTableId  in 
					--  (select Id from Tender.TenderQuantityTable where TenderId = @TenderId));

     --                  delete from tender.TenderQuantityTableChanges where TenderQuantitiesTableId  in 
					--  (select Id from Tender.TenderQuantityTable where TenderId = @TenderId);

	                    delete from Tender.TenderQuantitiyItemsJson where TenderQuantityTableId in
	                                (select Id from Tender.TenderQuantityTable where TenderId = @TenderId)
	  
	                    delete from Tender.TenderQuantityTable  where TenderId = @TenderId
						 
                    set @result = 1

					select @result as IsDeleted
					  
					END
