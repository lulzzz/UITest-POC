using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddExtendDatesStatusInQgecnyCommunicationReque : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "AgencyCommunicationRequestStatus",
                columns: new[] { "Id", "Name" },
                values: new object[] { 13, "تم تمديد تواريخ المنافسة" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "AgencyCommunicationRequestStatus",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
