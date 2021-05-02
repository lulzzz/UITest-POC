using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class updateExtendOfferValiditySupplierStatue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "SupplierExtendOffersValidityStatus",
                keyColumn: "SupplierExtendOffersValidityStatusId",
                keyValue: 16,
                column: "Name",
                value: "تم الرد (بالموافقةالمبدئية)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "SupplierExtendOffersValidityStatus",
                keyColumn: "SupplierExtendOffersValidityStatusId",
                keyValue: 16,
                column: "Name",
                value: "تم الرد (بالموافقة)");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(5388));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(6982));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7098));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7104));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7109));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7113));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7117));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7122));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7126));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7131));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7135));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7140));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7145));
        }
    }
}
