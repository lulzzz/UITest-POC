using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddAnnouncementJoinRequestAttchmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnouncementTemplateJoinRequestAttachment",
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
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    FileNetReferenceId = table.Column<string>(maxLength: 1024, nullable: true),
                    JoinRequestId = table.Column<int>(nullable: false),
                    JoinRequestSupplierTemplateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementTemplateJoinRequestAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementTemplateJoinRequestAttachment_AnnouncementRequestSupplierTemplate_JoinRequestSupplierTemplateId",
                        column: x => x.JoinRequestSupplierTemplateId,
                        principalSchema: "AnnouncementTemplate",
                        principalTable: "AnnouncementRequestSupplierTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });


        }
    }
}
