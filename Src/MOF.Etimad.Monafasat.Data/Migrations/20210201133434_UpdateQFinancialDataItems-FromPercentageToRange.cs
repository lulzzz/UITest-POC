using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class UpdateQFinancialDataItemsFromPercentageToRange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                schema: "Qualification",
                table: "QualificationItem",
                keyColumn: "ID",
                keyValue: 19,
                column: "QualificationItemTypeId",
                value: 2);

            migrationBuilder.UpdateData(
                schema: "Qualification",
                table: "QualificationItem",
                keyColumn: "ID",
                keyValue: 20,
                column: "QualificationItemTypeId",
                value: 2);

            migrationBuilder.UpdateData(
                schema: "Qualification",
                table: "QualificationItem",
                keyColumn: "ID",
                keyValue: 21,
                column: "QualificationItemTypeId",
                value: 2);

            migrationBuilder.UpdateData(
                schema: "Qualification",
                table: "QualificationItem",
                keyColumn: "ID",
                keyValue: 28,
                column: "QualificationItemTypeId",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                schema: "Qualification",
                table: "QualificationItem",
                keyColumn: "ID",
                keyValue: 19,
                column: "QualificationItemTypeId",
                value: 3);

            migrationBuilder.UpdateData(
                schema: "Qualification",
                table: "QualificationItem",
                keyColumn: "ID",
                keyValue: 20,
                column: "QualificationItemTypeId",
                value: 3);

            migrationBuilder.UpdateData(
                schema: "Qualification",
                table: "QualificationItem",
                keyColumn: "ID",
                keyValue: 21,
                column: "QualificationItemTypeId",
                value: 3);

            migrationBuilder.UpdateData(
                schema: "Qualification",
                table: "QualificationItem",
                keyColumn: "ID",
                keyValue: 28,
                column: "QualificationItemTypeId",
                value: 3);
        }
    }
}
