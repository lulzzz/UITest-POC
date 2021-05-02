using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddAnnouncementIdToTender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreAnnouncementId",
                schema: "Tender",
                table: "Tender",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 23, 24, 6, 330, DateTimeKind.Local).AddTicks(5355));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 23, 24, 6, 330, DateTimeKind.Local).AddTicks(6608));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 23, 24, 6, 330, DateTimeKind.Local).AddTicks(6635));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 23, 24, 6, 330, DateTimeKind.Local).AddTicks(6637));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 23, 24, 6, 330, DateTimeKind.Local).AddTicks(6639));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 23, 24, 6, 330, DateTimeKind.Local).AddTicks(6640));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 23, 24, 6, 330, DateTimeKind.Local).AddTicks(6641));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 23, 24, 6, 330, DateTimeKind.Local).AddTicks(6643));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 23, 24, 6, 330, DateTimeKind.Local).AddTicks(6644));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 23, 24, 6, 330, DateTimeKind.Local).AddTicks(6688));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 23, 24, 6, 330, DateTimeKind.Local).AddTicks(6690));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 23, 24, 6, 330, DateTimeKind.Local).AddTicks(6691));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 23, 24, 6, 330, DateTimeKind.Local).AddTicks(6693));

            migrationBuilder.CreateIndex(
                name: "IX_Tender_PreAnnouncementId",
                schema: "Tender",
                table: "Tender",
                column: "PreAnnouncementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tender_Announcement_PreAnnouncementId",
                schema: "Tender",
                table: "Tender",
                column: "PreAnnouncementId",
                principalSchema: "Announcement",
                principalTable: "Announcement",
                principalColumn: "AnnouncementId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tender_Announcement_PreAnnouncementId",
                schema: "Tender",
                table: "Tender");

            migrationBuilder.DropIndex(
                name: "IX_Tender_PreAnnouncementId",
                schema: "Tender",
                table: "Tender");

            migrationBuilder.DropColumn(
                name: "PreAnnouncementId",
                schema: "Tender",
                table: "Tender");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(5022));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6302));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6313));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6315));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6316));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6318));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6319));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6321));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6322));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6324));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6325));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6328));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6329));
        }
    }
}
