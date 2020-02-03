using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class spaceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdOfSpaceBooked",
                table: "Bookings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdOfSpaceBooked",
                table: "Bookings");
        }
    }
}
