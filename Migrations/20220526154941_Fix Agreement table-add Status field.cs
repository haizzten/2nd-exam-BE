using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2nd_exam_BE.Migrations
{
    public partial class FixAgreementtableaddStatusfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Agreement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Agreement");
        }
    }
}
