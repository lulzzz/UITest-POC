using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class Update_Announcement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgencyCode",
                schema: "Announcement",
                table: "Announcement",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                schema: "Announcement",
                table: "Announcement",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                schema: "Announcement",
                table: "Announcement",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                schema: "Announcement",
                table: "Announcement",
                maxLength: 100,
                nullable: true);

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 1,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(5022));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 2,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6302));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 3,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6313));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 4,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6315));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 5,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6316));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 6,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6318));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 7,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6319));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 8,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6321));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 9,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6322));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 10,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6324));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 11,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6325));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 12,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6328));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 13,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 5, 8, 7, 36, 7, 858, DateTimeKind.Local).AddTicks(6329));

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_AgencyCode",
                schema: "Announcement",
                table: "Announcement",
                column: "AgencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_BranchId",
                schema: "Announcement",
                table: "Announcement",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_GovAgency_AgencyCode",
                schema: "Announcement",
                table: "Announcement",
                column: "AgencyCode",
                principalSchema: "IDM",
                principalTable: "GovAgency",
                principalColumn: "AgencyCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_Branch_BranchId",
                schema: "Announcement",
                table: "Announcement",
                column: "BranchId",
                principalSchema: "Branch",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_GovAgency_AgencyCode",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_Branch_BranchId",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DropIndex(
                name: "IX_Announcement_AgencyCode",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DropIndex(
                name: "IX_Announcement_BranchId",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "AgencyCode",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 5, 17, 4, 34, 124, DateTimeKind.Local).AddTicks(5125));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 5, 17, 4, 34, 124, DateTimeKind.Local).AddTicks(6574));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 5, 17, 4, 34, 124, DateTimeKind.Local).AddTicks(6600));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 5, 17, 4, 34, 124, DateTimeKind.Local).AddTicks(6600));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 5, 17, 4, 34, 124, DateTimeKind.Local).AddTicks(6600));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 5, 17, 4, 34, 124, DateTimeKind.Local).AddTicks(6600));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 5, 17, 4, 34, 124, DateTimeKind.Local).AddTicks(6605));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 5, 17, 4, 34, 124, DateTimeKind.Local).AddTicks(6605));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 5, 17, 4, 34, 124, DateTimeKind.Local).AddTicks(6605));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 5, 17, 4, 34, 124, DateTimeKind.Local).AddTicks(6610));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 5, 17, 4, 34, 124, DateTimeKind.Local).AddTicks(6610));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 5, 17, 4, 34, 124, DateTimeKind.Local).AddTicks(6610));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 5, 17, 4, 34, 124, DateTimeKind.Local).AddTicks(6610));
        }
    }
}
