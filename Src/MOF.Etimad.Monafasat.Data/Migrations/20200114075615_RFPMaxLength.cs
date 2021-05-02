using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class RFPMaxLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OutputStage",
                schema: "Tender",
                table: "TenderConditionsTemplateTechnicalOutput",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "OutputName",
                schema: "Tender",
                table: "TenderConditionsTemplateTechnicalOutput",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "OutputDescriptions",
                schema: "Tender",
                table: "TenderConditionsTemplateTechnicalOutput",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "OutputDeliveryTime",
                schema: "Tender",
                table: "TenderConditionsTemplateTechnicalOutput",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Term",
                schema: "Tender",
                table: "TenderConditionsTemplateTechnicalDelrations",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Decleration",
                schema: "Tender",
                table: "TenderConditionsTemplateTechnicalDelrations",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "WritePrice",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WorksProgram",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WorkforceSpecifications",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WorkLocationDetails",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenderFragmentation",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ServicesAndWorkImplementationsMethod",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SafetySpecifications",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QualitySpecifications",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectsScope",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialsSpecifications",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EquipmentsSpecifications",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConfirimJoiningTheTender",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AlternativeEmailForCommuncation",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgentJob",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgentEmail",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgencyDecalration",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OutputStage",
                schema: "Tender",
                table: "TenderConditionsTemplateTechnicalOutput",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "OutputName",
                schema: "Tender",
                table: "TenderConditionsTemplateTechnicalOutput",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "OutputDescriptions",
                schema: "Tender",
                table: "TenderConditionsTemplateTechnicalOutput",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "OutputDeliveryTime",
                schema: "Tender",
                table: "TenderConditionsTemplateTechnicalOutput",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Term",
                schema: "Tender",
                table: "TenderConditionsTemplateTechnicalDelrations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "Decleration",
                schema: "Tender",
                table: "TenderConditionsTemplateTechnicalDelrations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "WritePrice",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WorksProgram",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WorkforceSpecifications",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WorkLocationDetails",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenderFragmentation",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ServicesAndWorkImplementationsMethod",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SafetySpecifications",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QualitySpecifications",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectsScope",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialsSpecifications",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EquipmentsSpecifications",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConfirimJoiningTheTender",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AlternativeEmailForCommuncation",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgentJob",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgentEmail",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgencyDecalration",
                schema: "Tender",
                table: "TenderConditionsTemplate",
                type: "nvarchar(500)",
                maxLength: 500,
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
                value: new DateTime(2019, 12, 26, 13, 16, 59, 267, DateTimeKind.Local).AddTicks(1523));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 26, 13, 16, 59, 267, DateTimeKind.Local).AddTicks(3009));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 26, 13, 16, 59, 267, DateTimeKind.Local).AddTicks(3035));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 26, 13, 16, 59, 267, DateTimeKind.Local).AddTicks(3035));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 26, 13, 16, 59, 267, DateTimeKind.Local).AddTicks(3040));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 26, 13, 16, 59, 267, DateTimeKind.Local).AddTicks(3040));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 26, 13, 16, 59, 267, DateTimeKind.Local).AddTicks(3040));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 26, 13, 16, 59, 267, DateTimeKind.Local).AddTicks(3040));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 26, 13, 16, 59, 267, DateTimeKind.Local).AddTicks(3040));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 26, 13, 16, 59, 267, DateTimeKind.Local).AddTicks(3045));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 26, 13, 16, 59, 267, DateTimeKind.Local).AddTicks(3045));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 26, 13, 16, 59, 267, DateTimeKind.Local).AddTicks(3045));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 26, 13, 16, 59, 267, DateTimeKind.Local).AddTicks(3045));
        }
    }
}
