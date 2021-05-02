using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class InitAnnouncement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Announcement");

            migrationBuilder.CreateTable(
                name: "AnnouncementStatus",
                schema: "Announcement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Announcement",
                schema: "Announcement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    AnnouncementName = table.Column<string>(maxLength: 1024, nullable: false),
                    IntroAboutTender = table.Column<string>(maxLength: 5000, nullable: false),
                    AnnouncementPeriod = table.Column<int>(nullable: false),
                    InsidKsa = table.Column<bool>(nullable: false),
                    Details = table.Column<string>(maxLength: 5000, nullable: true),
                    ActivityDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcement_AnnouncementStatus_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "Announcement",
                        principalTable: "AnnouncementStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementActivity",
                schema: "Announcement",
                columns: table => new
                {
                    TenderActivityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    ActivityId = table.Column<int>(nullable: false),
                    TenderId = table.Column<int>(nullable: false),
                    AnnouncementId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementActivity", x => x.TenderActivityId);
                    table.ForeignKey(
                        name: "FK_AnnouncementActivity_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalSchema: "LookUps",
                        principalTable: "Activity",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementActivity_Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "Announcement",
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementActivity_Tender_TenderId",
                        column: x => x.TenderId,
                        principalSchema: "Tender",
                        principalTable: "Tender",
                        principalColumn: "TenderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementArea",
                schema: "Announcement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    AreaId = table.Column<int>(nullable: false),
                    TenderId = table.Column<int>(nullable: false),
                    AnnouncementId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementArea_Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "Announcement",
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementArea_Area_AreaId",
                        column: x => x.AreaId,
                        principalSchema: "LookUps",
                        principalTable: "Area",
                        principalColumn: "AreaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementArea_Tender_TenderId",
                        column: x => x.TenderId,
                        principalSchema: "Tender",
                        principalTable: "Tender",
                        principalColumn: "TenderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementConstructionWork",
                schema: "Announcement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    ConstructionWorkId = table.Column<int>(nullable: false),
                    AnnouncementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementConstructionWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementConstructionWork_Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "Announcement",
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementConstructionWork_ConstructionWork_ConstructionWorkId",
                        column: x => x.ConstructionWorkId,
                        principalSchema: "LookUps",
                        principalTable: "ConstructionWork",
                        principalColumn: "ConstructionWorkId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementCountry",
                schema: "Announcement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    CountryId = table.Column<int>(nullable: false),
                    TenderId = table.Column<int>(nullable: false),
                    AnnouncementId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementCountry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementCountry_Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "Announcement",
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementCountry_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "LookUps",
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementCountry_Tender_TenderId",
                        column: x => x.TenderId,
                        principalSchema: "Tender",
                        principalTable: "Tender",
                        principalColumn: "TenderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementHistory",
                schema: "Announcement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    RejectionReason = table.Column<string>(maxLength: 2000, nullable: true),
                    AnnouncementId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementHistory_Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "Announcement",
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementHistory_AnnouncementStatus_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "Announcement",
                        principalTable: "AnnouncementStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementMaintenanceRunnigWork",
                schema: "Announcement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    MaintenanceRunningWorkId = table.Column<int>(nullable: false),
                    AnnouncementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementMaintenanceRunnigWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementMaintenanceRunnigWork_Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "Announcement",
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementMaintenanceRunnigWork_MaintenanceRunningWork_MaintenanceRunningWorkId",
                        column: x => x.MaintenanceRunningWorkId,
                        principalSchema: "LookUps",
                        principalTable: "MaintenanceRunningWork",
                        principalColumn: "MaintenanceRunningWorkId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_StatusId",
                schema: "Announcement",
                table: "Announcement",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementActivity_ActivityId",
                schema: "Announcement",
                table: "AnnouncementActivity",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementActivity_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementActivity",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementActivity_TenderId",
                schema: "Announcement",
                table: "AnnouncementActivity",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementArea_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementArea",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementArea_AreaId",
                schema: "Announcement",
                table: "AnnouncementArea",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementArea_TenderId",
                schema: "Announcement",
                table: "AnnouncementArea",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementConstructionWork_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementConstructionWork",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementConstructionWork_ConstructionWorkId",
                schema: "Announcement",
                table: "AnnouncementConstructionWork",
                column: "ConstructionWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementCountry_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementCountry",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementCountry_CountryId",
                schema: "Announcement",
                table: "AnnouncementCountry",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementCountry_TenderId",
                schema: "Announcement",
                table: "AnnouncementCountry",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementHistory_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementHistory",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementHistory_StatusId",
                schema: "Announcement",
                table: "AnnouncementHistory",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementMaintenanceRunnigWork_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementMaintenanceRunnigWork_MaintenanceRunningWorkId",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                column: "MaintenanceRunningWorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementActivity",
                schema: "Announcement");

            migrationBuilder.DropTable(
                name: "AnnouncementArea",
                schema: "Announcement");

            migrationBuilder.DropTable(
                name: "AnnouncementConstructionWork",
                schema: "Announcement");

            migrationBuilder.DropTable(
                name: "AnnouncementCountry",
                schema: "Announcement");

            migrationBuilder.DropTable(
                name: "AnnouncementHistory",
                schema: "Announcement");

            migrationBuilder.DropTable(
                name: "AnnouncementMaintenanceRunnigWork",
                schema: "Announcement");

            migrationBuilder.DropTable(
                name: "Announcement",
                schema: "Announcement");

            migrationBuilder.DropTable(
                name: "AnnouncementStatus",
                schema: "Announcement");

        }
    }
}
