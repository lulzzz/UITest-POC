using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddQuantityTableVersionIdInTender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityTableVersionId",
                schema: "Tender",
                table: "Tender",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 28, 12, 7, 35, 770, DateTimeKind.Local).AddTicks(5656));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 28, 12, 7, 35, 770, DateTimeKind.Local).AddTicks(7166));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 28, 12, 7, 35, 770, DateTimeKind.Local).AddTicks(7207));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 28, 12, 7, 35, 770, DateTimeKind.Local).AddTicks(7209));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 28, 12, 7, 35, 770, DateTimeKind.Local).AddTicks(7211));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 28, 12, 7, 35, 770, DateTimeKind.Local).AddTicks(7214));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 28, 12, 7, 35, 770, DateTimeKind.Local).AddTicks(7216));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 28, 12, 7, 35, 770, DateTimeKind.Local).AddTicks(7217));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 28, 12, 7, 35, 770, DateTimeKind.Local).AddTicks(7219));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 28, 12, 7, 35, 770, DateTimeKind.Local).AddTicks(7221));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 28, 12, 7, 35, 770, DateTimeKind.Local).AddTicks(7222));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 28, 12, 7, 35, 770, DateTimeKind.Local).AddTicks(7224));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 28, 12, 7, 35, 770, DateTimeKind.Local).AddTicks(7226));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityTableVersionId",
                schema: "Tender",
                table: "Tender");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 14, 10, 56, 13, 402, DateTimeKind.Local).AddTicks(2970));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 14, 10, 56, 13, 402, DateTimeKind.Local).AddTicks(4099));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 14, 10, 56, 13, 402, DateTimeKind.Local).AddTicks(4122));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 14, 10, 56, 13, 402, DateTimeKind.Local).AddTicks(4123));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 14, 10, 56, 13, 402, DateTimeKind.Local).AddTicks(4164));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 14, 10, 56, 13, 402, DateTimeKind.Local).AddTicks(4165));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 14, 10, 56, 13, 402, DateTimeKind.Local).AddTicks(4167));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 14, 10, 56, 13, 402, DateTimeKind.Local).AddTicks(4168));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 14, 10, 56, 13, 402, DateTimeKind.Local).AddTicks(4169));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 14, 10, 56, 13, 402, DateTimeKind.Local).AddTicks(4171));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 14, 10, 56, 13, 402, DateTimeKind.Local).AddTicks(4172));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 14, 10, 56, 13, 402, DateTimeKind.Local).AddTicks(4173));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 14, 10, 56, 13, 402, DateTimeKind.Local).AddTicks(4174));
        }
    }
}
