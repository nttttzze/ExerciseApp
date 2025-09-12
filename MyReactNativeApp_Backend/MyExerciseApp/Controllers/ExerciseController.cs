using System;
using System.Collections.Generic;
// using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyExerciseApp.Models;
using Ganss.Xss;
using MyExerciseApp.Entities;

namespace MyExerciseApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExerciseController : ControllerBase
{
    private readonly DataContext _context;
    private readonly HtmlSanitizer _htmlSanitizer = new();
    public ExerciseController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("listExercises")]
    public async Task<ActionResult> ListExercises()
    {
        try
        {
            var e = await _context.Exercises
            .Select(e => new
            {
                e.ExerciseId,
                e.ExerciseName,
                e.WorkoutType,
                e.MainTargetMuscle,
                e.ExerciseGroup,
                e.Image
            }).ToListAsync();
            return Ok(new { success = true, exercises = e });
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

            model.ExerciseName = _htmlSanitizer.Sanitize(model.ExerciseName);
            model.WorkoutType = _htmlSanitizer.Sanitize(model.WorkoutType);
            model.MainTargetMuscle = _htmlSanitizer.Sanitize(model.MainTargetMuscle);
            model.ExerciseGroup = _htmlSanitizer.Sanitize(model.ExerciseGroup);
            model.Image = _htmlSanitizer.Sanitize(model.Image);
            ModelState.Clear();
            TryValidateModel(model);

            var exercise = new Exercise
            {
                ExerciseName = model.ExerciseName,
                WorkoutType = model.WorkoutType,
                MainTargetMuscle = model.MainTargetMuscle,
                ExerciseGroup = model.ExerciseGroup,
                Image = model.Image,
            };

            bool alreadyExists = await _context.Exercises.AnyAsync(x => x.ExerciseName == model.ExerciseName);

            if (alreadyExists)
            {
                return BadRequest(new { success = false, message = "Exercise already exists." });
            }



            await _context.Exercises.AddAsync(exercise);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, data = exercise, message = "Exercise successfully added." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }


}
