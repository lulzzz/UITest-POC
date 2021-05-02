using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddcreatedbyIdToAnnouncementTemplateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementSupplierTemplate_ReasonForPurchaseTenderType_ReasonForSelectingTenderTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementSupplierTemplate_TenderType_TenderTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.DropIndex(
                name: "IX_AnnouncementSupplierTemplate_ReasonForSelectingTenderTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.DropIndex(
                name: "IX_AnnouncementSupplierTemplate_TenderTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.DropColumn(
                name: "ReasonForSelectingTenderTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.DropColumn(
                name: "TenderTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenderTypesId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.DropColumn(
                name: "TenderTypesId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.AddColumn<int>(
                name: "ReasonForSelectingTenderTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenderTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "int",
                nullable: false,
                defaultValue: 0);


            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementSupplierTemplate_ReasonForSelectingTenderTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                column: "ReasonForSelectingTenderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementSupplierTemplate_TenderTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                column: "TenderTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementSupplierTemplate_ReasonForPurchaseTenderType_ReasonForSelectingTenderTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                column: "ReasonForSelectingTenderTypeId",
                principalSchema: "Lookups",
                principalTable: "ReasonForPurchaseTenderType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementSupplierTemplate_TenderType_TenderTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                column: "TenderTypeId",
                principalSchema: "LookUps",
                principalTable: "TenderType",
                principalColumn: "TenderTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
