using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class addedIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_Location_LocationId",
                table: "Spaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_PricingOptions_SelectedPricingOptionId",
                table: "Spaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_SpaceTypes_TypeId",
                table: "Spaces");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Spaces",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SelectedPricingOptionId",
                table: "Spaces",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Spaces",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_Location_LocationId",
                table: "Spaces",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_PricingOptions_SelectedPricingOptionId",
                table: "Spaces",
                column: "SelectedPricingOptionId",
                principalTable: "PricingOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_SpaceTypes_TypeId",
                table: "Spaces",
                column: "TypeId",
                principalTable: "SpaceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_Location_LocationId",
                table: "Spaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_PricingOptions_SelectedPricingOptionId",
                table: "Spaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_SpaceTypes_TypeId",
                table: "Spaces");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Spaces",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SelectedPricingOptionId",
                table: "Spaces",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Spaces",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_Location_LocationId",
                table: "Spaces",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_PricingOptions_SelectedPricingOptionId",
                table: "Spaces",
                column: "SelectedPricingOptionId",
                principalTable: "PricingOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_SpaceTypes_TypeId",
                table: "Spaces",
                column: "TypeId",
                principalTable: "SpaceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
