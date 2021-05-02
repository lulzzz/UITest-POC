using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class Updateofferdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_OfferlocalContentDetails_OfferLocalContentId",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.DropIndex(
                name: "IX_Offer_OfferLocalContentId",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "OfferLocalContentId",
                schema: "Offer",
                table: "Offer");

            migrationBuilder.RenameTable(
                name: "OfferlocalContentDetails",
                newName: "OfferlocalContentDetails",
                newSchema: "Offer");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSmallOrMediumCorporation",
                schema: "Offer",
                table: "OfferlocalContentDetails",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsIncludedInMoneyMarket",
                schema: "Offer",
                table: "OfferlocalContentDetails",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                schema: "Offer",
                table: "OfferlocalContentDetails",
                nullable: false,
                defaultValue: 0);


            migrationBuilder.CreateIndex(
                name: "IX_OfferlocalContentDetails_OfferId",
                schema: "Offer",
                table: "OfferlocalContentDetails",
                column: "OfferId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferlocalContentDetails_Offer_OfferId",
                schema: "Offer",
                table: "OfferlocalContentDetails",
                column: "OfferId",
                principalSchema: "Offer",
                principalTable: "Offer",
                principalColumn: "OfferId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferlocalContentDetails_Offer_OfferId",
                schema: "Offer",
                table: "OfferlocalContentDetails");

            migrationBuilder.DropIndex(
                name: "IX_OfferlocalContentDetails_OfferId",
                schema: "Offer",
                table: "OfferlocalContentDetails");

            migrationBuilder.DropColumn(
                name: "OfferId",
                schema: "Offer",
                table: "OfferlocalContentDetails");

            migrationBuilder.RenameTable(
                name: "OfferlocalContentDetails",
                schema: "Offer",
                newName: "OfferlocalContentDetails");

            migrationBuilder.AddColumn<int>(
                name: "OfferLocalContentId",
                schema: "Offer",
                table: "Offer",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSmallOrMediumCorporation",
                table: "OfferlocalContentDetails",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsIncludedInMoneyMarket",
                table: "OfferlocalContentDetails",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

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
    }
}
