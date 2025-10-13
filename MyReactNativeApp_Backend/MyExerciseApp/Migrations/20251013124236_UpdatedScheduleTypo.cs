using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyExerciseApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedScheduleTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShceduledWorkout",
                table: "Schedules",
                newName: "ScheduledWorkout");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ScheduledWorkout",
                table: "Schedules",
                newName: "ShceduledWorkout");
        }
    }
}
