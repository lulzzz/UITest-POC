using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class addNewTenderStatusBackForAwardingFromPlaint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "TenderStatus",
                columns: new[] { "TenderStatusId", "NameAr", "NameEn" },
                values: new object[] { 94, "معادة للترسية بسبب قبول طلب التظلم", null });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "TenderStatus",
                keyColumn: "TenderStatusId",
                keyValue: 94);


        }
    }
}
