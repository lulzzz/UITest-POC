using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddLocalContentUpdatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<DateTime>(
                name: "BaseLineUpdatedDate",
                schema: "Offer",
                table: "OfferlocalContentDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CorporationSizeUpdatedDate",
                schema: "Offer",
                table: "OfferlocalContentDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "IsIncludedInMoneyMarketUpdatedDate",
                schema: "Offer",
                table: "OfferlocalContentDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LocalContentDesiredPercentageUpdatedDate",
                schema: "Offer",
                table: "OfferlocalContentDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "BaseLineUpdatedDate",
                schema: "Offer",
                table: "OfferlocalContentDetails");

            migrationBuilder.DropColumn(
                name: "CorporationSizeUpdatedDate",
                schema: "Offer",
                table: "OfferlocalContentDetails");

            migrationBuilder.DropColumn(
                name: "IsIncludedInMoneyMarketUpdatedDate",
                schema: "Offer",
                table: "OfferlocalContentDetails");

            migrationBuilder.DropColumn(
                name: "LocalContentDesiredPercentageUpdatedDate",
                schema: "Offer",
                table: "OfferlocalContentDetails");
        }
    }
}
