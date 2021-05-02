using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class addIsNotificationSentForStoppingPeriodTotenderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinishedStoppingAwardingPeriod",
                schema: "Tender",
                table: "Tender");

            migrationBuilder.AddColumn<bool>(
                name: "IsNotificationSentForStoppingPeriod",
                schema: "Tender",
                table: "Tender",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNotificationSentForStoppingPeriod",
                schema: "Tender",
                table: "Tender");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinishedStoppingAwardingPeriod",
                schema: "Tender",
                table: "Tender",
                type: "bit",
                nullable: true);
        }
    }
}
