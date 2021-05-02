using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class AddNewBillArchiveTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "BillArchive",
                schema: "Sadad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    ConditionsBookletID = table.Column<int>(nullable: true),
                    InvitationId = table.Column<int>(nullable: true),
                    TenderId = table.Column<int>(nullable: false),
                    TenderReferenceNumber = table.Column<string>(maxLength: 100, nullable: true),
                    BillInvoiceNumber = table.Column<string>(maxLength: 50, nullable: true),
                    AgencyCode = table.Column<string>(maxLength: 50, nullable: true),
                    SupplierNumber = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillArchive", x => x.Id);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillArchive",
                schema: "Sadad");
        }
    }
}
