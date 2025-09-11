

using System.ComponentModel.DataAnnotations;

namespace MyExerciseApp.Models;

public class ExercisePostViewModel
{
    [Required]
    [StringLength(40)]
    [RegularExpression(@"^[a-öA-Ö\s]+$", ErrorMessage = "Only letters allowed.")]
    public string ExerciseName { get; set; } = "";

    [Required]
    [StringLength(40)]
    [RegularExpression(@"^[a-öA-Ö\s]+$", ErrorMessage = "Only letters allowed.")]
    public string WorkoutType { get; set; } = "";

    [Required]
    [StringLength(40)]
    [RegularExpression(@"^[a-öA-Ö\s]+$", ErrorMessage = "Only letters allowed.")]
    public string MainTargetMuscle { get; set; } = "";

    [Required]
    [StringLength(40)]
    [RegularExpression(@"^[a-öA-Ö\s]+$", ErrorMessage = "Only letters allowed.")]
    public string ExerciseGroup { get; set; } = "";


    public string Image { get; set; } = "";

}