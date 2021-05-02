using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddIsTawreedColumnInTenderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
                name: "IsTenderContainsTawreedTables",
                schema: "Tender",
                table: "Tender",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
       
            migrationBuilder.DropColumn(
                name: "IsTenderContainsTawreedTables",
                schema: "Tender",
                table: "Tender");

        }
    }
}
