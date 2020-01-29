using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class RemovedUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Spaces",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Spaces",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
