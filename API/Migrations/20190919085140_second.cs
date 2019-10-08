using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenity_Spaces_SpaceId",
                table: "Amenity");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Bookings_BookingId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_SpaceType_TypeId",
                table: "Spaces");

            migrationBuilder.DropIndex(
                name: "IX_Chat_BookingId",
                table: "Chat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpaceType",
                table: "SpaceType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Amenity",
                table: "Amenity");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Chat");

            migrationBuilder.RenameTable(
                name: "SpaceType",
                newName: "SpaceTypes");

            migrationBuilder.RenameTable(
                name: "Amenity",
                newName: "Amenities");

            migrationBuilder.RenameIndex(
                name: "IX_Amenity_SpaceId",
                table: "Amenities",
                newName: "IX_Amenities_SpaceId");

            migrationBuilder.AddColumn<bool>(
                name: "EmailVerified",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Whatsapp",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpaceTypes",
                table: "SpaceTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Amenities",
                table: "Amenities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ChatMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MerchantId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    ChatId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessage_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ChatId",
                table: "Bookings",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_ChatId",
                table: "ChatMessage",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Spaces_SpaceId",
                table: "Amenities",
                column: "SpaceId",
                principalTable: "Spaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Chat_ChatId",
                table: "Bookings",
                column: "ChatId",
                principalTable: "Chat",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Spaces_SpaceId",
                table: "Amenities");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Chat_ChatId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Spaces_SpaceTypes_TypeId",
                table: "Spaces");

            migrationBuilder.DropTable(
                name: "ChatMessage");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ChatId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpaceTypes",
                table: "SpaceTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Amenities",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "EmailVerified",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Whatsapp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "SpaceTypes",
                newName: "SpaceType");

            migrationBuilder.RenameTable(
                name: "Amenities",
                newName: "Amenity");

            migrationBuilder.RenameIndex(
                name: "IX_Amenities_SpaceId",
                table: "Amenity",
                newName: "IX_Amenity_SpaceId");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Chat",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Chat",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Chat",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Chat",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpaceType",
                table: "SpaceType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Amenity",
                table: "Amenity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_BookingId",
                table: "Chat",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenity_Spaces_SpaceId",
                table: "Amenity",
                column: "SpaceId",
                principalTable: "Spaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Bookings_BookingId",
                table: "Chat",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Spaces_SpaceType_TypeId",
                table: "Spaces",
                column: "TypeId",
                principalTable: "SpaceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
