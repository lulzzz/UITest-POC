using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class addJoinRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnouncementJoinRequestStatus",
                schema: "LookUps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    NameEn = table.Column<string>(maxLength: 1024, nullable: true),
                    NameAr = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementJoinRequestStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementJoinRequest",
                schema: "Announcement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    AnnouncementId = table.Column<int>(nullable: false),
                    Cr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementJoinRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementJoinRequest_Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalSchema: "Announcement",
                        principalTable: "Announcement",
                        principalColumn: "AnnouncementId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnnouncementJoinRequest_AnnouncementJoinRequestStatus_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "LookUps",
                        principalTable: "AnnouncementJoinRequestStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "AnnouncementJoinRequestStatus",
                columns: new[] { "Id", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, "جديد", "New" },
                    { 2, "مسحوب", "WithDrawn" }
                });

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 12, 37, 33, 161, DateTimeKind.Local).AddTicks(9419));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 12, 37, 33, 162, DateTimeKind.Local).AddTicks(889));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 12, 37, 33, 162, DateTimeKind.Local).AddTicks(915));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 12, 37, 33, 162, DateTimeKind.Local).AddTicks(915));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 12, 37, 33, 162, DateTimeKind.Local).AddTicks(915));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 12, 37, 33, 162, DateTimeKind.Local).AddTicks(920));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 12, 37, 33, 162, DateTimeKind.Local).AddTicks(920));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 12, 37, 33, 162, DateTimeKind.Local).AddTicks(920));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 12, 37, 33, 162, DateTimeKind.Local).AddTicks(920));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 12, 37, 33, 162, DateTimeKind.Local).AddTicks(920));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 12, 37, 33, 162, DateTimeKind.Local).AddTicks(925));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 12, 37, 33, 162, DateTimeKind.Local).AddTicks(925));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 12, 37, 33, 162, DateTimeKind.Local).AddTicks(925));

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementJoinRequest_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementJoinRequest",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementJoinRequest_StatusId",
                schema: "Announcement",
                table: "AnnouncementJoinRequest",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementJoinRequest",
                schema: "Announcement");

            migrationBuilder.DropTable(
                name: "AnnouncementJoinRequestStatus",
                schema: "LookUps");

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 2, 44, 58, 803, DateTimeKind.Local).AddTicks(1464));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 2, 44, 58, 803, DateTimeKind.Local).AddTicks(2932));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 2, 44, 58, 803, DateTimeKind.Local).AddTicks(2945));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 2, 44, 58, 803, DateTimeKind.Local).AddTicks(2947));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 2, 44, 58, 803, DateTimeKind.Local).AddTicks(2948));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 2, 44, 58, 803, DateTimeKind.Local).AddTicks(2950));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 2, 44, 58, 803, DateTimeKind.Local).AddTicks(2952));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 2, 44, 58, 803, DateTimeKind.Local).AddTicks(2953));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 2, 44, 58, 803, DateTimeKind.Local).AddTicks(2955));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 2, 44, 58, 803, DateTimeKind.Local).AddTicks(2956));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 2, 44, 58, 803, DateTimeKind.Local).AddTicks(2958));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 2, 44, 58, 803, DateTimeKind.Local).AddTicks(2959));

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "TenderType",
                keyColumn: "TenderTypeId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2020, 5, 11, 2, 44, 58, 803, DateTimeKind.Local).AddTicks(2961));
        }
    }
}
