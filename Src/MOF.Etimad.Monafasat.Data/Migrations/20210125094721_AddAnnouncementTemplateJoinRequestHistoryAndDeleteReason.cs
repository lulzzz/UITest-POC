using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddAnnouncementTemplateJoinRequestHistoryAndDeleteReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnouncementTemplateJoinRequestHistory",
                schema: "AnnouncementTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    RejectionReason = table.Column<string>(maxLength: 2000, nullable: true),
                    DeleteReason = table.Column<string>(maxLength: 2000, nullable: true),
                    JoinRequestId = table.Column<int>(nullable: false),
                    JoinRequestStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementTemplateJoinRequestHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementTemplateJoinRequestHistory_AnnouncementRequestSupplierTemplate_JoinRequestId",
                        column: x => x.JoinRequestId,
                        principalSchema: "AnnouncementTemplate",
                        principalTable: "AnnouncementRequestSupplierTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementTemplateJoinRequestHistory_AnnouncementJoinRequestStatusSupplierTemplate_JoinRequestStatusId",
                        column: x => x.JoinRequestStatusId,
                        principalSchema: "LookUps",
                        principalTable: "AnnouncementJoinRequestStatusSupplierTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });


            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementTemplateJoinRequestHistory_JoinRequestId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestHistory",
                column: "JoinRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementTemplateJoinRequestHistory_JoinRequestStatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementTemplateJoinRequestHistory",
                column: "JoinRequestStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementTemplateJoinRequestHistory",
                schema: "AnnouncementTemplate");

        }
    }
}
