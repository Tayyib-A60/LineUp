using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class editedSpaceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SpaceTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "SpaceTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SpaceTypes");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "SpaceTypes");
        }
    }
}
