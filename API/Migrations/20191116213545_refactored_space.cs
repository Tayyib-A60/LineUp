using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class refactored_space : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Spaces");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Spaces",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PricePDId",
                table: "Spaces",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PricePHId",
                table: "Spaces",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PricePWId",
                table: "Spaces",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Long = table.Column<string>(nullable: true),
                    Lat = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pricing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Price = table.Column<double>(nullable: false),
                    discount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricing", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_LocationId",
                table: "Spaces",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_PricePDId",
                table: "Spaces",
                column: "PricePDId");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_PricePHId",
                table: "Spaces",
                column: "PricePHId");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_PricePWId",
                table: "Spaces",
                column: "PricePWId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_Location_LocationId",
                table: "Spaces",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_Pricing_PricePDId",
                table: "Spaces",
                column: "PricePDId",
                principalTable: "Pricing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_Pricing_PricePHId",
                table: "Spaces",
                column: "PricePHId",
                principalTable: "Pricing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_Pricing_PricePWId",
                table: "Spaces",
                column: "PricePWId",
                principalTable: "Pricing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_Location_LocationId",
                table: "Spaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_Pricing_PricePDId",
                table: "Spaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_Pricing_PricePHId",
                table: "Spaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_Pricing_PricePWId",
                table: "Spaces");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Pricing");

            migrationBuilder.DropIndex(
                name: "IX_Spaces_LocationId",
                table: "Spaces");

            migrationBuilder.DropIndex(
                name: "IX_Spaces_PricePDId",
                table: "Spaces");

            migrationBuilder.DropIndex(
                name: "IX_Spaces_PricePHId",
                table: "Spaces");

            migrationBuilder.DropIndex(
                name: "IX_Spaces_PricePWId",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "PricePDId",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "PricePHId",
                table: "Spaces");

            migrationBuilder.DropColumn(
                name: "PricePWId",
                table: "Spaces");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Spaces",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Spaces",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
