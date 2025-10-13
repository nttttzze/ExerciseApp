using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyExerciseApp.Entities;

public class Schedule
{
    public int ScheduleId { get; set; }
    public DateTime ScheduledWorkout { get; set; }

    public int WorkoutId { get; set; }

    public ICollection<Workout> Workouts { get; set; }
}
