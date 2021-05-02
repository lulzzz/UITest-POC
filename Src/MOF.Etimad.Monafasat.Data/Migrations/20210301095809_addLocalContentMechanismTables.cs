using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class addLocalContentMechanismTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TenderDates_TenderId",
                schema: "Tender",
                table: "TenderDates");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Tender",
                table: "TenderLocalContent");

            migrationBuilder.AddColumn<bool>(
                name: "IsApplyProvisionsMandatoryList",
                schema: "Tender",
                table: "TenderLocalContent",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MinimumBaseline",
                schema: "Tender",
                table: "TenderLocalContent",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinimumPercentageLocalContentTarget",
                schema: "Tender",
                table: "TenderLocalContent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudyMinimumTargetFileNetReferenceId",
                schema: "Tender",
                table: "TenderLocalContent",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LocalContentMechanismPreference",
                schema: "LookUps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    NameEn = table.Column<string>(maxLength: 1024, nullable: true),
                    NameAr = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalContentMechanismPreference", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalContentMechanism",
                schema: "Tender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    LocalContentMechanismPreferenceId = table.Column<int>(nullable: false),
                    TenderLocalContentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalContentMechanism", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalContentMechanism_LocalContentMechanismPreference_LocalContentMechanismPreferenceId",
                        column: x => x.LocalContentMechanismPreferenceId,
                        principalSchema: "LookUps",
                        principalTable: "LocalContentMechanismPreference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocalContentMechanism_TenderLocalContent_TenderLocalContentId",
                        column: x => x.TenderLocalContentId,
                        principalSchema: "Tender",
                        principalTable: "TenderLocalContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "LocalContentMechanismPreference",
                columns: new[] { "Id", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, "آلية وزن المحتوى المحلي في التقييم المالي", "آلية وزن المحتوى المحلي في التقييم المالي" },
                    { 2, "آلية الحد الأدنى المطلوب للمحتوى المحلي", "آلية الحد الأدنى المطلوب للمحتوى المحلي" },
                    { 3, "آلية التفضيل السعري للمنتج ", "آلية التفضيل السعري للمنتج" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalContentMechanism_LocalContentMechanismPreferenceId",
                schema: "Tender",
                table: "LocalContentMechanism",
                column: "LocalContentMechanismPreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalContentMechanism_TenderLocalContentId",
                schema: "Tender",
                table: "LocalContentMechanism",
                column: "TenderLocalContentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalContentMechanism",
                schema: "Tender");

            migrationBuilder.DropTable(
                name: "LocalContentMechanismPreference",
                schema: "LookUps");

            migrationBuilder.DropIndex(
                name: "IX_TenderDates_TenderId",
                schema: "Tender",
                table: "TenderDates");

            migrationBuilder.DropColumn(
                name: "IsApplyProvisionsMandatoryList",
                schema: "Tender",
                table: "TenderLocalContent");

            migrationBuilder.DropColumn(
                name: "MinimumBaseline",
                schema: "Tender",
                table: "TenderLocalContent");

            migrationBuilder.DropColumn(
                name: "MinimumPercentageLocalContentTarget",
                schema: "Tender",
                table: "TenderLocalContent");

            migrationBuilder.DropColumn(
                name: "StudyMinimumTargetFileNetReferenceId",
                schema: "Tender",
                table: "TenderLocalContent");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Tender",
                table: "TenderLocalContent",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

          
        }
    }
}
