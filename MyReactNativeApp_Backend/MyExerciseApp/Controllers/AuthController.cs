using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ganss.Xss;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyExerciseApp.Entities;
using MyExerciseApp.Models;
using MyExerciseApp.Services.Interfaces;

namespace MyExerciseApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly HtmlSanitizer _htmlSanitizer = new();
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterPostViewModel model)
    {
        // try
        // {
        //     if (!ModelState.IsValid) return ValidationProblem();

        //     model.Username = _htmlSanitizer.Sanitize(model.Username);
        //     model.Password = _htmlSanitizer.Sanitize(model.Password);

        //     var user = new User
        //     {
        //         UserName = model.Username,
        //         Email = model.Username
        //     };
        //     var result = await _userManager.CreateAsync(user, model.Password);

        //     if (result.Succeeded)
        //     {
        //         return StatusCode(201, new { success = true, message = "User registered succesfully" });
        //     }
        //     return BadRequest(new { success = false, message = result.Errors });
        // }
        // catch (Exception ex)
        // {
        //     return BadRequest(new { success = false, message = ex.Message });
        // }

        try
        {
            if (!ModelState.IsValid) return ValidationProblem();

            model.Username = _htmlSanitizer.Sanitize(model.Username);
            model.Password = _htmlSanitizer.Sanitize(model.Password);

            var user = await _authService.RegisterUserAsync(model);

            if (user == null)
            {
                return Conflict(new { success = false, message = "User already exists." });
            }

            return StatusCode(201, new { success = true, message = "User registered succesfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }
}
