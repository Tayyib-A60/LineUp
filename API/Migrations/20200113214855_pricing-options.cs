using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class pricingoptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MinimumTerm",
                table: "Spaces",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SelectedPricingOption",
                table: "Spaces",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumTerm",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "SelectedPricingOption",
                table: "Spaces");
        }
    }
}
