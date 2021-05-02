using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class addLocalContentAttachmentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "AttachmentType",
                columns: new[] { "AttachmentTypeId", "NameAr", "NameEn" },
                values: new object[] { 28, "LocalContent", null });

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "AttachmentType",
                keyColumn: "AttachmentTypeId",
                keyValue: 28);

         
        }
    }
}
