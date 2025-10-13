using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Entities;

namespace MyExerciseApp.Data.DTOs;

public class ScheduleDto
{
    public DateTime ScheduledWorkout { get; set; }
    public List<Workout> Workouts { get; set; }

}
