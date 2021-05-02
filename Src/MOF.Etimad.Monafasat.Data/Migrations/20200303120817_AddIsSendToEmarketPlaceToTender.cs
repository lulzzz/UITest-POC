using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddIsSendToEmarketPlaceToTender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSendToEmarketPlace",
                schema: "Tender",
                table: "Tender",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSendToEmarketPlace",
                schema: "Tender",
                table: "Tender");

        }
    }
}
