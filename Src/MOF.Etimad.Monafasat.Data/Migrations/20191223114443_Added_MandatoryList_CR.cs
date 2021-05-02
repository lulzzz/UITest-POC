using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class Added_MandatoryList_CR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MandatoryListChangeRequestStatus",
                schema: "LookUps",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false),
                    NameAr = table.Column<string>(maxLength: 100, nullable: true),
                    NameEn = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandatoryListChangeRequestStatus", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "MandatoryListChangeRequest",
                schema: "MandatoryList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    DivisionNameAr = table.Column<string>(maxLength: 400, nullable: true),
                    DivisionNameEn = table.Column<string>(maxLength: 400, nullable: true),
                    DivisionCode = table.Column<string>(maxLength: 400, nullable: true),
                    RejectionReason = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandatoryListChangeRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MandatoryListChangeRequest_MandatoryListChangeRequestStatus_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "LookUps",
                        principalTable: "MandatoryListChangeRequestStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MandatoryListProductChangeRequest",
                schema: "MandatoryList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    CSICode = table.Column<string>(maxLength: 400, nullable: true),
                    NameAr = table.Column<string>(maxLength: 400, nullable: true),
                    NameEn = table.Column<string>(maxLength: 400, nullable: true),
                    DescriptionAr = table.Column<string>(nullable: true),
                    DescriptionEn = table.Column<string>(nullable: true),
                    PriceCelling = table.Column<double>(nullable: false),
                    MandatoryListChangeRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandatoryListProductChangeRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MandatoryListProductChangeRequest_MandatoryListChangeRequest_MandatoryListChangeRequestId",
                        column: x => x.MandatoryListChangeRequestId,
                        principalSchema: "MandatoryList",
                        principalTable: "MandatoryListChangeRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "MandatoryListChangeRequestStatus",
                columns: new[] { "StatusId", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, "بانتظار الاعتماد", null },
                    { 2, "تم الرفض", null },
                    { 3, "معتمدة", null },
                    { 4, "مغلقة", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MandatoryListChangeRequest_StatusId",
                schema: "MandatoryList",
                table: "MandatoryListChangeRequest",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MandatoryListProductChangeRequest_MandatoryListChangeRequestId",
                schema: "MandatoryList",
                table: "MandatoryListProductChangeRequest",
                column: "MandatoryListChangeRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MandatoryListProductChangeRequest",
                schema: "MandatoryList");

            migrationBuilder.DropTable(
                name: "MandatoryListChangeRequest",
                schema: "MandatoryList");

            migrationBuilder.DropTable(
                name: "MandatoryListChangeRequestStatus",
                schema: "LookUps");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 22, 16, 46, 7, 754, DateTimeKind.Local).AddTicks(5614));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 22, 16, 46, 7, 754, DateTimeKind.Local).AddTicks(7074));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 22, 16, 46, 7, 754, DateTimeKind.Local).AddTicks(7100));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 22, 16, 46, 7, 754, DateTimeKind.Local).AddTicks(7100));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 22, 16, 46, 7, 754, DateTimeKind.Local).AddTicks(7105));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 22, 16, 46, 7, 754, DateTimeKind.Local).AddTicks(7105));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 22, 16, 46, 7, 754, DateTimeKind.Local).AddTicks(7105));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 22, 16, 46, 7, 754, DateTimeKind.Local).AddTicks(7105));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 22, 16, 46, 7, 754, DateTimeKind.Local).AddTicks(7105));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 22, 16, 46, 7, 754, DateTimeKind.Local).AddTicks(7110));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 22, 16, 46, 7, 754, DateTimeKind.Local).AddTicks(7110));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 22, 16, 46, 7, 754, DateTimeKind.Local).AddTicks(7110));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 22, 16, 46, 7, 754, DateTimeKind.Local).AddTicks(7110));
        }
    }
}
