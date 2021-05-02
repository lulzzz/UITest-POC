using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class SP_DeleteTenderQuantityTableWithItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE SP_DeleteTenderQuantityTableWithItems @TenderId int

                    AS
						declare @result bit

                    BEGIN 
	                    delete from Tender.TenderQuantitiyItemsJson where TenderQuantityTableId in
	                                (select Id from Tender.TenderQuantityTable where TenderId = @TenderId)
	  
	                    delete from Tender.TenderQuantityTable  where TenderId = @TenderId
                    set @result = 1
					select @result as IsDeleted
                    END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE SP_DeleteTenderQuantityTableWithItems
                            GO";

            migrationBuilder.Sql(sp);
        }
    }
}
