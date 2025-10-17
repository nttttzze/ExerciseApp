using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ganss.Xss;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyExerciseApp.Entities;
using MyExerciseApp.Models;
using MyExerciseApp.Services.Interfaces;

namespace MyExerciseApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    // private readonly HtmlSanitizer _htmlSanitizer = new();

    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }


    // 500 INTERNAL SERVER ERROR något med firstordefault? Behöve använda Microsoft.EntityFrameworkCore; 
    // Nu är tiden fel, kanske fel inmatning.
    [HttpPost("scheduleWorkout/{workoutName}")]
    public async Task<IActionResult> ScheduleWorkout([FromBody] SchedulePostViewModel model, string workoutName)
    {
        try
        {
            var schedule = await _scheduleService.CreateWorkoutSchedule(model, workoutName);
            return Ok(new { succes = true, schedule });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception in AddWorkout: {ex}");

            return StatusCode(500, new { success = false, message = "An error occurred. Please try again later." });
        }
    }

    [HttpDelete("deleteSchedule/{scheduleId}")]
    public async Task<IActionResult> DeleteSchedule(int scheduleId)
    {
        try
        {
            var deleteSchedule = await _scheduleService.DeleteScheduleAsync(scheduleId);
            if (!deleteSchedule)
            {
                return NotFound(new { success = false, message = "Schedule not found." });
            }
            return Ok(new { success = true, message = "Schedule deleted succesfully." });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception in DeleteWorkout: {ex}");

            return StatusCode(500, new { success = false, message = "An error occurred. Please try again later." });
        }
    }
}
