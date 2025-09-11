using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace MyExerciseApp.Entities;

public class Exercise
{
    public int ExerciseId { get; set; }
    [Required]
    public string ExerciseName { get; set; }

    public string WorkoutType { get; set; }
    // Sterngth/cardio/.../...
    [Required]
    public string MainTargetMuscle { get; set; }
    // Chest/Quads...../full-body...

    public string ExerciseGroup { get; set; }
    // Compound/Iso
    public string Image { get; set; }

    public ICollection<WorkoutItem> WorkoutItems { get; set; }

}