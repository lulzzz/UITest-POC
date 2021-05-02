using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class InitAnnouncement2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementActivity_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementArea_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementArea");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementConstructionWork_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementConstructionWork");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementCountry_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementCountry");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementHistory_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementMaintenanceRunnigWork_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Announcement",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.AddColumn<int>(
                name: "AnnouncementId",
                schema: "Announcement",
                table: "Announcement",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                schema: "Announcement",
                table: "Announcement",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TenderTypeId",
                schema: "Announcement",
                table: "Announcement",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Announcement",
                schema: "Announcement",
                table: "Announcement",
                column: "AnnouncementId");

            migrationBuilder.InsertData(
                schema: "Announcement",
                table: "AnnouncementStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "تحت الإنشاء" },
                    { 2, "بإنتظار الإعتماد" },
                    { 3, "معتمد" },
                    { 4, "منهي" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_TenderTypeId",
                schema: "Announcement",
                table: "Announcement",
                column: "TenderTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_TenderType_TenderTypeId",
                schema: "Announcement",
                table: "Announcement",
                column: "TenderTypeId",
                principalSchema: "LookUps",
                principalTable: "TenderType",
                principalColumn: "TenderTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementActivity_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementActivity",
                column: "AnnouncementId",
                principalSchema: "Announcement",
                principalTable: "Announcement",
                principalColumn: "AnnouncementId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementArea_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementArea",
                column: "AnnouncementId",
                principalSchema: "Announcement",
                principalTable: "Announcement",
                principalColumn: "AnnouncementId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementConstructionWork_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementConstructionWork",
                column: "AnnouncementId",
                principalSchema: "Announcement",
                principalTable: "Announcement",
                principalColumn: "AnnouncementId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementCountry_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementCountry",
                column: "AnnouncementId",
                principalSchema: "Announcement",
                principalTable: "Announcement",
                principalColumn: "AnnouncementId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementHistory_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementHistory",
                column: "AnnouncementId",
                principalSchema: "Announcement",
                principalTable: "Announcement",
                principalColumn: "AnnouncementId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementMaintenanceRunnigWork_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                column: "AnnouncementId",
                principalSchema: "Announcement",
                principalTable: "Announcement",
                principalColumn: "AnnouncementId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_TenderType_TenderTypeId",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementActivity_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementArea_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementArea");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementConstructionWork_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementConstructionWork");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementCountry_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementCountry");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementHistory_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementMaintenanceRunnigWork_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Announcement",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DropIndex(
                name: "IX_Announcement_TenderTypeId",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DeleteData(
                schema: "Announcement",
                table: "AnnouncementStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Announcement",
                table: "AnnouncementStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Announcement",
                table: "AnnouncementStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Announcement",
                table: "AnnouncementStatus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "AnnouncementId",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "PublishedDate",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "TenderTypeId",
                schema: "Announcement",
                table: "Announcement");


            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "Announcement",
                table: "Announcement",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Announcement",
                schema: "Announcement",
                table: "Announcement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementActivity_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementActivity",
                column: "AnnouncementId",
                principalSchema: "Announcement",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementArea_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementArea",
                column: "AnnouncementId",
                principalSchema: "Announcement",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementConstructionWork_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementConstructionWork",
                column: "AnnouncementId",
                principalSchema: "Announcement",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementCountry_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementCountry",
                column: "AnnouncementId",
                principalSchema: "Announcement",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementHistory_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementHistory",
                column: "AnnouncementId",
                principalSchema: "Announcement",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementMaintenanceRunnigWork_Announcement_AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementMaintenanceRunnigWork",
                column: "AnnouncementId",
                principalSchema: "Announcement",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
