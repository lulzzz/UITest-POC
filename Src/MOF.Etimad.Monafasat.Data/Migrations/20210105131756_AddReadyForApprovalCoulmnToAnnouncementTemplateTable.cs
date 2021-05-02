using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddReadyForApprovalCoulmnToAnnouncementTemplateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReadyForApproval",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "AnnouncementTemplate",
                table: "AnnouncementStatusSupplierTemplate",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "بانتظار الاعتماد" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "AnnouncementTemplate",
                table: "AnnouncementStatusSupplierTemplate",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "ReadyForApproval",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");


        }
    }
}
