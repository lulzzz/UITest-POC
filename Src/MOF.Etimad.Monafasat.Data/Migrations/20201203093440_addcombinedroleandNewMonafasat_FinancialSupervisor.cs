using Microsoft.EntityFrameworkCore.Migrations;

namespace MOF.Etimad.Monafasat.Data.Migrations
{
    public partial class addcombinedroleandNewMonafasat_FinancialSupervisor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCombinedRole",
                schema: "LookUps",
                table: "UserRole",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                schema: "LookUps",
                table: "UserRole",
                columns: new[] { "UserRoleId", "DisplayNameAr", "DisplayNameEn", "Name", "IsCombinedRole" },
                values: new object[,]
                {
                    { 44, "المراقب المالى ", "Financial Supervisor", "NewMonafasat_FinancialSupervisor",false },
                    { 100, "عضولجنة الشراء المباشر", "Direct Purchase Member", "CR_DirectPurchaseMember",true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                schema: "LookUps",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: 100);

            migrationBuilder.DropColumn(
                name: "IsCombinedRole",
                schema: "LookUps",
                table: "UserRole");
        }
    }
}
