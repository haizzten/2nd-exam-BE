using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2nd_exam_BE.Migrations
{
    public partial class AddAgreementtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Agreement",
                newName: "QuoteNumber");

            migrationBuilder.AddColumn<string>(
                name: "AgreementName",
                table: "Agreement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AgreementType",
                table: "Agreement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Agreement",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DistributorName",
                table: "Agreement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                table: "Agreement",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Agreement",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgreementName",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "AgreementType",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "DistributorName",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Agreement");

            migrationBuilder.RenameColumn(
                name: "QuoteNumber",
                table: "Agreement",
                newName: "CustomerId");
        }
    }
}
