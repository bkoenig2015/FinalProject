using Microsoft.EntityFrameworkCore.Migrations;

namespace TripLog.Migrations
{
    public partial class AddActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThingToDo1",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ThingToDo2",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ThingToDo3",
                table: "Trips");

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                });

            migrationBuilder.CreateTable(
                name: "TripActivity",
                columns: table => new
                {
                    TripId = table.Column<int>(nullable: false),
                    ActivityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripActivity", x => new { x.TripId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_TripActivity_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripActivity_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripActivity_ActivityId",
                table: "TripActivity",
                column: "ActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripActivity");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.AddColumn<string>(
                name: "ThingToDo1",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThingToDo2",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThingToDo3",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
