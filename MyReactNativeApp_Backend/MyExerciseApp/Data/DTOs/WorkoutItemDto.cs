using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyExerciseApp.Data.DTOs;

public class WorkoutItemDto
{
    public string ExerciseName { get; set; }
    public int ExerciseOrder { get; set; }
    public string Sets { get; set; }
    public string Reps { get; set; }
}
// Ta bort?