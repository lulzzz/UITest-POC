using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class allownullonhasyears : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "HasYears",
                schema: "Tender",
                table: "ActivityTemplateVersion",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 15, 51, 836, DateTimeKind.Local).AddTicks(1241));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 15, 51, 836, DateTimeKind.Local).AddTicks(2581));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 15, 51, 836, DateTimeKind.Local).AddTicks(2620));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 15, 51, 836, DateTimeKind.Local).AddTicks(2622));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 15, 51, 836, DateTimeKind.Local).AddTicks(2623));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 15, 51, 836, DateTimeKind.Local).AddTicks(2624));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 15, 51, 836, DateTimeKind.Local).AddTicks(2626));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 15, 51, 836, DateTimeKind.Local).AddTicks(2627));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 15, 51, 836, DateTimeKind.Local).AddTicks(2629));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 15, 51, 836, DateTimeKind.Local).AddTicks(2630));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 15, 51, 836, DateTimeKind.Local).AddTicks(2631));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 15, 51, 836, DateTimeKind.Local).AddTicks(2633));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 15, 51, 836, DateTimeKind.Local).AddTicks(2634));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 100,
                column: "IsCombinedRole",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "HasYears",
                schema: "Tender",
                table: "ActivityTemplateVersion",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 10, 49, 604, DateTimeKind.Local).AddTicks(1599));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 10, 49, 604, DateTimeKind.Local).AddTicks(2729));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 10, 49, 604, DateTimeKind.Local).AddTicks(2757));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 10, 49, 604, DateTimeKind.Local).AddTicks(2758));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 10, 49, 604, DateTimeKind.Local).AddTicks(2759));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 10, 49, 604, DateTimeKind.Local).AddTicks(2760));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 10, 49, 604, DateTimeKind.Local).AddTicks(2761));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 10, 49, 604, DateTimeKind.Local).AddTicks(2762));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 10, 49, 604, DateTimeKind.Local).AddTicks(2763));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 10, 49, 604, DateTimeKind.Local).AddTicks(2764));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 10, 49, 604, DateTimeKind.Local).AddTicks(2765));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 10, 49, 604, DateTimeKind.Local).AddTicks(2766));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 25, 13, 10, 49, 604, DateTimeKind.Local).AddTicks(2766));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 100,
                column: "IsCombinedRole",
                value: true);
        }
    }
}
