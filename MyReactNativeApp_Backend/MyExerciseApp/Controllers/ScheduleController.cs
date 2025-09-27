using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyExerciseApp.Entities;
using MyExerciseApp.Models;

namespace MyExerciseApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly DataContext _context;
    private readonly HtmlSanitizer _htmlSanitizer = new();

    public ScheduleController(DataContext context)
    {
        _context = context;
    }

    [HttpPost("scheduleWorkout/{workoutName}")]
    public async Task<IActionResult> ScheduleWorkout([FromBody] SchedulePostViewModel model, string workoutName)
    {
        // var schedule = new Schedule
        // {
        //     ShceduledWorkout = model.ShceduledWorkout,
        //     Workouts = [.. model.Workout.Select(e => new Workout {
        //         WorkoutName = e.WorkoutName
        //     })]
        // };

        var workout = await _context.Workout.FirstOrDefaultAsync(w => w.WorkoutName == workoutName);

        if (workout == null)
        {
            return NotFound(new { success = false, message = "Workout not found." });
        }

        var schedule = new Schedule
        {

            ShceduledWorkout = model.ScheduledWorkout,
            Workouts = [workout]
        };

        _context.Schedules.Add(schedule);
        await _context.SaveChangesAsync();

        return Ok(new { success = true, data = schedule });
    }
}
