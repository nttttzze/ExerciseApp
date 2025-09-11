using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyExerciseApp.Entities;

public class Workout
{
    public int WorkoutId { get; set; }
    public string WorkoutName { get; set; }



    public ICollection<WorkoutItem> WorkoutItems { get; set; }
}
