using Microsoft.EntityFrameworkCore;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using MyExerciseApp.Entities;
using MyExerciseApp.Models;
using MyExerciseApp.Services.Interfaces;

namespace MyExerciseApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutController : ControllerBase
{

    private readonly HtmlSanitizer _htmlSanitizer = new();
    private readonly IWorkoutService _workoutService;

    public WorkoutController(IWorkoutService workoutService)
    {
        _workoutService = workoutService;
    }

    [HttpGet("listWorkouts")]
    public async Task<ActionResult> ListWorkouts()
    {
        try
        {
            var workout = await _workoutService.GetWorkoutsAsync();
            return Ok(new { success = true, workout });
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
            var workout = await _workoutService.GetWorkoutByNameAsync(workoutName);

            if (workout is null)
            {
                return NotFound(new { success = false, message = "Workout not found." });
            }

            return Ok(new { success = true, workout });
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
            ModelState.Clear();
            TryValidateModel(model);

            var workout = await _workoutService.AddWorkoutAsync(model);

            return Ok(new { success = true, workout });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception in AddWorkout: {ex}");

            return StatusCode(500, new { success = false, message = "An error occurred. Please try again later." });
        }
    }

    [HttpDelete("deleteWorkout/{workoutId}")]
    public async Task<IActionResult> DeleteWorkout(int workoutId)
    {
        try
        {
            var deleteWorkout = await _workoutService.DeleteWorkoutAsync(workoutId);

            if (!deleteWorkout)
            {
                return NotFound(new { success = false, message = "Workout not found." });
            }

            return Ok(new { success = true, message = "Workout deleted succesfully." });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception in DeleteWorkout: {ex}");

            return StatusCode(500, new { success = false, message = "An error occurred. Please try again later." });
        }
    }


    /// <summary> -----------------------------------------------------------------------------
    ///  Lös PATCH senare.
    ///  Inte Klar
    /// </summary>

    /// 
    // [HttpPatch("updateWorkout/{workoutId}")]
    // public async Task<IActionResult> UpdateWorkout(int workoutId, [FromBody] UpdateWorkoutDto dto)
    // {
    //     var update = await _context.Workout.FirstOrDefaultAsync(wi => wi.WorkoutId == workoutId);

    //     if (update != null)
    //     {
    //         update.WorkoutName = dto.WorkoutName,
    //         update.ExerciseOrder = dto.ExerciseOrder,
    //         update.Reps = dto.Reps,
    //         update.Sets = dto.Sets
    //     }
    //     else
    //     {
    //         return NotFound(new { success = false, message = $"Produkten med ID {id} finns ej. " });
    //     }
    // }

}
