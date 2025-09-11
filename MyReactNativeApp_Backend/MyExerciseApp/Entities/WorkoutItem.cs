using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyExerciseApp.Entities;

public class WorkoutItem
{
    public int WorkoutId { get; set; }

    public Workout Workout { get; set; }

    public int ExerciseId { get; set; }
    public Exercise Exercise { get; set; }

    public int ExerciseOrder { get; set; }
    public string Sets { get; set; }
    public string Reps { get; set; }


}
