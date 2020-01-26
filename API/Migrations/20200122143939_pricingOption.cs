using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class pricingOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedPricingOption",
                table: "Spaces");

            migrationBuilder.AddColumn<int>(
                name: "SelectedPricingOptionId",
                table: "Spaces",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PricingOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Option = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingOptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_SelectedPricingOptionId",
                table: "Spaces",
                column: "SelectedPricingOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_PricingOptions_SelectedPricingOptionId",
                table: "Spaces",
                column: "SelectedPricingOptionId",
                principalTable: "PricingOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_PricingOptions_SelectedPricingOptionId",
                table: "Spaces");

            migrationBuilder.DropTable(
                name: "PricingOptions");

            migrationBuilder.DropIndex(
                name: "IX_Spaces_SelectedPricingOptionId",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "SelectedPricingOptionId",
                table: "Spaces");

            migrationBuilder.AddColumn<int>(
                name: "SelectedPricingOption",
                table: "Spaces",
                nullable: false,
                defaultValue: 0);
        }
    }
}
