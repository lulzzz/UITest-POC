using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class CreateProcedureToGetTenderQuantityTableChangCellCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProc = @"Create Proc SP_TenderQuantityTableChangeCellCount
@TableId bigint 

as
begin 

select top 1 CAST(count(*) as bigint) as ItemCount

from Tender.TenderQuantitiyItemsChangeJson qt 
CROSS APPLY OPENJSON(qt.TenderQuantityTableItemChanges) WITH(ItemNumber nvarchar(30))
where qt.TenderQuantityTableChangeId = @TableId
group by qt.TenderQuantityTableChangeId, ItemNumber

end";
            migrationBuilder.Sql(createProc);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropProc = @"drop proc SP_TenderQuantityTableChangeCellCount";
            migrationBuilder.Sql(dropProc);
        }
    }
}
