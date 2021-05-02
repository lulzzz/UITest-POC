using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AnnouncementStatusSupplierTemplate_Canceled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "AnnouncementTemplate",
                table: "AnnouncementStatusSupplierTemplate",
                columns: new[] { "Id", "Name" },
                values: new object[] { 7, "ملغي" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "AnnouncementTemplate",
                table: "AnnouncementStatusSupplierTemplate",
                keyColumn: "Id",
                keyValue: 7);

        }
    }
}
