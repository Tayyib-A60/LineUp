using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class addedPublicId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "discount",
                table: "Pricing",
                newName: "Discount");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Photos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Photos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Pricing",
                newName: "discount");
        }
    }
}
