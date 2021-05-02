using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class addmetadataforsuppliertendertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "SupplierPreQualificationDocument",
                schema: "Qualification",
                comment: "Define the Documents applied from the supplier  Qualification");

            migrationBuilder.AlterTable(
                name: "SupplierPreQualificationAttachment",
                schema: "Qualification",
                comment: "Define the Attachments needed for the Qualification");

            migrationBuilder.AlterTable(
                name: "SupplierTenderQuantityTable",
                schema: "Offer",
                comment: "Define the Quantity Table for Supplier to Apply his offer");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierId",
                schema: "Qualification",
                table: "SupplierPreQualificationDocument",
                nullable: true,
                comment: "The supplier Commercial Registeration Number ",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "Qualification",
                table: "SupplierPreQualificationDocument",
                nullable: true,
                comment: "Define the status of the request",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "Qualification",
                table: "SupplierPreQualificationDocument",
                nullable: true,
                comment: "Define the Rejection Reason if the request was rejected",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PreQualificationResult",
                schema: "Qualification",
                table: "SupplierPreQualificationDocument",
                nullable: true,
                comment: "Define result of evaluation the supplier files ",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PreQualificationId",
                schema: "Qualification",
                table: "SupplierPreQualificationDocument",
                nullable: false,
                comment: "Define the related Qualification",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "FileNetReferenceId",
                schema: "Qualification",
                table: "SupplierPreQualificationAttachment",
                maxLength: 1024,
                nullable: true,
                comment: "Define the reference id from file net system",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                schema: "Qualification",
                table: "SupplierPreQualificationAttachment",
                maxLength: 1024,
                nullable: true,
                comment: "Define name of the attachment",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                schema: "Qualification",
                table: "SupplierPreQualificationAttachment",
                nullable: false,
                comment: "Define  the Identity  for the table",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "TenederQuantityId",
                schema: "Offer",
                table: "SupplierTenderQuantityTable",
                nullable: true,
                comment: "Define releated Tender Quantity table",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Offer",
                table: "SupplierTenderQuantityTable",
                nullable: false,
                comment: "Define  Name of  the table",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdjustedTotalVAT",
                schema: "Offer",
                table: "SupplierTenderQuantityTable",
                nullable: false,
                comment: "Not Used",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "AdjustedTotalPrice",
                schema: "Offer",
                table: "SupplierTenderQuantityTable",
                nullable: true,
                comment: "Not Used",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdjustedTotalDiscount",
                schema: "Offer",
                table: "SupplierTenderQuantityTable",
                nullable: true,
                comment: "Not Used",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "Offer",
                table: "SupplierTenderQuantityTable",
                nullable: false,
                comment: "Define  the Identity  for the table",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 16, 21, 19, 767, DateTimeKind.Local).AddTicks(1134));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 16, 21, 19, 767, DateTimeKind.Local).AddTicks(2487));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 16, 21, 19, 767, DateTimeKind.Local).AddTicks(2520));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 16, 21, 19, 767, DateTimeKind.Local).AddTicks(2522));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 16, 21, 19, 767, DateTimeKind.Local).AddTicks(2524));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 16, 21, 19, 767, DateTimeKind.Local).AddTicks(2525));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 16, 21, 19, 767, DateTimeKind.Local).AddTicks(2526));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 16, 21, 19, 767, DateTimeKind.Local).AddTicks(2527));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 16, 21, 19, 767, DateTimeKind.Local).AddTicks(2528));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 16, 21, 19, 767, DateTimeKind.Local).AddTicks(2529));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 16, 21, 19, 767, DateTimeKind.Local).AddTicks(2530));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 16, 21, 19, 767, DateTimeKind.Local).AddTicks(2531));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 23, 16, 21, 19, 767, DateTimeKind.Local).AddTicks(2532));

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
            migrationBuilder.AlterTable(
                name: "SupplierPreQualificationDocument",
                schema: "Qualification",
                oldComment: "Define the Documents applied from the supplier  Qualification");

            migrationBuilder.AlterTable(
                name: "SupplierPreQualificationAttachment",
                schema: "Qualification",
                oldComment: "Define the Attachments needed for the Qualification");

            migrationBuilder.AlterTable(
                name: "SupplierTenderQuantityTable",
                schema: "Offer",
                oldComment: "Define the Quantity Table for Supplier to Apply his offer");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierId",
                schema: "Qualification",
                table: "SupplierPreQualificationDocument",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "The supplier Commercial Registeration Number ");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                schema: "Qualification",
                table: "SupplierPreQualificationDocument",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the status of the request");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "Qualification",
                table: "SupplierPreQualificationDocument",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Define the Rejection Reason if the request was rejected");

            migrationBuilder.AlterColumn<int>(
                name: "PreQualificationResult",
                schema: "Qualification",
                table: "SupplierPreQualificationDocument",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define result of evaluation the supplier files ");

            migrationBuilder.AlterColumn<int>(
                name: "PreQualificationId",
                schema: "Qualification",
                table: "SupplierPreQualificationDocument",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Qualification");

            migrationBuilder.AlterColumn<string>(
                name: "FileNetReferenceId",
                schema: "Qualification",
                table: "SupplierPreQualificationAttachment",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define the reference id from file net system");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                schema: "Qualification",
                table: "SupplierPreQualificationAttachment",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true,
                oldComment: "Define name of the attachment");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                schema: "Qualification",
                table: "SupplierPreQualificationAttachment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define  the Identity  for the table")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "TenederQuantityId",
                schema: "Offer",
                table: "SupplierTenderQuantityTable",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true,
                oldComment: "Define releated Tender Quantity table");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Offer",
                table: "SupplierTenderQuantityTable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldComment: "Define  Name of  the table");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdjustedTotalVAT",
                schema: "Offer",
                table: "SupplierTenderQuantityTable",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<string>(
                name: "AdjustedTotalPrice",
                schema: "Offer",
                table: "SupplierTenderQuantityTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<string>(
                name: "AdjustedTotalDiscount",
                schema: "Offer",
                table: "SupplierTenderQuantityTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComment: "Not Used");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "Offer",
                table: "SupplierTenderQuantityTable",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldComment: "Define  the Identity  for the table")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 14, 13, 34, 57, 128, DateTimeKind.Local).AddTicks(3751));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 14, 13, 34, 57, 128, DateTimeKind.Local).AddTicks(4873));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 14, 13, 34, 57, 128, DateTimeKind.Local).AddTicks(4882));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 14, 13, 34, 57, 128, DateTimeKind.Local).AddTicks(4884));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 14, 13, 34, 57, 128, DateTimeKind.Local).AddTicks(4885));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 14, 13, 34, 57, 128, DateTimeKind.Local).AddTicks(4887));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 14, 13, 34, 57, 128, DateTimeKind.Local).AddTicks(4888));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 14, 13, 34, 57, 128, DateTimeKind.Local).AddTicks(4890));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 14, 13, 34, 57, 128, DateTimeKind.Local).AddTicks(4891));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 14, 13, 34, 57, 128, DateTimeKind.Local).AddTicks(4893));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 14, 13, 34, 57, 128, DateTimeKind.Local).AddTicks(4894));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 14, 13, 34, 57, 128, DateTimeKind.Local).AddTicks(4896));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 14, 13, 34, 57, 128, DateTimeKind.Local).AddTicks(4898));

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
