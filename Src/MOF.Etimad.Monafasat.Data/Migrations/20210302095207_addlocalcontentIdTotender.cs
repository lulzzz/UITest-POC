using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class addlocalcontentIdTotender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<int>(
                name: "TenderLocalContentId",
                schema: "Tender",
                table: "Tender",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "TenderConditoinsStatus",
                columns: new[] { "TenderConditoinsStatusId", "NameAr", "NameEn" },
                values: new object[] { 8, "المحتوى المحلي", null });


            migrationBuilder.CreateIndex(
                name: "IX_Tender_TenderLocalContentId",
                schema: "Tender",
                table: "Tender",
                column: "TenderLocalContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tender_TenderLocalContent_TenderLocalContentId",
                schema: "Tender",
                table: "Tender",
                column: "TenderLocalContentId",
                principalSchema: "Tender",
                principalTable: "TenderLocalContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tender_TenderLocalContent_TenderLocalContentId",
                schema: "Tender",
                table: "Tender");


            migrationBuilder.DropIndex(
                name: "IX_Tender_TenderLocalContentId",
                schema: "Tender",
                table: "Tender");

            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "TenderConditoinsStatus",
                keyColumn: "TenderConditoinsStatusId",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "TenderLocalContentId",
                schema: "Tender",
                table: "Tender");




        }
    }
}
