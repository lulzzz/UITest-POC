using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class MandatoryListProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MandatoryList");

            migrationBuilder.CreateTable(
                name: "MandatoryListStatus",
                schema: "LookUps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    NameAr = table.Column<string>(maxLength: 100, nullable: true),
                    NameEn = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandatoryListStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MandatoryList",
                schema: "MandatoryList",
                columns: table => new
                {
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DivisionNameAr = table.Column<string>(maxLength: 400, nullable: true),
                    DivisionNameEn = table.Column<string>(maxLength: 400, nullable: true),
                    DivisionCode = table.Column<string>(maxLength: 400, nullable: true),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandatoryList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MandatoryList_MandatoryListStatus_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "LookUps",
                        principalTable: "MandatoryListStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MandatoryListProduct",
                schema: "MandatoryList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CSICode = table.Column<string>(maxLength: 400, nullable: true),
                    NameAr = table.Column<string>(maxLength: 400, nullable: true),
                    NameEn = table.Column<string>(maxLength: 400, nullable: true),
                    DescriptionAr = table.Column<string>(nullable: true),
                    DescriptionEn = table.Column<string>(nullable: true),
                    PriceCelling = table.Column<double>(nullable: false),
                    MandatoryListId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandatoryListProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MandatoryListProduct_MandatoryList_MandatoryListId",
                        column: x => x.MandatoryListId,
                        principalSchema: "MandatoryList",
                        principalTable: "MandatoryList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "MandatoryListStatus",
                columns: new[] { "Id", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, "تحت الإنشاء", null },
                    { 2, "بانتظار الاعتماد", null },
                    { 3, "تم الرفض", null },
                    { 4, "معتمدة", null },
                    { 5, "بانتظار اعتماد الإلغاء", null },
                    { 6, "تم رفض الالغاء", null },
                    { 7, "تم الالغاء", null }
                });

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                columns: new[] { "CreatedAt", "NameAr" },
                values: new object[] { new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local), "منافسة عامة (النظام القديم)" });

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                columns: new[] { "CreatedAt", "NameAr" },
                values: new object[] { new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local), "شراء مباشر (النظام القديم)" });

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local));

            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "UserRole",
                columns: new[] { "UserRoleId", "DisplayNameAr", "DisplayNameEn", "Name" },
                values: new object[,]
                {
                    { 41, "مسؤول منتجات القائمة الإلزامية", "Mandatory List Officer", "LC_ProductsOfficer" },
                    { 42, "معتمد منتجات القائمة الإلزامية", "Mandatory List Approver", "LC_ProductsApprover" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MandatoryList_StatusId",
                schema: "MandatoryList",
                table: "MandatoryList",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MandatoryListProduct_MandatoryListId",
                schema: "MandatoryList",
                table: "MandatoryListProduct",
                column: "MandatoryListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MandatoryListProduct",
                schema: "MandatoryList");

            migrationBuilder.DropTable(
                name: "MandatoryList",
                schema: "MandatoryList");

            migrationBuilder.DropTable(
                name: "MandatoryListStatus",
                schema: "LookUps");

            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 42);

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 3, 15, 48, 11, 483, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 3, 15, 48, 11, 483, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 3, 15, 48, 11, 483, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 3, 15, 48, 11, 483, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 3, 15, 48, 11, 483, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 3, 15, 48, 11, 483, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 3, 15, 48, 11, 483, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 3, 15, 48, 11, 483, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                columns: new[] { "CreatedAt", "NameAr" },
                values: new object[] { new DateTime(2019, 12, 3, 15, 48, 11, 483, DateTimeKind.Local), "منافسة عامة (حالى)" });

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                columns: new[] { "CreatedAt", "NameAr" },
                values: new object[] { new DateTime(2019, 12, 3, 15, 48, 11, 483, DateTimeKind.Local), " شراء مباشر (حالى)" });

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 3, 15, 48, 11, 483, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 3, 15, 48, 11, 483, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 3, 15, 48, 11, 483, DateTimeKind.Local));
        }
    }
}
