using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddAnnouncementInVerificationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                schema: "Verification",
                table: "VerificationType",
                columns: new[] { "VerificationTypeId", "VerificationTypeName" },
                values: new object[] { 7, "الإعلان" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                schema: "Verification",
                table: "VerificationType",
                keyColumn: "VerificationTypeId",
                keyValue: 7);

        }
    }
}
