using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddExtraDiscountColumnInNegotiationFirstStage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ExtraDiscountValue",
                schema: "CommunicationRequest",
                table: "Negotiation",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "NegotiationStatus",
                columns: new[] { "NegotiationStatusId", "Name" },
                values: new object[] { 50, " تم الرد بالموافقة مع تخفيض إضافي " });

            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "NegotiationSupplierStatus",
                columns: new[] { "NegotiationSupplierStatusId", "Name" },
                values: new object[] { 30, "تم الرد بالموافقة مع تخفيض إضافي " });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "NegotiationStatus",
                keyColumn: "NegotiationStatusId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "NegotiationSupplierStatus",
                keyColumn: "NegotiationSupplierStatusId",
                keyValue: 30);

            migrationBuilder.DropColumn(
                name: "ExtraDiscountValue",
                schema: "CommunicationRequest",
                table: "Negotiation");

        }
    }
}
