using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class CreateSPQTCellCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProc = @"Create Proc SP_QuantityTableCellCount
@TableId bigint 

as
begin

SELECT top 1 cast(qt.SupplierTenderQuantityTableId as bigint) as TableId,CAST(count(*) as bigint)  as ItemCount,CAST( ItemNumber as bigint) as ItemNumber
 FROM Offer.SupplierTenderQuantityTableItemJson qt
CROSS APPLY OPENJSON(qt.SupplierTenderQuantityTableItems) WITH(ItemNumber nvarchar(30))
where qt.SupplierTenderQuantityTableId = @TableId
group by qt.SupplierTenderQuantityTableId,ItemNumber

end

";
            migrationBuilder.Sql(createProc);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropProc = @"drop proc SP_QuantityTableCellCount";
            migrationBuilder.Sql(dropProc);
        }
    }
}
