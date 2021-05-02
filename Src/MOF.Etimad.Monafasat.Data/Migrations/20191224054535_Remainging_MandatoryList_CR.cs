using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class Remainging_MandatoryList_CR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OriginalEntityId",
                schema: "MandatoryList",
                table: "MandatoryListChangeRequest",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MandatoryListChangeRequest_OriginalEntityId",
                schema: "MandatoryList",
                table: "MandatoryListChangeRequest",
                column: "OriginalEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MandatoryListChangeRequest_MandatoryList_OriginalEntityId",
                schema: "MandatoryList",
                table: "MandatoryListChangeRequest",
                column: "OriginalEntityId",
                principalSchema: "MandatoryList",
                principalTable: "MandatoryList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MandatoryListChangeRequest_MandatoryList_OriginalEntityId",
                schema: "MandatoryList",
                table: "MandatoryListChangeRequest");

            migrationBuilder.DropIndex(
                name: "IX_MandatoryListChangeRequest_OriginalEntityId",
                schema: "MandatoryList",
                table: "MandatoryListChangeRequest");

            migrationBuilder.DropColumn(
                name: "OriginalEntityId",
                schema: "MandatoryList",
                table: "MandatoryListChangeRequest");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 23, 14, 44, 36, 822, DateTimeKind.Local).AddTicks(6528));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 23, 14, 44, 36, 822, DateTimeKind.Local).AddTicks(8003));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 23, 14, 44, 36, 822, DateTimeKind.Local).AddTicks(8018));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 23, 14, 44, 36, 822, DateTimeKind.Local).AddTicks(8023));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 23, 14, 44, 36, 822, DateTimeKind.Local).AddTicks(8026));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 23, 14, 44, 36, 822, DateTimeKind.Local).AddTicks(8029));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 23, 14, 44, 36, 822, DateTimeKind.Local).AddTicks(8032));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 23, 14, 44, 36, 822, DateTimeKind.Local).AddTicks(8035));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 23, 14, 44, 36, 822, DateTimeKind.Local).AddTicks(8038));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 23, 14, 44, 36, 822, DateTimeKind.Local).AddTicks(8042));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 23, 14, 44, 36, 822, DateTimeKind.Local).AddTicks(8045));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 23, 14, 44, 36, 822, DateTimeKind.Local).AddTicks(8048));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2019, 12, 23, 14, 44, 36, 822, DateTimeKind.Local).AddTicks(8051));
        }
    }
}
