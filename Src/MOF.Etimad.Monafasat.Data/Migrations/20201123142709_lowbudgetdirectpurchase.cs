using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class lowbudgetdirectpurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DirectPurchaseMemberId",
                schema: "Tender",
                table: "Tender",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLowBudgetDirectPurchase",
                schema: "Tender",
                table: "Tender",
                nullable: true);


            migrationBuilder.CreateIndex(
                name: "IX_Tender_DirectPurchaseMemberId",
                schema: "Tender",
                table: "Tender",
                column: "DirectPurchaseMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tender_UserProfile_DirectPurchaseMemberId",
                schema: "Tender",
                table: "Tender",
                column: "DirectPurchaseMemberId",
                principalSchema: "IDM",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tender_UserProfile_DirectPurchaseMemberId",
                schema: "Tender",
                table: "Tender");

            migrationBuilder.DropIndex(
                name: "IX_Tender_DirectPurchaseMemberId",
                schema: "Tender",
                table: "Tender");

            migrationBuilder.DropColumn(
                name: "DirectPurchaseMemberId",
                schema: "Tender",
                table: "Tender");

            migrationBuilder.DropColumn(
                name: "IsLowBudgetDirectPurchase",
                schema: "Tender",
                table: "Tender");
        }
    }
}
