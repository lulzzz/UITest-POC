using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class relatioinattachement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementTemplateJoinRequestAttachment_AnnouncementRequestSupplierTemplate_JoinRequestSupplierTemplateId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment");

            migrationBuilder.DropIndex(
                name: "IX_AnnouncementTemplateJoinRequestAttachment_JoinRequestSupplierTemplateId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment");

            migrationBuilder.DropColumn(
                name: "JoinRequestSupplierTemplateId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment");


            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementTemplateJoinRequestAttachment_JoinRequestId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                column: "JoinRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementTemplateJoinRequestAttachment_AnnouncementRequestSupplierTemplate_JoinRequestId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                column: "JoinRequestId",
                principalSchema: "AnnouncementTemplate",
                principalTable: "AnnouncementRequestSupplierTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementTemplateJoinRequestAttachment_AnnouncementRequestSupplierTemplate_JoinRequestId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment");

            migrationBuilder.DropIndex(
                name: "IX_AnnouncementTemplateJoinRequestAttachment_JoinRequestId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment");

            migrationBuilder.AddColumn<int>(
                name: "JoinRequestSupplierTemplateId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementTemplateJoinRequestAttachment_JoinRequestSupplierTemplateId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                column: "JoinRequestSupplierTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementTemplateJoinRequestAttachment_AnnouncementRequestSupplierTemplate_JoinRequestSupplierTemplateId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                column: "JoinRequestSupplierTemplateId",
                principalSchema: "AnnouncementTemplate",
                principalTable: "AnnouncementRequestSupplierTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
