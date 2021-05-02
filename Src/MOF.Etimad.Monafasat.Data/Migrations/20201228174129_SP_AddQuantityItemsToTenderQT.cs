using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class SP_AddQuantityItemsToTenderQT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var script = @"CREATE TYPE [dbo].[TenderQuantityItem] AS TABLE(
	                        [Id] [bigint] NULL,
	                        [ColumnId] [bigint] NULL,
	                        [TenderFormHeaderId] [bigint] NULL,
	                        [ActivityTemplateId] [int] NULL,
	                        [ItemNumber] [bigint] NULL,
	                        [Value] [nvarchar](max) NULL,
	                        [ColumnName] [nvarchar](max) NULL
                        )

                        GO

                        Create PROCEDURE [dbo].[SP_AddQuantityItemsToTenderQT]
							(
								@TableId [bigint],
								@newItems [TenderQuantityItem] READONLY
							)
							AS
							DECLARE @items table ([Id] [bigint] NULL, [ColumnId] [bigint] NULL, [TenderFormHeaderId] [bigint] NULL, [ActivityTemplateId] [int] NULL, [ItemNumber] [bigint] NULL, [Value] [nvarchar](max) NULL, [ColumnName] [nvarchar](max) NULL)
							insert into @items select [Id]
										,[ColumnId]
										,[TenderFormHeaderId]
										,[ActivityTemplateId]
										,[Value]
										,[ItemNumber]
										,[ColumnName]
									from [Tender].[VW_TendersQTJsonItems]
									where QTableId = @TableId
							insert into @items 
								SELECT [Id]
									,[ColumnId]
									,[TenderFormHeaderId]
									,[ActivityTemplateId]
									,[Value]
									,[ItemNumber]
									,[ColumnName]
								FROM @newItems
							
							DECLARE @json NVARCHAR(MAX)
							SET @json = (SELECT [Id]
								,[ColumnId]
								,[TenderFormHeaderId]
								,[ActivityTemplateId]
								,[Value]
								,[ItemNumber]
								,[ColumnName]
							FROM @items
							FOR JSON AUTO)
							BEGIN
							INSERT INTO [Tender].[TenderQuantitiyItemsJson] (TenderQuantityTableItems, TenderQuantityTableId) VALUES (@json, @TableId)

							SELECT id as ItemsJsonId from [Tender].[TenderQuantitiyItemsJson]  where TenderQuantityTableId = @TableId
							END

                        GO";

            migrationBuilder.Sql(script);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var script = @"DROP PROCEDURE [dbo].[SP_AddQuantityItemsToTenderQT]
                            GO

                            DROP TYPE [dbo].[TenderQuantityItem]
                            GO";
            migrationBuilder.Sql(script);
        }
    }
}
