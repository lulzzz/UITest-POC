using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class removeFavSupplierListIDPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteSuppliers_Supplier_SupplierCRNumber",
                schema: "Supplier",
                table: "FavouriteSuppliers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_FavouriteSupplierTenders_FavouriteSupplierTenderId",
                schema: "Tender",
                table: "FavouriteSupplierTenders");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_FavouriteSuppliers_FavouriteSupplierId",
                schema: "Supplier",
                table: "FavouriteSuppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteSuppliers",
                schema: "Supplier",
                table: "FavouriteSuppliers");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierCRNumber",
                schema: "Supplier",
                table: "FavouriteSuppliers",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteSuppliers",
                schema: "Supplier",
                table: "FavouriteSuppliers",
                column: "FavouriteSupplierId");


            migrationBuilder.CreateIndex(
                name: "IX_FavouriteSuppliers_FavouriteSupplierListId",
                schema: "Supplier",
                table: "FavouriteSuppliers",
                column: "FavouriteSupplierListId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteSuppliers_Supplier_SupplierCRNumber",
                schema: "Supplier",
                table: "FavouriteSuppliers",
                column: "SupplierCRNumber",
                principalSchema: "IDM",
                principalTable: "Supplier",
                principalColumn: "SelectedCr",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteSuppliers_Supplier_SupplierCRNumber",
                schema: "Supplier",
                table: "FavouriteSuppliers");

            migrationBuilder.DropTable(
                name: "SP_GenerateReferenceNumber");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteSuppliers",
                schema: "Supplier",
                table: "FavouriteSuppliers");

            migrationBuilder.DropIndex(
                name: "IX_FavouriteSuppliers_FavouriteSupplierListId",
                schema: "Supplier",
                table: "FavouriteSuppliers");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierCRNumber",
                schema: "Supplier",
                table: "FavouriteSuppliers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FavouriteSupplierTenders_FavouriteSupplierTenderId",
                schema: "Tender",
                table: "FavouriteSupplierTenders",
                column: "FavouriteSupplierTenderId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FavouriteSuppliers_FavouriteSupplierId",
                schema: "Supplier",
                table: "FavouriteSuppliers",
                column: "FavouriteSupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteSuppliers",
                schema: "Supplier",
                table: "FavouriteSuppliers",
                columns: new[] { "FavouriteSupplierListId", "SupplierCRNumber" });


            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteSuppliers_Supplier_SupplierCRNumber",
                schema: "Supplier",
                table: "FavouriteSuppliers",
                column: "SupplierCRNumber",
                principalSchema: "IDM",
                principalTable: "Supplier",
                principalColumn: "SelectedCr",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
