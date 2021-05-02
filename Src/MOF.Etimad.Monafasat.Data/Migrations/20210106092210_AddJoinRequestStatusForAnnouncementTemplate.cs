using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddJoinRequestStatusForAnnouncementTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "AnnouncementJoinRequestStatusSupplierTemplate",
                columns: new[] { "Id", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, "تم الارسال", "Request Sent" },
                    { 2, "بنتظار القبول", "Pending Acceptance" },
                    { 3, "بانتظار الرفض", "Pending Rejection" },
                    { 4, "مقبول", "Accepted" },
                    { 5, "مرفوض", "Rejected" },
                    { 6, "منسحب", "Withdrawn" },
                    { 7, "محذوف", "Deleted" }
                });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "AnnouncementJoinRequestStatusSupplierTemplate",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "AnnouncementJoinRequestStatusSupplierTemplate",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "AnnouncementJoinRequestStatusSupplierTemplate",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "AnnouncementJoinRequestStatusSupplierTemplate",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "AnnouncementJoinRequestStatusSupplierTemplate",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "AnnouncementJoinRequestStatusSupplierTemplate",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "AnnouncementJoinRequestStatusSupplierTemplate",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
