using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class removeAuditableEntityFromEstablishmentTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "LookUps",
                table: "EstablishmentType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "LookUps",
                table: "EstablishmentType");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "LookUps",
                table: "EstablishmentType");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "LookUps",
                table: "EstablishmentType");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "LookUps",
                table: "EstablishmentType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "LookUps",
                table: "EstablishmentType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "LookUps",
                table: "EstablishmentType",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "LookUps",
                table: "EstablishmentType",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "LookUps",
                table: "EstablishmentType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "LookUps",
                table: "EstablishmentType",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
