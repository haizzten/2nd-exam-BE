using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2nd_exam_BE.Migrations
{
    public partial class FixAgreementtableaddDaysUntilExpirationfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DaysUntilExpiration",
                table: "Agreement",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysUntilExpiration",
                table: "Agreement");
        }
    }
}
