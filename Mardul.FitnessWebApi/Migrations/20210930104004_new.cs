using Microsoft.EntityFrameworkCore.Migrations;

namespace Mardul.FitnessWebApi.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Workouts_WorkoutId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WorkoutId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserWorkout",
                columns: table => new
                {
                    UsersId = table.Column<string>(type: "TEXT", nullable: false),
                    WorkoutsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkout", x => new { x.UsersId, x.WorkoutsId });
                    table.ForeignKey(
                        name: "FK_UserWorkout_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWorkout_Workouts_WorkoutsId",
                        column: x => x.WorkoutsId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkout_WorkoutsId",
                table: "UserWorkout",
                column: "WorkoutsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWorkout");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WorkoutId",
                table: "AspNetUsers",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Workouts_WorkoutId",
                table: "AspNetUsers",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
