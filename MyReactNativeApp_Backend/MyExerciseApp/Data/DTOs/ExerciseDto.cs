using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyExerciseApp.Data.DTOs;

public class ExerciseDto
{
    public int ExerciseId { get; set; }
    public string ExerciseName { get; set; }
    public string WorkoutType { get; set; }
    public string MainTargetMuscle { get; set; }
    public string ExerciseGroup { get; set; }
    public string Image { get; set; }
}
