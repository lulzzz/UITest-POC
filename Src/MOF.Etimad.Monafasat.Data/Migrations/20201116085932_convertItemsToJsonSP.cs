using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class convertItemsToJsonSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var script = @"CREATE TYPE [dbo].[OfferQuantityItems] AS TABLE(
							[Id] [bigint] NULL,
							[ColumnId] [bigint] NULL,
							[TenderFormHeaderId] [bigint] NULL,
							[ActivityTemplateId] [int] NULL,
							[ItemNumber] [bigint] NULL,
							[Value] [nvarchar](max) NULL,
							[AlternativeValue] [nvarchar](max) NULL,
							[IsDefault] [bit] NULL
							)
							GO


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

							GO";
            migrationBuilder.Sql(script);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var script = @"DROP PROCEDURE [dbo].[SP_convertOfferQTItemsToJson]
                            GO

                            DROP TYPE [dbo].[OfferQuantityItems]
                            GO";
            migrationBuilder.Sql(script);
        }
    }
}
