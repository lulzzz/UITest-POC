using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class RemoveFinalGuaranteePercentage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalGuaranteePercentage",
                schema: "Tender",
                table: "Tender");

            migrationBuilder.AlterColumn<decimal>(
                name: "AwardingDiscountPercentage",
                schema: "Tender",
                table: "Tender",
                nullable: true,
                comment: "Value of the final guarantee",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Value of the final guarantee");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AwardingDiscountPercentage",
                schema: "Tender",
                table: "Tender",
                type: "int",
                nullable: true,
                comment: "Value of the final guarantee",
                oldClrType: typeof(decimal),
                oldNullable: true,
                oldComment: "Value of the final guarantee");

            migrationBuilder.AddColumn<decimal>(
                name: "FinalGuaranteePercentage",
                schema: "Tender",
                table: "Tender",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
