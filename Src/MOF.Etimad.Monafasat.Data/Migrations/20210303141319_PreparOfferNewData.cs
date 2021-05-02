using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class PreparOfferNewData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ServicesAndWorkImplementationsMethod",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OffersChecking",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OffersEvaluationCriteria",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 16, 13, 16, 195, DateTimeKind.Local).AddTicks(2329));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 16, 13, 16, 195, DateTimeKind.Local).AddTicks(3933));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 16, 13, 16, 195, DateTimeKind.Local).AddTicks(3947));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 16, 13, 16, 195, DateTimeKind.Local).AddTicks(3950));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 16, 13, 16, 195, DateTimeKind.Local).AddTicks(3952));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 16, 13, 16, 195, DateTimeKind.Local).AddTicks(3954));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 16, 13, 16, 195, DateTimeKind.Local).AddTicks(3957));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 16, 13, 16, 195, DateTimeKind.Local).AddTicks(3959));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 16, 13, 16, 195, DateTimeKind.Local).AddTicks(3961));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 16, 13, 16, 195, DateTimeKind.Local).AddTicks(3964));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 16, 13, 16, 195, DateTimeKind.Local).AddTicks(3965));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 16, 13, 16, 195, DateTimeKind.Local).AddTicks(3967));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 16, 13, 16, 195, DateTimeKind.Local).AddTicks(3969));

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
            migrationBuilder.DropColumn(
                name: "OffersChecking",
                schema: "Tender",
                table: "TenderConditionsTemplate");

            migrationBuilder.DropColumn(
                name: "OffersEvaluationCriteria",
                schema: "Tender",
                table: "TenderConditionsTemplate");

            migrationBuilder.AlterColumn<string>(
                name: "ServicesAndWorkImplementationsMethod",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 12, 35, 26, 482, DateTimeKind.Local).AddTicks(6695));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 12, 35, 26, 482, DateTimeKind.Local).AddTicks(7708));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 12, 35, 26, 482, DateTimeKind.Local).AddTicks(7729));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 12, 35, 26, 482, DateTimeKind.Local).AddTicks(7729));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 12, 35, 26, 482, DateTimeKind.Local).AddTicks(7729));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 12, 35, 26, 482, DateTimeKind.Local).AddTicks(7729));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 12, 35, 26, 482, DateTimeKind.Local).AddTicks(7734));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 12, 35, 26, 482, DateTimeKind.Local).AddTicks(7734));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 12, 35, 26, 482, DateTimeKind.Local).AddTicks(7734));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 12, 35, 26, 482, DateTimeKind.Local).AddTicks(7734));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 12, 35, 26, 482, DateTimeKind.Local).AddTicks(7734));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 12, 35, 26, 482, DateTimeKind.Local).AddTicks(7734));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 3, 12, 35, 26, 482, DateTimeKind.Local).AddTicks(7739));

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
