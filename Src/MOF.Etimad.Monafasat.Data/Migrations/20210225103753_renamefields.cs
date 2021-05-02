using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class renamefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Activitytemplate_ActivitytemplateID",
                schema: "LookUps",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_ConditionTemplateActivities_Activitytemplate_ActivityTemplateId",
                schema: "Tender",
                table: "ConditionTemplateActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_TenderVersion_Versions_VersionId",
                schema: "Tender",
                table: "TenderVersion");

            migrationBuilder.DropTable(
                name: "ActivityVersion",
                schema: "Tender");

            migrationBuilder.DropTable(
                name: "Activitytemplate",
                schema: "LookUps");

            migrationBuilder.DropTable(
                name: "Versions",
                schema: "Versions");

            migrationBuilder.CreateTable(
                name: "Template",
                schema: "LookUps",
                columns: table => new
                {
                    ActivitytemplatId = table.Column<int>(nullable: false, comment: "Define identity of activity template"),
                    NameAr = table.Column<string>(maxLength: 1024, nullable: true, comment: "Define arabic name of activity template"),
                    NameEn = table.Column<string>(maxLength: 1024, nullable: true, comment: "Define english name of activity template"),
                    HasYears = table.Column<bool>(nullable: true, comment: "Define activity template has years or not")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.ActivitytemplatId);
                });

            migrationBuilder.CreateTable(
                name: "VersionHistory",
                schema: "Versions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    IsCurrentVersion = table.Column<bool>(nullable: false),
                    VersionTypeId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VersionHistory_VersionType_VersionTypeId",
                        column: x => x.VersionTypeId,
                        principalSchema: "Lookups",
                        principalTable: "VersionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityTemplateVersion",
                schema: "Tender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(nullable: false),
                    TemplateId = table.Column<int>(nullable: false),
                    VersionId = table.Column<int>(nullable: false),
                    HasYears = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTemplateVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityTemplateVersion_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalSchema: "LookUps",
                        principalTable: "Activity",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityTemplateVersion_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalSchema: "LookUps",
                        principalTable: "Template",
                        principalColumn: "ActivitytemplatId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityTemplateVersion_VersionHistory_VersionId",
                        column: x => x.VersionId,
                        principalSchema: "Versions",
                        principalTable: "VersionHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "Template",
                columns: new[] { "ActivitytemplatId", "HasYears", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, null, "عام", null },
                    { 14, null, "الخدمات الهندسية (اشراف, )", null },
                    { 13, null, "المستلزمات العامة(التوريد)", null },
                    { 12, null, "الخدمات الاستشارية", null },
                    { 11, null, "نظافة المدن", null },
                    { 10, null, "الخدمات الهندسية (تصميم)", null },
                    { 9, null, "تقنية المعلومات", null },
                    { 15, null, "جداول البيانات", null },
                    { 7, null, "التغذية ", null },
                    { 6, null, "المستلزمات الطبية", null },
                    { 5, null, "الصيانة الطبية ", null },
                    { 4, null, "الصيانة والتشغيل", null },
                    { 3, null, "انشاء مباني", null },
                    { 2, null, "إنشاء الطرق ", null },
                    { 8, null, " الادوية", null }
                });

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 12, 37, 49, 778, DateTimeKind.Local).AddTicks(6011));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 12, 37, 49, 778, DateTimeKind.Local).AddTicks(7335));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 12, 37, 49, 778, DateTimeKind.Local).AddTicks(7384));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 12, 37, 49, 778, DateTimeKind.Local).AddTicks(7387));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 12, 37, 49, 778, DateTimeKind.Local).AddTicks(7390));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 12, 37, 49, 778, DateTimeKind.Local).AddTicks(7393));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 12, 37, 49, 778, DateTimeKind.Local).AddTicks(7396));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 12, 37, 49, 778, DateTimeKind.Local).AddTicks(7399));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 12, 37, 49, 778, DateTimeKind.Local).AddTicks(7420));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 12, 37, 49, 778, DateTimeKind.Local).AddTicks(7442));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 12, 37, 49, 778, DateTimeKind.Local).AddTicks(7445));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 12, 37, 49, 778, DateTimeKind.Local).AddTicks(7448));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 12, 37, 49, 778, DateTimeKind.Local).AddTicks(7451));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 100,
                column: "IsCombinedRole",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTemplateVersion_ActivityId",
                schema: "Tender",
                table: "ActivityTemplateVersion",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTemplateVersion_TemplateId",
                schema: "Tender",
                table: "ActivityTemplateVersion",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTemplateVersion_VersionId",
                schema: "Tender",
                table: "ActivityTemplateVersion",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_VersionHistory_VersionTypeId",
                schema: "Versions",
                table: "VersionHistory",
                column: "VersionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Template_ActivitytemplateID",
                schema: "LookUps",
                table: "Activity",
                column: "ActivitytemplateID",
                principalSchema: "LookUps",
                principalTable: "Template",
                principalColumn: "ActivitytemplatId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConditionTemplateActivities_Template_ActivityTemplateId",
                schema: "Tender",
                table: "ConditionTemplateActivities",
                column: "ActivityTemplateId",
                principalSchema: "LookUps",
                principalTable: "Template",
                principalColumn: "ActivitytemplatId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TenderVersion_VersionHistory_VersionId",
                schema: "Tender",
                table: "TenderVersion",
                column: "VersionId",
                principalSchema: "Versions",
                principalTable: "VersionHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Template_ActivitytemplateID",
                schema: "LookUps",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_ConditionTemplateActivities_Template_ActivityTemplateId",
                schema: "Tender",
                table: "ConditionTemplateActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_TenderVersion_VersionHistory_VersionId",
                schema: "Tender",
                table: "TenderVersion");

            migrationBuilder.DropTable(
                name: "ActivityTemplateVersion",
                schema: "Tender");

            migrationBuilder.DropTable(
                name: "Template",
                schema: "LookUps");

            migrationBuilder.DropTable(
                name: "VersionHistory",
                schema: "Versions");

            migrationBuilder.CreateTable(
                name: "Activitytemplate",
                schema: "LookUps",
                columns: table => new
                {
                    ActivitytemplatId = table.Column<int>(type: "int", nullable: false, comment: "Define identity of activity template"),
                    HasYears = table.Column<bool>(type: "bit", nullable: true, comment: "Define activity template has years or not"),
                    NameAr = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true, comment: "Define arabic name of activity template"),
                    NameEn = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true, comment: "Define english name of activity template")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activitytemplate", x => x.ActivitytemplatId);
                });

            migrationBuilder.CreateTable(
                name: "Versions",
                schema: "Versions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsCurrentVersion = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VersionTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Versions_VersionType_VersionTypeId",
                        column: x => x.VersionTypeId,
                        principalSchema: "Lookups",
                        principalTable: "VersionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityVersion",
                schema: "Tender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    ActivityTemplateId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    VersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityVersion_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalSchema: "LookUps",
                        principalTable: "Activity",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityVersion_Activitytemplate_ActivityTemplateId",
                        column: x => x.ActivityTemplateId,
                        principalSchema: "LookUps",
                        principalTable: "Activitytemplate",
                        principalColumn: "ActivitytemplatId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityVersion_Versions_VersionId",
                        column: x => x.VersionId,
                        principalSchema: "Versions",
                        principalTable: "Versions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "Activitytemplate",
                columns: new[] { "ActivitytemplatId", "HasYears", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, null, "عام", null },
                    { 14, null, "الخدمات الهندسية (اشراف, )", null },
                    { 13, null, "المستلزمات العامة(التوريد)", null },
                    { 12, null, "الخدمات الاستشارية", null },
                    { 11, null, "نظافة المدن", null },
                    { 10, null, "الخدمات الهندسية (تصميم)", null },
                    { 9, null, "تقنية المعلومات", null },
                    { 15, null, "جداول البيانات", null },
                    { 7, null, "التغذية ", null },
                    { 6, null, "المستلزمات الطبية", null },
                    { 5, null, "الصيانة الطبية ", null },
                    { 4, null, "الصيانة والتشغيل", null },
                    { 3, null, "انشاء مباني", null },
                    { 2, null, "إنشاء الطرق ", null },
                    { 8, null, " الادوية", null }
                });

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 24, 18, 42, 7, 337, DateTimeKind.Local).AddTicks(219));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 24, 18, 42, 7, 337, DateTimeKind.Local).AddTicks(1808));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 24, 18, 42, 7, 337, DateTimeKind.Local).AddTicks(1820));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 24, 18, 42, 7, 337, DateTimeKind.Local).AddTicks(1823));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 24, 18, 42, 7, 337, DateTimeKind.Local).AddTicks(1825));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 24, 18, 42, 7, 337, DateTimeKind.Local).AddTicks(1827));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 24, 18, 42, 7, 337, DateTimeKind.Local).AddTicks(1829));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 24, 18, 42, 7, 337, DateTimeKind.Local).AddTicks(1831));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 24, 18, 42, 7, 337, DateTimeKind.Local).AddTicks(1833));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 24, 18, 42, 7, 337, DateTimeKind.Local).AddTicks(1835));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 24, 18, 42, 7, 337, DateTimeKind.Local).AddTicks(1837));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 24, 18, 42, 7, 337, DateTimeKind.Local).AddTicks(1839));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 24, 18, 42, 7, 337, DateTimeKind.Local).AddTicks(1841));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 100,
                column: "IsCombinedRole",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityVersion_ActivityId",
                schema: "Tender",
                table: "ActivityVersion",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityVersion_ActivityTemplateId",
                schema: "Tender",
                table: "ActivityVersion",
                column: "ActivityTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityVersion_VersionId",
                schema: "Tender",
                table: "ActivityVersion",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_VersionTypeId",
                schema: "Versions",
                table: "Versions",
                column: "VersionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Activitytemplate_ActivitytemplateID",
                schema: "LookUps",
                table: "Activity",
                column: "ActivitytemplateID",
                principalSchema: "LookUps",
                principalTable: "Activitytemplate",
                principalColumn: "ActivitytemplatId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConditionTemplateActivities_Activitytemplate_ActivityTemplateId",
                schema: "Tender",
                table: "ConditionTemplateActivities",
                column: "ActivityTemplateId",
                principalSchema: "LookUps",
                principalTable: "Activitytemplate",
                principalColumn: "ActivitytemplatId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TenderVersion_Versions_VersionId",
                schema: "Tender",
                table: "TenderVersion",
                column: "VersionId",
                principalSchema: "Versions",
                principalTable: "Versions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
