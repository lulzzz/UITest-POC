using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class Update_AnnouncementJoinRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cr",
                schema: "Announcement",
                table: "AnnouncementJoinRequest",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            //migrationBuilder.InsertData(
            //    schema: "Announcement",
            //    table: "AnnouncementStatus",
            //    columns: new[] { "Id", "Name" },
            //    values: new object[] { 4, "مرفوض" });

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 1,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(5388));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 2,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(6982));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 3,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7098));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 4,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7104));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 5,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7109));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 6,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7113));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 7,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7117));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 8,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7122));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 9,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7126));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 10,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7131));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 11,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7135));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 12,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7140));

            //migrationBuilder.UpdateData(
            //    schema: "LookUps",
            //    table: "TenderType",
            //    keyColumn: "TenderTypeId",
            //    keyValue: 13,
            //    column: "CreatedAt",
            //    value: new DateTime(2020, 6, 10, 17, 46, 0, 870, DateTimeKind.Local).AddTicks(7145));

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementJoinRequest_Cr",
                schema: "Announcement",
                table: "AnnouncementJoinRequest",
                column: "Cr");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementJoinRequest_Supplier_Cr",
                schema: "Announcement",
                table: "AnnouncementJoinRequest",
                column: "Cr",
                principalSchema: "IDM",
                principalTable: "Supplier",
                principalColumn: "SelectedCr",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementJoinRequest_Supplier_Cr",
                schema: "Announcement",
                table: "AnnouncementJoinRequest");

            migrationBuilder.DropIndex(
                name: "IX_AnnouncementJoinRequest_Cr",
                schema: "Announcement",
                table: "AnnouncementJoinRequest");

            migrationBuilder.DeleteData(
                schema: "Announcement",
                table: "AnnouncementStatus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Cr",
                schema: "Announcement",
                table: "AnnouncementJoinRequest",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 29, 12, 16, 27, 457, DateTimeKind.Local).AddTicks(3065));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 29, 12, 16, 27, 457, DateTimeKind.Local).AddTicks(5526));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 29, 12, 16, 27, 457, DateTimeKind.Local).AddTicks(5584));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 29, 12, 16, 27, 457, DateTimeKind.Local).AddTicks(5591));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 29, 12, 16, 27, 457, DateTimeKind.Local).AddTicks(5594));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 29, 12, 16, 27, 457, DateTimeKind.Local).AddTicks(5597));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 29, 12, 16, 27, 457, DateTimeKind.Local).AddTicks(5599));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 29, 12, 16, 27, 457, DateTimeKind.Local).AddTicks(5603));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 29, 12, 16, 27, 457, DateTimeKind.Local).AddTicks(5606));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 29, 12, 16, 27, 457, DateTimeKind.Local).AddTicks(5609));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 29, 12, 16, 27, 457, DateTimeKind.Local).AddTicks(5612));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 29, 12, 16, 27, 457, DateTimeKind.Local).AddTicks(5615));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 29, 12, 16, 27, 457, DateTimeKind.Local).AddTicks(5620));
        }
    }
}
