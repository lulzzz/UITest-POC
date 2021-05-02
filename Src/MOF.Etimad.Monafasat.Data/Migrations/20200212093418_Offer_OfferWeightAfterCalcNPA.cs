using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class Offer_OfferWeightAfterCalcNPA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.EnsureSchema(
            //    name: "Settings");

            migrationBuilder.AddColumn<decimal>(
                name: "OfferWeightAfterCalcNPA",
                schema: "Offer",
                table: "Offer",
                nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "NationalProductSettings",
            //    schema: "Settings",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CreatedAt = table.Column<DateTime>(nullable: false),
            //        CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
            //        UpdatedAt = table.Column<DateTime>(nullable: true),
            //        UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
            //        IsActive = table.Column<bool>(nullable: true),
            //        Rating = table.Column<decimal>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NationalProductSettings", x => x.Id);
            //    });

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 12, 12, 34, 16, 445, DateTimeKind.Local).AddTicks(6158));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 12, 12, 34, 16, 445, DateTimeKind.Local).AddTicks(7405));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 12, 12, 34, 16, 445, DateTimeKind.Local).AddTicks(7436));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 12, 12, 34, 16, 445, DateTimeKind.Local).AddTicks(7438));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 12, 12, 34, 16, 445, DateTimeKind.Local).AddTicks(7440));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 12, 12, 34, 16, 445, DateTimeKind.Local).AddTicks(7442));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 12, 12, 34, 16, 445, DateTimeKind.Local).AddTicks(7444));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 12, 12, 34, 16, 445, DateTimeKind.Local).AddTicks(7445));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 12, 12, 34, 16, 445, DateTimeKind.Local).AddTicks(7480));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 12, 12, 34, 16, 445, DateTimeKind.Local).AddTicks(7482));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 12, 12, 34, 16, 445, DateTimeKind.Local).AddTicks(7484));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 12, 12, 34, 16, 445, DateTimeKind.Local).AddTicks(7485));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 2, 12, 12, 34, 16, 445, DateTimeKind.Local).AddTicks(7487));

            //migrationBuilder.InsertData(
            //    schema: "LookUps",
            //    table: "TenderUnitUpdateType",
            //    columns: new[] { "TenderUnitUpdateTypeId", "Name" },
            //    values: new object[] { 7, "متطلبات المحتوى المحلي" });

            //migrationBuilder.InsertData(
            //    schema: "LookUps",
            //    table: "UserRole",
            //    columns: new[] { "UserRoleId", "DisplayNameAr", "DisplayNameEn", "Name" },
            //    values: new object[] { 43, "مسؤول متطلبات المحتوى المحلي", "Local Content Officer", "NewMonafasat_LocalContentOfficer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "NationalProductSettings",
            //    schema: "Settings");

            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "TenderUnitUpdateType",
                keyColumn: "TenderUnitUpdateTypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 43);

            migrationBuilder.DropColumn(
                name: "OfferWeightAfterCalcNPA",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 30, 10, 16, 46, 875, DateTimeKind.Local).AddTicks(8376));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 30, 10, 16, 46, 875, DateTimeKind.Local).AddTicks(9273));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 30, 10, 16, 46, 875, DateTimeKind.Local).AddTicks(9281));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 30, 10, 16, 46, 875, DateTimeKind.Local).AddTicks(9282));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 30, 10, 16, 46, 875, DateTimeKind.Local).AddTicks(9283));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 30, 10, 16, 46, 875, DateTimeKind.Local).AddTicks(9284));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 30, 10, 16, 46, 875, DateTimeKind.Local).AddTicks(9286));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 30, 10, 16, 46, 875, DateTimeKind.Local).AddTicks(9287));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 30, 10, 16, 46, 875, DateTimeKind.Local).AddTicks(9288));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 30, 10, 16, 46, 875, DateTimeKind.Local).AddTicks(9289));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 30, 10, 16, 46, 875, DateTimeKind.Local).AddTicks(9290));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 30, 10, 16, 46, 875, DateTimeKind.Local).AddTicks(9292));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 1, 30, 10, 16, 46, 875, DateTimeKind.Local).AddTicks(9293));
        }
    }
}
