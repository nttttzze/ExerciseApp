using Microsoft.AspNetCore.Mvc;
using MyExerciseApp.Models;
using Ganss.Xss;
using MyExerciseApp.Services.Interfaces;

namespace MyExerciseApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExerciseController : ControllerBase
{
    private readonly HtmlSanitizer _htmlSanitizer = new();
    private readonly IExerciseService _exerciseService;
    public ExerciseController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [HttpGet("listExercises")]
    public async Task<ActionResult> ListExercises()
    {
        try
        {
            var exercise = await _exerciseService.GetExercisesAsync();
            return Ok(new { success = true, exercise });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = "An error occured.", error = ex.Message });
        }
    }

    [HttpPost("addExercise")]
    public async Task<IActionResult> AddExercise(ExercisePostViewModel model)
    {
        try
        {
            if (!ModelState.IsValid) return ValidationProblem();

            // ----- Sanering borde ligga i service. -----

            model.ExerciseName = _htmlSanitizer.Sanitize(model.ExerciseName);
            model.WorkoutType = _htmlSanitizer.Sanitize(model.WorkoutType);
            model.MainTargetMuscle = _htmlSanitizer.Sanitize(model.MainTargetMuscle);
            model.ExerciseGroup = _htmlSanitizer.Sanitize(model.ExerciseGroup);
            model.Image = _htmlSanitizer.Sanitize(model.Image);
            ModelState.Clear();
            TryValidateModel(model);

            var exercise = await _exerciseService.AddExerciseAsync(model);

            if (exercise == null)
            {
                return Conflict(new { success = false, message = "Exercise already exists." });
            }

            return Ok(new { success = true, data = exercise, message = "Exercise successfully added." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }
}
