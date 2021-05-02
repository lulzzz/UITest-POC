using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class removecoulmnsFromBillTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillCategory",
                schema: "Sadad",
                table: "BillInfo");

            migrationBuilder.DropColumn(
                name: "BillCycle",
                schema: "Sadad",
                table: "BillInfo");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Sadad",
                table: "BillInfo");

            migrationBuilder.DropColumn(
                name: "DistrictCode",
                schema: "Sadad",
                table: "BillInfo");

            migrationBuilder.DropColumn(
                name: "EffectDate",
                schema: "Sadad",
                table: "BillInfo");

            migrationBuilder.DropColumn(
                name: "PaymentNotificationtDate",
                schema: "Sadad",
                table: "BillInfo");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                schema: "Sadad",
                table: "BillInfo");

            migrationBuilder.DropColumn(
                name: "ReconciledDt",
                schema: "Sadad",
                table: "BillInfo");

            migrationBuilder.DropColumn(
                name: "ReconciledStatus",
                schema: "Sadad",
                table: "BillInfo");

            migrationBuilder.DropColumn(
                name: "SadadBillId",
                schema: "Sadad",
                table: "BillInfo");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                schema: "Sadad",
                table: "BillInfo");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "Sadad",
                table: "BillInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillCategory",
                schema: "Sadad",
                table: "BillInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillCycle",
                schema: "Sadad",
                table: "BillInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Sadad",
                table: "BillInfo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictCode",
                schema: "Sadad",
                table: "BillInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectDate",
                schema: "Sadad",
                table: "BillInfo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentNotificationtDate",
                schema: "Sadad",
                table: "BillInfo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                schema: "Sadad",
                table: "BillInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReconciledDt",
                schema: "Sadad",
                table: "BillInfo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReconciledStatus",
                schema: "Sadad",
                table: "BillInfo",
                type: "char(20)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SadadBillId",
                schema: "Sadad",
                table: "BillInfo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                schema: "Sadad",
                table: "BillInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "Sadad",
                table: "BillInfo",
                type: "datetime2",
                nullable: true);
        }
    }
}
