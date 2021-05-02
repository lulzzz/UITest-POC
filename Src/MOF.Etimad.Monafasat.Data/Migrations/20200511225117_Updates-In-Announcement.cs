using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class UpdatesInAnnouncement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementActivity_Tender_TenderId",
                schema: "Announcement",
                table: "AnnouncementActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementArea_Tender_TenderId",
                schema: "Announcement",
                table: "AnnouncementArea");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementCountry_Tender_TenderId",
                schema: "Announcement",
                table: "AnnouncementCountry");

            migrationBuilder.DropIndex(
                name: "IX_AnnouncementCountry_TenderId",
                schema: "Announcement",
                table: "AnnouncementCountry");

            migrationBuilder.DropIndex(
                name: "IX_AnnouncementArea_TenderId",
                schema: "Announcement",
                table: "AnnouncementArea");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnnouncementActivity",
                schema: "Announcement",
                table: "AnnouncementActivity");

            migrationBuilder.DropIndex(
                name: "IX_AnnouncementActivity_TenderId",
                schema: "Announcement",
                table: "AnnouncementActivity");

            migrationBuilder.DropColumn(
                name: "TenderId",
                schema: "Announcement",
                table: "AnnouncementCountry");

            migrationBuilder.DropColumn(
                name: "TenderId",
                schema: "Announcement",
                table: "AnnouncementArea");

            migrationBuilder.DropColumn(
                name: "TenderActivityId",
                schema: "Announcement",
                table: "AnnouncementActivity");

            migrationBuilder.DropColumn(
                name: "TenderId",
                schema: "Announcement",
                table: "AnnouncementActivity");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementCountry",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementArea",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementActivity",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnnouncementActivityId",
                schema: "Announcement",
                table: "AnnouncementActivity",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "IntroAboutTender",
                schema: "Announcement",
                table: "Announcement",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "AnnouncementName",
                schema: "Announcement",
                table: "Announcement",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AddColumn<int>(
                name: "ReasonForSelectingTenderTypeId",
                schema: "Announcement",
                table: "Announcement",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnnouncementActivity",
                schema: "Announcement",
                table: "AnnouncementActivity",
                column: "AnnouncementActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_ReasonForSelectingTenderTypeId",
                schema: "Announcement",
                table: "Announcement",
                column: "ReasonForSelectingTenderTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_ReasonForPurchaseTenderType_ReasonForSelectingTenderTypeId",
                schema: "Announcement",
                table: "Announcement",
                column: "ReasonForSelectingTenderTypeId",
                principalSchema: "Lookups",
                principalTable: "ReasonForPurchaseTenderType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_ReasonForPurchaseTenderType_ReasonForSelectingTenderTypeId",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnnouncementActivity",
                schema: "Announcement",
                table: "AnnouncementActivity");

            migrationBuilder.DropIndex(
                name: "IX_Announcement_ReasonForSelectingTenderTypeId",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "AnnouncementActivityId",
                schema: "Announcement",
                table: "AnnouncementActivity");

            migrationBuilder.DropColumn(
                name: "ReasonForSelectingTenderTypeId",
                schema: "Announcement",
                table: "Announcement");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementCountry",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TenderId",
                schema: "Announcement",
                table: "AnnouncementCountry",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementArea",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TenderId",
                schema: "Announcement",
                table: "AnnouncementArea",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                schema: "Announcement",
                table: "AnnouncementActivity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TenderActivityId",
                schema: "Announcement",
                table: "AnnouncementActivity",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "TenderId",
                schema: "Announcement",
                table: "AnnouncementActivity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "IntroAboutTender",
                schema: "Announcement",
                table: "Announcement",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AnnouncementName",
                schema: "Announcement",
                table: "Announcement",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnnouncementActivity",
                schema: "Announcement",
                table: "AnnouncementActivity",
                column: "TenderActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementCountry_TenderId",
                schema: "Announcement",
                table: "AnnouncementCountry",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementArea_TenderId",
                schema: "Announcement",
                table: "AnnouncementArea",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementActivity_TenderId",
                schema: "Announcement",
                table: "AnnouncementActivity",
                column: "TenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementActivity_Tender_TenderId",
                schema: "Announcement",
                table: "AnnouncementActivity",
                column: "TenderId",
                principalSchema: "Tender",
                principalTable: "Tender",
                principalColumn: "TenderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementArea_Tender_TenderId",
                schema: "Announcement",
                table: "AnnouncementArea",
                column: "TenderId",
                principalSchema: "Tender",
                principalTable: "Tender",
                principalColumn: "TenderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementCountry_Tender_TenderId",
                schema: "Announcement",
                table: "AnnouncementCountry",
                column: "TenderId",
                principalSchema: "Tender",
                principalTable: "Tender",
                principalColumn: "TenderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
