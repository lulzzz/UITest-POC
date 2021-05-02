using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AnnouncementSupplierTemplateStatusData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "AnnouncementTemplate",
                table: "AnnouncementStatusSupplierTemplate",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "تحت الإنشاء" },
                    { 2, "بإنتظار الإعتماد" },
                    { 3, "معتمد" },
                    { 4, "مرفوض" },
                    { 5, "منهي" }
                });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "AnnouncementTemplate",
                table: "AnnouncementStatusSupplierTemplate",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "AnnouncementTemplate",
                table: "AnnouncementStatusSupplierTemplate",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "AnnouncementTemplate",
                table: "AnnouncementStatusSupplierTemplate",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "AnnouncementTemplate",
                table: "AnnouncementStatusSupplierTemplate",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "AnnouncementTemplate",
                table: "AnnouncementStatusSupplierTemplate",
                keyColumn: "Id",
                keyValue: 5);


        }
    }
}
