using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class RemoveTenderLocalContentSettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenderLocalContentSetting",
                schema: "Tender");

            migrationBuilder.AddColumn<decimal>(
                name: "BaselineWeight",
                schema: "Tender",
                table: "TenderLocalContent",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FinancialMarketListedWeight",
                schema: "Tender",
                table: "TenderLocalContent",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HighValueContractsAmmount",
                schema: "Tender",
                table: "TenderLocalContent",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LocalContentMaximumPercentage",
                schema: "Tender",
                table: "TenderLocalContent",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LocalContentTargetWeight",
                schema: "Tender",
                table: "TenderLocalContent",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LocalContentWeightAndFinancialMarket",
                schema: "Tender",
                table: "TenderLocalContent",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NationalProductPercentage",
                schema: "Tender",
                table: "TenderLocalContent",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceWeightAfterAdjustment",
                schema: "Tender",
                table: "TenderLocalContent",
                nullable: true);

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaselineWeight",
                schema: "Tender",
                table: "TenderLocalContent");

            migrationBuilder.DropColumn(
                name: "FinancialMarketListedWeight",
                schema: "Tender",
                table: "TenderLocalContent");

            migrationBuilder.DropColumn(
                name: "HighValueContractsAmmount",
                schema: "Tender",
                table: "TenderLocalContent");

            migrationBuilder.DropColumn(
                name: "LocalContentMaximumPercentage",
                schema: "Tender",
                table: "TenderLocalContent");

            migrationBuilder.DropColumn(
                name: "LocalContentTargetWeight",
                schema: "Tender",
                table: "TenderLocalContent");

            migrationBuilder.DropColumn(
                name: "LocalContentWeightAndFinancialMarket",
                schema: "Tender",
                table: "TenderLocalContent");

            migrationBuilder.DropColumn(
                name: "NationalProductPercentage",
                schema: "Tender",
                table: "TenderLocalContent");

            migrationBuilder.DropColumn(
                name: "PriceWeightAfterAdjustment",
                schema: "Tender",
                table: "TenderLocalContent");

            migrationBuilder.CreateTable(
                name: "TenderLocalContentSetting",
                schema: "Tender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HighValueContractsAmmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LocalContentMaximumPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LocalContentWeightAndFinancialMarket = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NationalProductPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceWeightAfterAdjustment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderLocalContentSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderLocalContentSetting_Tender_TenderId",
                        column: x => x.TenderId,
                        principalSchema: "Tender",
                        principalTable: "Tender",
                        principalColumn: "TenderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenderLocalContentSetting_TenderId",
                schema: "Tender",
                table: "TenderLocalContentSetting",
                column: "TenderId",
                unique: true);
        }
    }
}
