using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddLinkedAgenciesAnnouncementTemplateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "LinkedAgenciesAnnouncementTemplate",
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
                    AgencyCode = table.Column<string>(maxLength: 20, nullable: true),
                    AnnouncementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedAgenciesAnnouncementTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkedAgenciesAnnouncementTemplate_GovAgency_AgencyCode",
                        column: x => x.AgencyCode,
                        principalSchema: "IDM",
                        principalTable: "GovAgency",
                        principalColumn: "AgencyCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkedAgenciesAnnouncementTemplate_AnnouncementSupplierTemplate_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "AnnouncementTemplate",
                        principalTable: "AnnouncementSupplierTemplate",
                        principalColumn: "AnnouncementId",
                        onDelete: ReferentialAction.Restrict);
                });


            migrationBuilder.CreateIndex(
                name: "IX_LinkedAgenciesAnnouncementTemplate_AgencyCode",
                schema: "AnnouncementTemplate",
                table: "LinkedAgenciesAnnouncementTemplate",
                column: "AgencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_LinkedAgenciesAnnouncementTemplate_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "LinkedAgenciesAnnouncementTemplate",
                column: "AnnouncementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkedAgenciesAnnouncementTemplate",
                schema: "AnnouncementTemplate");

        }
    }
}
