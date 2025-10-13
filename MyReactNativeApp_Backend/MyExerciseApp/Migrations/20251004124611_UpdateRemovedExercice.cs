using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyExerciseApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRemovedExercice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExerciseName",
                table: "WorkoutItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExerciseName",
                table: "WorkoutItem",
                type: "TEXT",
                nullable: true);
        }
    }
}
