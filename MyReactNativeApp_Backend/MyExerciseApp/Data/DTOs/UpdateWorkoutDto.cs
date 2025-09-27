using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Entities;

namespace MyExerciseApp.Data.DTOs;

public class UpdateWorkoutDto
{

    public Exercise Exercise { get; set; }
    public int ExerciseOrder { get; set; }

    public string Reps { get; set; }

    public string Sets { get; set; }

}
