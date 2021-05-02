using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddCommemtToTenderEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TenderTypeOtherSelectionReason",
                schema: "Tender",
                table: "Tender",
                maxLength: 1024,
                nullable: true,
                comment: "Define the reason of selecting direct purchase or limited tender that not exist in reasons list",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderTypeId",
                schema: "Tender",
                table: "Tender",
                nullable: false,
                comment: "Define Type Of Tender",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TenderNumber",
                schema: "Tender",
                table: "Tender",
                maxLength: 100,
                nullable: true,
                comment: "Define Number Of Tender",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenderName",
                schema: "Tender",
                table: "Tender",
                maxLength: 1024,
                nullable: true,
                comment: "Define Name Of Tender",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "TenderAwardingType",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the awarding type of tender partial or total awarding",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "SupplierNeedSample",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Boolean detrmine tf the supplier need samples of the tender",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmitionDate",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the publish/approval date of tender",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SamplesDeliveryAddress",
                schema: "Tender",
                table: "Tender",
                maxLength: 2048,
                nullable: true,
                comment: "Define the address of samples deleviry of the tender",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                schema: "Tender",
                table: "Tender",
                maxLength: 100,
                nullable: true,
                comment: "Define Reference Number Of Tender, its a unique Identifier Like Tender Identity",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                schema: "Tender",
                table: "Tender",
                maxLength: 1024,
                nullable: true,
                comment: "Define Purpose Of Tender",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OffersOpeningDate",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the opening date for opening the suppliers offers",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OffersCheckingDate",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the checking date for checking the suppliers offers",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastOfferPresentationDate",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the last date that supplier can apply offers on Tender",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEnqueriesDate",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the last date that supplier can enquiry or ask questions for tender",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "InsideKSA",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the location of tender",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InitialGuaranteePercentage",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the percentage of initial gurantee for suppliers if agency require it",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InitialGuaranteeAddress",
                schema: "Tender",
                table: "Tender",
                maxLength: 1024,
                nullable: true,
                comment: "Define the address of initial gurantee that suppliers apply it",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedValue",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the estimation value of tender",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                schema: "Tender",
                table: "Tender",
                maxLength: 5000,
                nullable: true,
                comment: "Define more information about tender",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ConditionsBookletPrice",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the price of conditions booklet Of Tender, it is determined by the government agency and supplier can buy it",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AwardingStoppingPeriod",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Define the period that suppliers can add plaint on tender after awarding between 5 and 10 days",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AwardingReportFileNameId",
                schema: "Tender",
                table: "Tender",
                maxLength: 500,
                nullable: true,
                comment: "Define the identity of awarding report that entered while entering awarding data in awarding stage",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AwardingReportFileName",
                schema: "Tender",
                table: "Tender",
                maxLength: 500,
                nullable: true,
                comment: "Define the name of awarding report that entered while entering awarding data in awarding stage",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActivityDescription",
                schema: "Tender",
                table: "Tender",
                maxLength: 2000,
                nullable: true,
                comment: "Define the description of tender activiites",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "Tender",
                nullable: false,
                comment: "Define Identity Of Tender",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 2, 10, 17, 9, 29, 719, DateTimeKind.Local).AddTicks(8106));

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TenderTypeOtherSelectionReason",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the reason of selecting direct purchase or limited tender that not exist in reasons list");

            migrationBuilder.AlterColumn<int>(
                name: "TenderTypeId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Type Of Tender");

            migrationBuilder.AlterColumn<string>(
                name: "TenderNumber",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define Number Of Tender");

            migrationBuilder.AlterColumn<string>(
                name: "TenderName",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define Name Of Tender");

            migrationBuilder.AlterColumn<bool>(
                name: "TenderAwardingType",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define the awarding type of tender partial or total awarding");

            migrationBuilder.AlterColumn<bool>(
                name: "SupplierNeedSample",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Boolean detrmine tf the supplier need samples of the tender");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmitionDate",
                schema: "Tender",
                table: "Tender",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define the publish/approval date of tender");

            migrationBuilder.AlterColumn<string>(
                name: "SamplesDeliveryAddress",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2048,
                oldNullable: true,
                oldComment: "Define the address of samples deleviry of the tender");

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceNumber",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Define Reference Number Of Tender, its a unique Identifier Like Tender Identity");

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define Purpose Of Tender");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OffersOpeningDate",
                schema: "Tender",
                table: "Tender",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define the opening date for opening the suppliers offers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OffersCheckingDate",
                schema: "Tender",
                table: "Tender",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define the checking date for checking the suppliers offers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastOfferPresentationDate",
                schema: "Tender",
                table: "Tender",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define the last date that supplier can apply offers on Tender");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastEnqueriesDate",
                schema: "Tender",
                table: "Tender",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldComment: "Define the last date that supplier can enquiry or ask questions for tender");

            migrationBuilder.AlterColumn<bool>(
                name: "InsideKSA",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define the location of tender");

            migrationBuilder.AlterColumn<decimal>(
                name: "InitialGuaranteePercentage",
                schema: "Tender",
                table: "Tender",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "Define the percentage of initial gurantee for suppliers if agency require it");

            migrationBuilder.AlterColumn<string>(
                name: "InitialGuaranteeAddress",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the address of initial gurantee that suppliers apply it");

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedValue",
                schema: "Tender",
                table: "Tender",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "Define the estimation value of tender");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5000,
                oldNullable: true,
                oldComment: "Define more information about tender");

            migrationBuilder.AlterColumn<decimal>(
                name: "ConditionsBookletPrice",
                schema: "Tender",
                table: "Tender",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "Define the price of conditions booklet Of Tender, it is determined by the government agency and supplier can buy it");

            migrationBuilder.AlterColumn<int>(
                name: "AwardingStoppingPeriod",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the period that suppliers can add plaint on tender after awarding between 5 and 10 days");

            migrationBuilder.AlterColumn<string>(
                name: "AwardingReportFileNameId",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Define the identity of awarding report that entered while entering awarding data in awarding stage");

            migrationBuilder.AlterColumn<string>(
                name: "AwardingReportFileName",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Define the name of awarding report that entered while entering awarding data in awarding stage");

            migrationBuilder.AlterColumn<string>(
                name: "ActivityDescription",
                schema: "Tender",
                table: "Tender",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "Define the description of tender activiites");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define Identity Of Tender")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
