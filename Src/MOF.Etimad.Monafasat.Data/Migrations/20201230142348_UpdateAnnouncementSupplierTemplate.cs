using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class UpdateAnnouncementSupplierTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgencyName",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.DropColumn(
                name: "AnnouncementListType",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.AddColumn<int>(
                name: "AnnouncementTemplateSuppliersListTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SP_AddQuantityItemsToTenderQT",
                columns: table => new
                {
                    ItemsJsonId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementTemplateListType",
                schema: "LookUps",
                columns: table => new
                {
                    AnnouncementTemplateSuppliersListTypeId = table.Column<int>(nullable: false),
                    NameAr = table.Column<string>(maxLength: 100, nullable: true),
                    NameEn = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementTemplateListType", x => x.AnnouncementTemplateSuppliersListTypeId);
                });

            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "AnnouncementTemplateListType",
                columns: new[] { "AnnouncementTemplateSuppliersListTypeId", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, "عامه", null },
                    { 2, "خاصة", null }
                });

            migrationBuilder.UpdateData(
                schema: "LookUps",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 100,
                column: "IsCombinedRole",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementSupplierTemplate_AnnouncementTemplateSuppliersListTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                column: "AnnouncementTemplateSuppliersListTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementSupplierTemplate_AnnouncementTemplateListType_AnnouncementTemplateSuppliersListTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                column: "AnnouncementTemplateSuppliersListTypeId",
                principalSchema: "LookUps",
                principalTable: "AnnouncementTemplateListType",
                principalColumn: "AnnouncementTemplateSuppliersListTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementSupplierTemplate_AnnouncementTemplateListType_AnnouncementTemplateSuppliersListTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.DropTable(
                name: "SP_AddQuantityItemsToTenderQT");

            migrationBuilder.DropTable(
                name: "AnnouncementTemplateListType",
                schema: "LookUps");

            migrationBuilder.DropIndex(
                name: "IX_AnnouncementSupplierTemplate_AnnouncementTemplateSuppliersListTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.DropColumn(
                name: "AnnouncementTemplateSuppliersListTypeId",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.DropColumn(
                name: "Details",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate");

            migrationBuilder.AddColumn<string>(
                name: "AgencyName",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnnouncementListType",
                schema: "AnnouncementTemplate",
                table: "AnnouncementSupplierTemplate",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
