using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyExerciseApp.Entities;
using MyExerciseApp.Models;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.CodeAnalysis;


namespace MyExerciseApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutController : ControllerBase
{
    private readonly DataContext _context;
    private readonly HtmlSanitizer _htmlSanitizer = new();

    public WorkoutController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("listWorkouts")]
    public async Task<ActionResult> ListWorkouts()
    {
        try
        {
            var w = await _context.Workout
            .Include(x => x.WorkoutItems)
                .ThenInclude(w => w.Exercise)
            .Select(workout => new
            {
                workout.WorkoutName,
                WorkoutItem = workout.WorkoutItems.Select(wi => new
                {
                    wi.Exercise.ExerciseName,
                    wi.ExerciseOrder,
                    wi.Sets,
                    wi.Reps

                }).ToList()
            }).ToListAsync();

            // throw new Exception("Test exception");

            return Ok(new { success = true, workout = w });

        }
        catch (Exception ex)
        {
            // Log the exception details for internal tracking
            Console.Error.WriteLine($"Exception in ListWorkouts: {ex}");

            return StatusCode(500, new { success = false, message = "An error occurred. Please try again later." });
        }
    }

    [HttpGet("findWorkout/{workoutName}")]
    public async Task<ActionResult> FindWorkout(string workoutName)
    {
        try
        {
            var w = await _context.Workout
            .Include(x => x.WorkoutItems)
                .ThenInclude(w => w.Exercise)
                .Where(x => x.WorkoutName == workoutName)
            .Select(workout => new
            {
                workout.WorkoutName,
                WorkoutItem = workout.WorkoutItems.Select(wi => new
                {
                    wi.Exercise.ExerciseName,
                    wi.ExerciseOrder,
                    wi.Sets,
                    wi.Reps
                }).ToList()
            })
            .FirstOrDefaultAsync();
            // .SingleOrDefaultAsync();

            if (w is null)
            {
                return NotFound(new { success = false, message = "Workout not found." });
            }


            return Ok(new { success = true, workout = w });

        }
        catch (Exception ex)
        {
            // Log the exception details for internal tracking
            Console.Error.WriteLine($"Exception in FindWorkout: {ex}");

            return StatusCode(500, new { success = false, message = "An error occurred. Please try again later." });
        }
    }

    [HttpPost("addWorkout")]
    public async Task<ActionResult> AddWorkout([FromBody] WorkoutPostViewModel model)
    {
        try
        {
            if (!ModelState.IsValid) return ValidationProblem();

            model.WorkoutName = _htmlSanitizer.Sanitize(model.WorkoutName);
            // model.Exercises = _htmlSanitizer.Sanitize(model.Exercises);
            ModelState.Clear();
            TryValidateModel(model);

            var workout = new Workout
            {
                WorkoutName = model.WorkoutName,
                WorkoutItems = [.. model.Exercises.Select(e => new WorkoutItem
                {
                    ExerciseId = e.ExerciseId,
                    ExerciseOrder = e.ExerciseOrder,
                    Sets = e.Sets,
                    Reps = e.Reps

                })] // .ToList()
            };
            _context.Workout.Add(workout);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, data = workout });

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception in AddWorkout: {ex}");

            return StatusCode(500, new { success = false, message = "An error occurred. Please try again later." });
        }
    }


    [HttpDelete("deleteWorkout/{workoutName}")]
    public async Task<IActionResult> DeleteWorkout(string workoutName)
    {
        try
        {
            var deleteWorkout = await _context.Workout.FirstOrDefaultAsync(w => w.WorkoutName == workoutName);

            if (deleteWorkout != null)
            {
                _context.Workout.Remove(deleteWorkout);
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound(new { success = false, message = "Workout not found. Nothing was deleted." });
                // throw new Exception("fel");
            }

            return Ok(new { success = true, deletedWorkout = deleteWorkout.WorkoutName });

        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception in DeleteWorkout: {ex}");

            return StatusCode(500, new { success = false, message = "An error occurred. Please try again later." });
        }
    }
}
