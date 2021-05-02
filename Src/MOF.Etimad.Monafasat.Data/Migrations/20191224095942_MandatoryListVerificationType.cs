using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class MandatoryListVerificationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Verification",
                table: "VerificationType",
                columns: new[] { "VerificationTypeId", "VerificationTypeName" },
                values: new object[] { 6, "القائوة الالزامية" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Verification",
                table: "VerificationType",
                keyColumn: "VerificationTypeId",
                keyValue: 6);
        }
    }
}
