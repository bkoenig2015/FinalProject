using Microsoft.EntityFrameworkCore.Migrations;

namespace TripLog.Migrations
{
    public partial class AddAccommodation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accommodation",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "AccommodationEmail",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "AccommodationPhone",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "AccommodationId",
                table: "Trips",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    AccommodationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.AccommodationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_AccommodationId",
                table: "Trips",
                column: "AccommodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Accommodations_AccommodationId",
                table: "Trips",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "AccommodationId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Accommodations_AccommodationId",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropIndex(
                name: "IX_Trips_AccommodationId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "AccommodationId",
                table: "Trips");

            migrationBuilder.AddColumn<string>(
                name: "Accommodation",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccommodationEmail",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccommodationPhone",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
