using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class Offer_LocalContentPricesFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "BillArchive",
                schema: "Sadad",
                comment: "Descripe the payment history for Conditional Booklet and Invitation");

            migrationBuilder.AlterTable(
                name: "SupplierCombinedDetail",
                schema: "Offer",
                comment: "Descripe the Details for suppliers [Offer owner and Combined]");

            migrationBuilder.AlterColumn<string>(
                name: "TenderReferenceNumber",
                schema: "Sadad",
                table: "BillArchive",
                maxLength: 100,
                nullable: true,
                comment: "Related Tender Reference Number",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Sadad",
                table: "BillArchive",
                nullable: false,
                comment: "Define the related Tender",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierNumber",
                schema: "Sadad",
                table: "BillArchive",
                maxLength: 20,
                nullable: true,
                comment: "Define Supplier Commercial Number",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InvitationId",
                schema: "Sadad",
                table: "BillArchive",
                nullable: true,
                comment: "This refere the invitation ",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConditionsBookletID",
                schema: "Sadad",
                table: "BillArchive",
                nullable: true,
                comment: "Define the reference to Conditions Booklet",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BillInvoiceNumber",
                schema: "Sadad",
                table: "BillArchive",
                maxLength: 50,
                nullable: true,
                comment: "Define the number of Bill",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "Sadad",
                table: "BillArchive",
                maxLength: 50,
                nullable: true,
                comment: "Define the Code for the agency that own the Tender",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Sadad",
                table: "BillArchive",
                nullable: false,
                comment: "Define  the Identity  for the table ",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<bool>(
                name: "IsZakatValidDate",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                nullable: false,
                comment: "Define   if the Zakat Certificate  is Valid ",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsZakatAttached",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                nullable: false,
                comment: "Define  if the Zakat Certificate Copy  is Attached with offer papers ",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSpecificationValidDate",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                nullable: true,
                comment: "Define   if the Specification Certificate  is Valid ",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSpecificationAttached",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                nullable: true,
                comment: "Define  if the Specification Copy is Attached with offer papers",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSocialInsuranceAttached",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                nullable: true,
                comment: "Define  if the Specification Insurance Copy  is Attached with offer papers ",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSaudizationValidDate",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                nullable: true,
                comment: "Define   if the Saudization Copy   Is Still valid",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSaudizationAttached",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                nullable: true,
                comment: "Define  if the Saudization Copy is Attached with offer papers",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCommercialRegisterValid",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                nullable: true,
                comment: "Define   if the Commercial Registeration    is Valid  ",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCommercialRegisterAttached",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                nullable: true,
                comment: "Define  if the Commercial Registeration number Copy is Attached with offer papers",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsChamberJoiningValid",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                nullable: true,
                comment: "Define   if the Chamber Join  Is Still valid",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsChamberJoiningAttached",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                nullable: true,
                comment: "Define  if the Chamber Join Request is Attached with offer papers",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CombinedId",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                nullable: false,
                comment: "Define  the supplier who applied for the tender",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CombinedDetailId",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                nullable: false,
                comment: "Define  the Identity  for the table ",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "OfferPriceAfterLocalContent",
                schema: "Offer",
                table: "Offer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OfferPriceAfterPreference",
                schema: "Offer",
                table: "Offer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePreferancePercentage",
                schema: "Offer",
                table: "Offer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isBindedToMandatoryList",
                schema: "Offer",
                table: "Offer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 31, 14, 26, 18, 187, DateTimeKind.Local).AddTicks(2839));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 31, 14, 26, 18, 187, DateTimeKind.Local).AddTicks(3581));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 31, 14, 26, 18, 187, DateTimeKind.Local).AddTicks(3602));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 31, 14, 26, 18, 187, DateTimeKind.Local).AddTicks(3603));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 31, 14, 26, 18, 187, DateTimeKind.Local).AddTicks(3605));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 31, 14, 26, 18, 187, DateTimeKind.Local).AddTicks(3606));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 31, 14, 26, 18, 187, DateTimeKind.Local).AddTicks(3607));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 31, 14, 26, 18, 187, DateTimeKind.Local).AddTicks(3608));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 31, 14, 26, 18, 187, DateTimeKind.Local).AddTicks(3610));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 31, 14, 26, 18, 187, DateTimeKind.Local).AddTicks(3611));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 31, 14, 26, 18, 187, DateTimeKind.Local).AddTicks(3612));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 31, 14, 26, 18, 187, DateTimeKind.Local).AddTicks(3613));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 31, 14, 26, 18, 187, DateTimeKind.Local).AddTicks(3615));

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
                name: "OfferPriceAfterLocalContent",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "OfferPriceAfterPreference",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "PricePreferancePercentage",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "isBindedToMandatoryList",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.AlterTable(
                name: "BillArchive",
                schema: "Sadad",
                oldComment: "Descripe the payment history for Conditional Booklet and Invitation");

            migrationBuilder.AlterTable(
                name: "SupplierCombinedDetail",
                schema: "Offer",
                oldComment: "Descripe the Details for suppliers [Offer owner and Combined]");

            migrationBuilder.AlterColumn<string>(
                name: "TenderReferenceNumber",
                schema: "Sadad",
                table: "BillArchive",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "Related Tender Reference Number");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                schema: "Sadad",
                table: "BillArchive",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define the related Tender");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierNumber",
                schema: "Sadad",
                table: "BillArchive",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "Define Supplier Commercial Number");

            migrationBuilder.AlterColumn<int>(
                name: "InvitationId",
                schema: "Sadad",
                table: "BillArchive",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "This refere the invitation ");

            migrationBuilder.AlterColumn<int>(
                name: "ConditionsBookletID",
                schema: "Sadad",
                table: "BillArchive",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true,
                oldComment: "Define the reference to Conditions Booklet");

            migrationBuilder.AlterColumn<string>(
                name: "BillInvoiceNumber",
                schema: "Sadad",
                table: "BillArchive",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "Define the number of Bill");

            migrationBuilder.AlterColumn<string>(
                name: "AgencyCode",
                schema: "Sadad",
                table: "BillArchive",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "Define the Code for the agency that own the Tender");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Sadad",
                table: "BillArchive",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define  the Identity  for the table ")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<bool>(
                name: "IsZakatValidDate",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define   if the Zakat Certificate  is Valid ");

            migrationBuilder.AlterColumn<bool>(
                name: "IsZakatAttached",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldComment: "Define  if the Zakat Certificate Copy  is Attached with offer papers ");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSpecificationValidDate",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define   if the Specification Certificate  is Valid ");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSpecificationAttached",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define  if the Specification Copy is Attached with offer papers");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSocialInsuranceAttached",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define  if the Specification Insurance Copy  is Attached with offer papers ");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSaudizationValidDate",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define   if the Saudization Copy   Is Still valid");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSaudizationAttached",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define  if the Saudization Copy is Attached with offer papers");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCommercialRegisterValid",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define   if the Commercial Registeration    is Valid  ");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCommercialRegisterAttached",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define  if the Commercial Registeration number Copy is Attached with offer papers");

            migrationBuilder.AlterColumn<bool>(
                name: "IsChamberJoiningValid",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define   if the Chamber Join  Is Still valid");

            migrationBuilder.AlterColumn<bool>(
                name: "IsChamberJoiningAttached",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldComment: "Define  if the Chamber Join Request is Attached with offer papers");

            migrationBuilder.AlterColumn<int>(
                name: "CombinedId",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define  the supplier who applied for the tender");

            migrationBuilder.AlterColumn<int>(
                name: "CombinedDetailId",
                schema: "Offer",
                table: "SupplierCombinedDetail",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldComment: "Define  the Identity  for the table ")
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
    }
}
