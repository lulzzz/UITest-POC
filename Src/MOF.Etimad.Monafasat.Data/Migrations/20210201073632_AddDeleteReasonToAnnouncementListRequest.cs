using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddDeleteReasonToAnnouncementListRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkedAgency",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.AddColumn<string>(
                name: "DeleteReason",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate",
                maxLength: 2000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteReason",
                schema: "AnnouncementTemplate",
                table: "AnnouncementRequestSupplierTemplate");

            migrationBuilder.AddColumn<string>(
                name: "LinkedAgency",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
