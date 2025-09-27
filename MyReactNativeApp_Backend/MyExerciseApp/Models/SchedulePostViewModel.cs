using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Entities;

namespace MyExerciseApp.Models;

public class SchedulePostViewModel
{
    public DateTime ScheduledWorkout { get; set; }
    public List<Workout> Workout { get; set; }

}
