using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class CreateAnnouncementSupplierListTemplateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AnnouncementTemplate");

            migrationBuilder.CreateTable(
                name: "AnnouncementStatusSupplierTemplate",
                schema: "AnnouncementTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementStatusSupplierTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementJoinRequestStatusSupplierTemplate",
                schema: "LookUps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    NameEn = table.Column<string>(maxLength: 1024, nullable: true),
                    NameAr = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementJoinRequestStatusSupplierTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementSupplierTemplate",
                schema: "AnnouncementTemplate",
                columns: table => new
                {
                    AnnouncementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    AnnouncementName = table.Column<string>(maxLength: 1024, nullable: true),
                    AnnouncementListType = table.Column<int>(nullable: false),
                    IntroAboutAnnouncementTemplate = table.Column<string>(maxLength: 5000, nullable: true),
                    InsidKsa = table.Column<bool>(nullable: false),
                    Descriptions = table.Column<string>(maxLength: 5000, nullable: true),
                    ActivityDescription = table.Column<string>(maxLength: 2000, nullable: true),
                    PublishedDate = table.Column<DateTime>(nullable: true),
                    TenderTypeId = table.Column<int>(nullable: false),
                    ReasonForSelectingTenderTypeId = table.Column<int>(nullable: false),
                    AgencyCode = table.Column<string>(maxLength: 20, nullable: true),
                    BranchId = table.Column<int>(nullable: true),
                    ApprovedBy = table.Column<string>(maxLength: 200, nullable: true),
                    ReferenceNumber = table.Column<string>(maxLength: 100, nullable: true),
                    IsEffectiveList = table.Column<bool>(nullable: true),
                    EffectiveListDate = table.Column<DateTime>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    RequirementConditionsToJoinList = table.Column<string>(nullable: true),
                    AgencyName = table.Column<string>(nullable: true),
                    AgencyAddress = table.Column<string>(nullable: true),
                    AgencyPhone = table.Column<string>(nullable: true),
                    AgencyFax = table.Column<string>(nullable: true),
                    AgencyEmail = table.Column<string>(nullable: true),
                    IsRequiredAttachmentToJoinList = table.Column<bool>(nullable: false),
                    RequiredAttachment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementSupplierTemplate", x => x.AnnouncementId);
                    table.ForeignKey(
                        name: "FK_AnnouncementSupplierTemplate_GovAgency_AgencyCode",
                        column: x => x.AgencyCode,
                        principalSchema: "IDM",
                        principalTable: "GovAgency",
                        principalColumn: "AgencyCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementSupplierTemplate_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Branch",
                        principalTable: "Branch",
                        principalColumn: "BranchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementSupplierTemplate_ReasonForPurchaseTenderType_ReasonForSelectingTenderTypeId",
                        column: x => x.ReasonForSelectingTenderTypeId,
                        principalSchema: "Lookups",
                        principalTable: "ReasonForPurchaseTenderType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementSupplierTemplate_AnnouncementStatusSupplierTemplate_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "AnnouncementTemplate",
                        principalTable: "AnnouncementStatusSupplierTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementSupplierTemplate_TenderType_TenderTypeId",
                        column: x => x.TenderTypeId,
                        principalSchema: "LookUps",
                        principalTable: "TenderType",
                        principalColumn: "TenderTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementActivitySupplierTemplate",
                schema: "AnnouncementTemplate",
                columns: table => new
                {
                    AnnouncementActivityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    ActivityId = table.Column<int>(nullable: false),
                    AnnouncementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementActivitySupplierTemplate", x => x.AnnouncementActivityId);
                    table.ForeignKey(
                        name: "FK_AnnouncementActivitySupplierTemplate_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalSchema: "LookUps",
                        principalTable: "Activity",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementActivitySupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "AnnouncementTemplate",
                        principalTable: "AnnouncementSupplierTemplate",
                        principalColumn: "AnnouncementId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementAreaSupplierTemplate",
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
                    AreaId = table.Column<int>(nullable: false),
                    AnnouncementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementAreaSupplierTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementAreaSupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "AnnouncementTemplate",
                        principalTable: "AnnouncementSupplierTemplate",
                        principalColumn: "AnnouncementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementAreaSupplierTemplate_Area_AreaId",
                        column: x => x.AreaId,
                        principalSchema: "LookUps",
                        principalTable: "Area",
                        principalColumn: "AreaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementConstructionWorkSupplierTemplate",
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
                    ConstructionWorkId = table.Column<int>(nullable: false),
                    AnnouncementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementConstructionWorkSupplierTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementConstructionWorkSupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "AnnouncementTemplate",
                        principalTable: "AnnouncementSupplierTemplate",
                        principalColumn: "AnnouncementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementConstructionWorkSupplierTemplate_ConstructionWork_ConstructionWorkId",
                        column: x => x.ConstructionWorkId,
                        principalSchema: "LookUps",
                        principalTable: "ConstructionWork",
                        principalColumn: "ConstructionWorkId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementCountrySupplierTemplate",
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
                    CountryId = table.Column<int>(nullable: false),
                    AnnouncementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementCountrySupplierTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementCountrySupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "AnnouncementTemplate",
                        principalTable: "AnnouncementSupplierTemplate",
                        principalColumn: "AnnouncementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementCountrySupplierTemplate_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "LookUps",
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementHistorySupplierTemplate",
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
                    StatusId = table.Column<int>(nullable: false),
                    RejectionReason = table.Column<string>(maxLength: 2000, nullable: true),
                    AnnouncementId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementHistorySupplierTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementHistorySupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "AnnouncementTemplate",
                        principalTable: "AnnouncementSupplierTemplate",
                        principalColumn: "AnnouncementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementHistorySupplierTemplate_AnnouncementStatusSupplierTemplate_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "AnnouncementTemplate",
                        principalTable: "AnnouncementStatusSupplierTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
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
                    MaintenanceRunningWorkId = table.Column<int>(nullable: false),
                    AnnouncementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementMaintenanceRunnigWorkSupplierTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementMaintenanceRunnigWorkSupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "AnnouncementTemplate",
                        principalTable: "AnnouncementSupplierTemplate",
                        principalColumn: "AnnouncementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementMaintenanceRunnigWorkSupplierTemplate_MaintenanceRunningWork_MaintenanceRunningWorkId",
                        column: x => x.MaintenanceRunningWorkId,
                        principalSchema: "LookUps",
                        principalTable: "MaintenanceRunningWork",
                        principalColumn: "MaintenanceRunningWorkId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementRequestSupplierTemplate",
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
                    StatusId = table.Column<int>(nullable: false),
                    AnnouncementId = table.Column<int>(nullable: false),
                    Cr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementRequestSupplierTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementRequestSupplierTemplate_AnnouncementSupplierTemplate_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "AnnouncementTemplate",
                        principalTable: "AnnouncementSupplierTemplate",
                        principalColumn: "AnnouncementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementRequestSupplierTemplate_Supplier_Cr",
                        column: x => x.Cr,
                        principalSchema: "IDM",
                        principalTable: "Supplier",
                        principalColumn: "SelectedCr",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementRequestSupplierTemplate_AnnouncementJoinRequestStatusSupplierTemplate_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "LookUps",
                        principalTable: "AnnouncementJoinRequestStatusSupplierTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementSuppliersTemplateAttachment",
                schema: "AnnouncementTemplate",
                columns: table => new
                {
                    AnnouncementSuppliersTemplateAttachmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    AttachmentTypeId = table.Column<int>(nullable: false),
                    FileNetReferenceId = table.Column<string>(maxLength: 1024, nullable: true),
                    AnnouncementId = table.Column<int>(nullable: false),
                    ChangeStatusId = table.Column<int>(nullable: true),
                    ReviewStatusId = table.Column<int>(nullable: true),
                    RejectionReason = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementSuppliersTemplateAttachment", x => x.AnnouncementSuppliersTemplateAttachmentId);
                    table.ForeignKey(
                        name: "FK_AnnouncementSuppliersTemplateAttachment_AnnouncementSupplierTemplate_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "AnnouncementTemplate",
                        principalTable: "AnnouncementSupplierTemplate",
                        principalColumn: "AnnouncementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementSuppliersTemplateAttachment_AttachmentType_AttachmentTypeId",
                        column: x => x.AttachmentTypeId,
                        principalSchema: "LookUps",
                        principalTable: "AttachmentType",
                        principalColumn: "AttachmentTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 29, 16, 26, 2, 721, DateTimeKind.Local).AddTicks(9321));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 29, 16, 26, 2, 722, DateTimeKind.Local).AddTicks(1434));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 29, 16, 26, 2, 722, DateTimeKind.Local).AddTicks(1464));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 29, 16, 26, 2, 722, DateTimeKind.Local).AddTicks(1469));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 29, 16, 26, 2, 722, DateTimeKind.Local).AddTicks(1469));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 29, 16, 26, 2, 722, DateTimeKind.Local).AddTicks(1469));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 29, 16, 26, 2, 722, DateTimeKind.Local).AddTicks(1469));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 29, 16, 26, 2, 722, DateTimeKind.Local).AddTicks(1475));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 29, 16, 26, 2, 722, DateTimeKind.Local).AddTicks(1475));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 29, 16, 26, 2, 722, DateTimeKind.Local).AddTicks(1475));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 29, 16, 26, 2, 722, DateTimeKind.Local).AddTicks(1475));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 29, 16, 26, 2, 722, DateTimeKind.Local).AddTicks(1480));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 12, 29, 16, 26, 2, 722, DateTimeKind.Local).AddTicks(1480));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 100,
                column: "IsCombinedRole",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementActivitySupplierTemplate_ActivityId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementActivitySupplierTemplate",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementActivitySupplierTemplate_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementActivitySupplierTemplate",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementAreaSupplierTemplate_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementAreaSupplierTemplate",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementAreaSupplierTemplate_AreaId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementAreaSupplierTemplate",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementConstructionWorkSupplierTemplate_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementConstructionWorkSupplierTemplate",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementConstructionWorkSupplierTemplate_ConstructionWorkId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementConstructionWorkSupplierTemplate",
                column: "ConstructionWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementCountrySupplierTemplate_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementCountrySupplierTemplate",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementCountrySupplierTemplate_CountryId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementCountrySupplierTemplate",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementHistorySupplierTemplate_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementHistorySupplierTemplate",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementHistorySupplierTemplate_StatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementHistorySupplierTemplate",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementMaintenanceRunnigWorkSupplierTemplate_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementMaintenanceRunnigWorkSupplierTemplate_MaintenanceRunningWorkId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                column: "MaintenanceRunningWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementRequestSupplierTemplate_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementRequestSupplierTemplate_Cr",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate",
                column: "Cr");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementRequestSupplierTemplate_StatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementSuppliersTemplateAttachment_AnnouncementId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementSuppliersTemplateAttachment_AttachmentTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSuppliersTemplateAttachment",
                column: "AttachmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementSupplierTemplate_AgencyCode",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                column: "AgencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementSupplierTemplate_BranchId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementSupplierTemplate_ReasonForSelectingTenderTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                column: "ReasonForSelectingTenderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementSupplierTemplate_StatusId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementSupplierTemplate_TenderTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                column: "TenderTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementActivitySupplierTemplate",
                schema: "AnnouncementTemplate");

            migrationBuilder.DropTable(
                name: "AnnouncementAreaSupplierTemplate",
                schema: "AnnouncementTemplate");

            migrationBuilder.DropTable(
                name: "AnnouncementConstructionWorkSupplierTemplate",
                schema: "AnnouncementTemplate");

            migrationBuilder.DropTable(
                name: "AnnouncementCountrySupplierTemplate",
                schema: "AnnouncementTemplate");

            migrationBuilder.DropTable(
                name: "AnnouncementHistorySupplierTemplate",
                schema: "AnnouncementTemplate");

            migrationBuilder.DropTable(
                name: "AnnouncementMaintenanceRunnigWorkSupplierTemplate",
                schema: "AnnouncementTemplate");

            migrationBuilder.DropTable(
                name: "AnnouncementRequestSupplierTemplate",
                schema: "AnnouncementTemplate");

            migrationBuilder.DropTable(
                name: "AnnouncementSuppliersTemplateAttachment",
                schema: "AnnouncementTemplate");

            migrationBuilder.DropTable(
                name: "AnnouncementJoinRequestStatusSupplierTemplate",
                schema: "LookUps");

            migrationBuilder.DropTable(
                name: "AnnouncementSupplierTemplate",
                schema: "AnnouncementTemplate");

            migrationBuilder.DropTable(
                name: "AnnouncementStatusSupplierTemplate",
                schema: "AnnouncementTemplate");
        }
    }
}
