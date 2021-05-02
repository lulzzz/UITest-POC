using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddAnnouncementTemplateIdInTenderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnnouncementTemplateId",
                schema: "Tender",
                table: "Tender",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tender_AnnouncementTemplateId",
                schema: "Tender",
                table: "Tender",
                column: "AnnouncementTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tender_AnnouncementSupplierTemplate_AnnouncementTemplateId",
                schema: "Tender",
                table: "Tender",
                column: "AnnouncementTemplateId",
                principalSchema: "AnnouncementTemplate",
                principalTable: "AnnouncementSupplierTemplate",
                principalColumn: "AnnouncementId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tender_AnnouncementSupplierTemplate_AnnouncementTemplateId",
                schema: "Tender",
                table: "Tender");

            migrationBuilder.DropIndex(
                name: "IX_Tender_AnnouncementTemplateId",
                schema: "Tender",
                table: "Tender");

            migrationBuilder.DropColumn(
                name: "AnnouncementTemplateId",
                schema: "Tender",
                table: "Tender");

        }
    }
}
