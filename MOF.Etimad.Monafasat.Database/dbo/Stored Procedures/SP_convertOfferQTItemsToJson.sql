
Create PROCEDURE [dbo].[SP_convertOfferQTItemsToJson]
							(
								@TableId [bigint],
								@OfferId [BigInt],
								@items OfferQuantityItems READONLY
							)
							AS
							DECLARE @json NVARCHAR(MAX)
							SET @json = (SELECT [Id]
								,[ColumnId]
								,[TenderFormHeaderId]
								,[ActivityTemplateId]
								,AlternativeValue
								,[Value]
								,[ItemNumber]
								,IsDefault
							FROM @items
							FOR JSON AUTO)
							DECLARE @QuantityId [bigint]
							SET @QuantityId = (select Id from offer.SupplierTenderQuantityTable where TenederQuantityId = @TableId and OfferId = @OfferId)
							BEGIN
							INSERT INTO [Offer].[SupplierTenderQuantityTableItemJson] (SupplierTenderQuantityTableItems, SupplierTenderQuantityTableId) VALUES (@json, @QuantityId)

							SELECT id as ItemsJsonId from [Offer].[SupplierTenderQuantityTableItemJson] where SupplierTenderQuantityTableId = @QuantityId
							END

