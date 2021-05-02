using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class CreateStoredProcToGetTenderQuantityTableCellCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProc = @"Create Proc SP_TenderQuantityTableCellCount
@TableId bigint 

as
begin

select top 1 CAST(count(*) as bigint) as ItemCount

from Tender.TenderQuantitiyItemsJson qt 
CROSS APPLY OPENJSON(qt.TenderQuantityTableItems) WITH(ItemNumber nvarchar(30))
where qt.TenderQuantityTableId = @TableId
group by qt.TenderQuantityTableId,ItemNumber

end";
            migrationBuilder.Sql(createProc);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropProc = @"drop proc SP_TenderQuantityTableCellCount";
            migrationBuilder.Sql(dropProc);
        }
    }
}
