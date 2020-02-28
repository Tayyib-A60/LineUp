using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class additionalpropsbkn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookedForEmail",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookedForName",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookedForPhone",
                table: "Bookings",
                nullable: true);
            // migrationBuilder.UpdateData("Bookings", "Id", "BookedForEmail", "client@mail.com");
            // migrationBuilder.UpdateData("Bookings", "Id", "BookedForName", "Client Name");
            // migrationBuilder.UpdateData("Bookings", "Id", "BookedForPhone", "08012345678");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookedForEmail",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookedForName",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookedForPhone",
                table: "Bookings");
        }
    }
}
