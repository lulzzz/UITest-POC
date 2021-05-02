using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class update_mandatorylist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "LookUps",
                table: "MandatoryListStatus",
                newName: "StatusId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "MandatoryList",
                table: "MandatoryListProduct",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MandatoryList",
                table: "MandatoryListProduct",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "MandatoryList",
                table: "MandatoryListProduct",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "MandatoryList",
                table: "MandatoryListProduct",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MandatoryList",
                table: "MandatoryListProduct",
                maxLength: 256,
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 18, 12, 27, 53, 677, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 18, 12, 27, 53, 678, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 18, 12, 27, 53, 678, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 18, 12, 27, 53, 678, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 18, 12, 27, 53, 678, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 18, 12, 27, 53, 678, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 18, 12, 27, 53, 678, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 18, 12, 27, 53, 678, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 18, 12, 27, 53, 678, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 18, 12, 27, 53, 678, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 18, 12, 27, 53, 678, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 18, 12, 27, 53, 678, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 18, 12, 27, 53, 678, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "MandatoryList",
                table: "MandatoryListProduct");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MandatoryList",
                table: "MandatoryListProduct");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "MandatoryList",
                table: "MandatoryListProduct");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "MandatoryList",
                table: "MandatoryListProduct");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MandatoryList",
                table: "MandatoryListProduct");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                schema: "LookUps",
                table: "MandatoryListStatus",
                newName: "Id");

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
                column: "CreatedAt",
                value: new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 16, 15, 6, 0, 522, DateTimeKind.Local));

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
        }
    }
}
