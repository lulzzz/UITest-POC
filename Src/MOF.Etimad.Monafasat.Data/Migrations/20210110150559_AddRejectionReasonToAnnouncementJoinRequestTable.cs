using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddRejectionReasonToAnnouncementJoinRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementJoinRequestSupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementJoinRequestSupplierTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementJoinRequestSupplierTemplate_Supplier_Cr",
                schema: "AnnouncementTemplate",
                table: "AnnouncementJoinRequestSupplierTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementJoinRequestSupplierTemplate_AnnouncementJoinRequestStatusSupplierTemplate_StatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementJoinRequestSupplierTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementTemplateJoinRequestAttachment_AnnouncementJoinRequestSupplierTemplate_JoinRequestSupplierTemplateId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnnouncementJoinRequestSupplierTemplate",
                schema: "AnnouncementTemplate",
                table: "AnnouncementJoinRequestSupplierTemplate");

            migrationBuilder.RenameTable(
                name: "AnnouncementJoinRequestSupplierTemplate",
                schema: "AnnouncementTemplate",
                newName: "AnnouncementRequestSupplierTemplate",
                newSchema: "AnnouncementTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_AnnouncementJoinRequestSupplierTemplate_StatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate",
                newName: "IX_AnnouncementRequestSupplierTemplate_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_AnnouncementJoinRequestSupplierTemplate_Cr",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate",
                newName: "IX_AnnouncementRequestSupplierTemplate_Cr");

            migrationBuilder.RenameIndex(
                name: "IX_AnnouncementJoinRequestSupplierTemplate_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate",
                newName: "IX_AnnouncementRequestSupplierTemplate_AnnouncementId");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnnouncementRequestSupplierTemplate",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementRequestSupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate",
                column: "AnnouncementId",
                principalSchema: "AnnouncementTemplate",
                principalTable: "AnnouncementSupplierTemplate",
                principalColumn: "AnnouncementId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementRequestSupplierTemplate_Supplier_Cr",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate",
                column: "Cr",
                principalSchema: "IDM",
                principalTable: "Supplier",
                principalColumn: "SelectedCr",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementRequestSupplierTemplate_AnnouncementJoinRequestStatusSupplierTemplate_StatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate",
                column: "StatusId",
                principalSchema: "LookUps",
                principalTable: "AnnouncementJoinRequestStatusSupplierTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementRequestSupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementRequestSupplierTemplate_Supplier_Cr",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementRequestSupplierTemplate_AnnouncementJoinRequestStatusSupplierTemplate_StatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementTemplateJoinRequestAttachment_AnnouncementRequestSupplierTemplate_JoinRequestSupplierTemplateId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnnouncementRequestSupplierTemplate",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate");

            migrationBuilder.RenameTable(
                name: "AnnouncementRequestSupplierTemplate",
                schema: "AnnouncementTemplate",
                newName: "AnnouncementJoinRequestSupplierTemplate",
                newSchema: "AnnouncementTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_AnnouncementRequestSupplierTemplate_StatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementJoinRequestSupplierTemplate",
                newName: "IX_AnnouncementJoinRequestSupplierTemplate_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_AnnouncementRequestSupplierTemplate_Cr",
                schema: "AnnouncementTemplate",
                table: "AnnouncementJoinRequestSupplierTemplate",
                newName: "IX_AnnouncementJoinRequestSupplierTemplate_Cr");

            migrationBuilder.RenameIndex(
                name: "IX_AnnouncementRequestSupplierTemplate_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementJoinRequestSupplierTemplate",
                newName: "IX_AnnouncementJoinRequestSupplierTemplate_AnnouncementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnnouncementJoinRequestSupplierTemplate",
                schema: "AnnouncementTemplate",
                table: "AnnouncementJoinRequestSupplierTemplate",
                column: "Id");


            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementJoinRequestSupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementJoinRequestSupplierTemplate",
                column: "AnnouncementId",
                principalSchema: "AnnouncementTemplate",
                principalTable: "AnnouncementSupplierTemplate",
                principalColumn: "AnnouncementId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementJoinRequestSupplierTemplate_Supplier_Cr",
                schema: "AnnouncementTemplate",
                table: "AnnouncementJoinRequestSupplierTemplate",
                column: "Cr",
                principalSchema: "IDM",
                principalTable: "Supplier",
                principalColumn: "SelectedCr",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementJoinRequestSupplierTemplate_AnnouncementJoinRequestStatusSupplierTemplate_StatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementJoinRequestSupplierTemplate",
                column: "StatusId",
                principalSchema: "LookUps",
                principalTable: "AnnouncementJoinRequestStatusSupplierTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementTemplateJoinRequestAttachment_AnnouncementJoinRequestSupplierTemplate_JoinRequestSupplierTemplateId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestAttachment",
                column: "JoinRequestSupplierTemplateId",
                principalSchema: "AnnouncementTemplate",
                principalTable: "AnnouncementJoinRequestSupplierTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
