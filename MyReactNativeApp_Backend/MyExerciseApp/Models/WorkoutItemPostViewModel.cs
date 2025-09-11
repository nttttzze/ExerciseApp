using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MyExerciseApp.Models;

public class WorkoutItemPostViewModel
{
    public int ExerciseId { get; set; }
    public int ExerciseOrder { get; set; }
    [RegularExpression(@"^[0-9-]*$", ErrorMessage = "Only numbers and - allowed.")]
    public string Sets { get; set; }
    [RegularExpression(@"^[0-9-]*$", ErrorMessage = "Only numbers and - allowed.")]
    public string Reps { get; set; }
}
