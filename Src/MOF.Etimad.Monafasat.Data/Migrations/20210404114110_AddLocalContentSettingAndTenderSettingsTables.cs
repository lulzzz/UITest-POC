using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddLocalContentSettingAndTenderSettingsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NationalProductSettings",
                schema: "Settings");

            migrationBuilder.CreateTable(
                name: "LocalContentSetting",
                schema: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    NationalProductPercentage = table.Column<decimal>(nullable: false),
                    HighValueContractsAmmount = table.Column<decimal>(nullable: false),
                    LocalContentMaximumPercentage = table.Column<decimal>(nullable: false),
                    PriceWeightAfterAdjustment = table.Column<decimal>(nullable: false),
                    LocalContentWeightAndFinancialMarket = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalContentSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenderLocalContentSetting",
                schema: "Tender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenderId = table.Column<int>(nullable: false),
                    NationalProductPercentage = table.Column<decimal>(nullable: true),
                    HighValueContractsAmmount = table.Column<decimal>(nullable: true),
                    LocalContentMaximumPercentage = table.Column<decimal>(nullable: true),
                    PriceWeightAfterAdjustment = table.Column<decimal>(nullable: true),
                    LocalContentWeightAndFinancialMarket = table.Column<decimal>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalContentSetting",
                schema: "Settings");

            migrationBuilder.DropTable(
                name: "TenderLocalContentSetting",
                schema: "Tender");

            migrationBuilder.CreateTable(
                name: "NationalProductSettings",
                schema: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalProductSettings", x => x.Id);
                });
        }
    }
}
