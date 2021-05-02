using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class addtableofferlocalcontentdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferPriceAfterLocalContent",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "OfferPriceAfterPreference",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "PricePreferancePercentage",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "isBindedToMandatoryList",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.AddColumn<decimal>(
                name: "OfferFinalPrice",
                schema: "Offer",
                table: "Offer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfferLocalContentId",
                schema: "Offer",
                table: "Offer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OfferlocalContentDetails",
                columns: table => new
                {
                    OfferLocalContentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalContentPercentage = table.Column<decimal>(nullable: true),
                    LocalContentDesiredPercentage = table.Column<decimal>(nullable: true),
                    IsSmallOrMediumCorporation = table.Column<bool>(nullable: false),
                    IsIncludedInMoneyMarket = table.Column<bool>(nullable: false),
                    OfferPriceAfterLocalContent = table.Column<decimal>(nullable: true),
                    PricePreferancePercentage = table.Column<decimal>(nullable: true),
                    OfferPriceAfterSmallAndMediumCorporations = table.Column<decimal>(nullable: true),
                    IsBindedToMandatoryList = table.Column<bool>(nullable: false),
                    IsBindedToTheLowestBaseLine = table.Column<bool>(nullable: false),
                    IsBindedToTheLowestLocalContent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferlocalContentDetails", x => x.OfferLocalContentId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offer_OfferLocalContentId",
                schema: "Offer",
                table: "Offer",
                column: "OfferLocalContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_OfferlocalContentDetails_OfferLocalContentId",
                schema: "Offer",
                table: "Offer",
                column: "OfferLocalContentId",
                principalTable: "OfferlocalContentDetails",
                principalColumn: "OfferLocalContentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_OfferlocalContentDetails_OfferLocalContentId",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.DropTable(
                name: "OfferlocalContentDetails");

            migrationBuilder.DropIndex(
                name: "IX_Offer_OfferLocalContentId",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "OfferFinalPrice",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "OfferLocalContentId",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.AddColumn<decimal>(
                name: "OfferPriceAfterLocalContent",
                schema: "Offer",
                table: "Offer",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OfferPriceAfterPreference",
                schema: "Offer",
                table: "Offer",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePreferancePercentage",
                schema: "Offer",
                table: "Offer",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isBindedToMandatoryList",
                schema: "Offer",
                table: "Offer",
                type: "bit",
                nullable: false,
                defaultValue: false);

        }
    }
}
