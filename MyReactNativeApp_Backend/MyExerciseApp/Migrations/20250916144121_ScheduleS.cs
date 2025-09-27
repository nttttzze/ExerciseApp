using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyExerciseApp.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Workout",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShceduledWorkout = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workout_ScheduleId",
                table: "Workout",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_Schedules_ScheduleId",
                table: "Workout",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "ScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_Schedules_ScheduleId",
                table: "Workout");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Workout_ScheduleId",
                table: "Workout");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Workout");
        }
    }
}
