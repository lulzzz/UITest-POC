using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class added_FinancialCheckingDateSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FinancialCheckingDateSet",
                schema: "Tender",
                table: "Tender",
                nullable: true);

            //migrationBuilder.InsertData(
            //    schema: "LookUps",
            //    table: "TenderStatus",
            //    columns: new[] { "TenderStatusId", "NameAr", "NameEn" },
            //    values: new object[,]
            //    {
            //        { 85, "بانتظار اعتماد تقرير فتح العروض المالية", null },
            //        { 86, "تم فتح العروض المالية", null },
            //        { 87, "تم رفض تقرير فتح العروض المالية", null },
            //        { 78, "مرحلة فتح العروض الفنية", null },
            //        { 80, "بإنتظار إعتماد تقرير فتح العروض الفنية", null },
            //        { 82, "تم فتح العروض الفنية", null },
            //        { 84, "تم رفض تقرير فتح العروض الفنية", null },
            //        { 90, "مرحلة فحص العروض الفنية", null }
            //    });

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 1,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 27, 12, 24, 32, 2, DateTimeKind.Local).AddTicks(2770));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 2,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 27, 12, 24, 32, 2, DateTimeKind.Local).AddTicks(4726));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 3,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 27, 12, 24, 32, 2, DateTimeKind.Local).AddTicks(4779));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 4,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 27, 12, 24, 32, 2, DateTimeKind.Local).AddTicks(4783));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 5,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 27, 12, 24, 32, 2, DateTimeKind.Local).AddTicks(4786));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 6,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 27, 12, 24, 32, 2, DateTimeKind.Local).AddTicks(4789));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 7,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 27, 12, 24, 32, 2, DateTimeKind.Local).AddTicks(4792));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 8,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 27, 12, 24, 32, 2, DateTimeKind.Local).AddTicks(4795));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 9,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 27, 12, 24, 32, 2, DateTimeKind.Local).AddTicks(4797));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 10,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 27, 12, 24, 32, 2, DateTimeKind.Local).AddTicks(4800));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 11,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 27, 12, 24, 32, 2, DateTimeKind.Local).AddTicks(4803));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 12,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 27, 12, 24, 32, 2, DateTimeKind.Local).AddTicks(4806));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 13,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 9, 27, 12, 24, 32, 2, DateTimeKind.Local).AddTicks(4808));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    schema: "LookUps",
            //    table: "TenderStatus",
            //    keyColumn: "TenderStatusId",
            //    keyValue: 78);

            //migrationBuilder.DeleteData(
            //    schema: "LookUps",
            //    table: "TenderStatus",
            //    keyColumn: "TenderStatusId",
            //    keyValue: 80);

            //migrationBuilder.DeleteData(
            //    schema: "LookUps",
            //    table: "TenderStatus",
            //    keyColumn: "TenderStatusId",
            //    keyValue: 82);

            //migrationBuilder.DeleteData(
            //    schema: "LookUps",
            //    table: "TenderStatus",
            //    keyColumn: "TenderStatusId",
            //    keyValue: 84);

            //migrationBuilder.DeleteData(
            //    schema: "LookUps",
            //    table: "TenderStatus",
            //    keyColumn: "TenderStatusId",
            //    keyValue: 85);

            //migrationBuilder.DeleteData(
            //    schema: "LookUps",
            //    table: "TenderStatus",
            //    keyColumn: "TenderStatusId",
            //    keyValue: 86);

            //migrationBuilder.DeleteData(
            //    schema: "LookUps",
            //    table: "TenderStatus",
            //    keyColumn: "TenderStatusId",
            //    keyValue: 87);

            //migrationBuilder.DeleteData(
            //    schema: "LookUps",
            //    table: "TenderStatus",
            //    keyColumn: "TenderStatusId",
            //    keyValue: 90);

            //migrationBuilder.DropColumn(
            //    name: "FinancialCheckingDateSet",
            //    schema: "Tender",
            //    table: "Tender");

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 1,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 16, 13, 27, 107, DateTimeKind.Local).AddTicks(9340));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 2,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(898));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 3,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(938));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 4,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(941));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 5,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(943));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 6,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(945));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 7,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(948));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 8,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(950));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 9,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(952));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 10,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(955));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 11,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(957));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 12,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(959));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 13,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 8, 10, 16, 13, 27, 108, DateTimeKind.Local).AddTicks(962));
        }
    }
}
