using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddLinkedAgencyToAnnouncementSupplierTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkedAgency",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkedAgency",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");
        }
    }
}
