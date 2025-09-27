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

namespace MyExerciseApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(UserManager<User> userManager) : ControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly HtmlSanitizer _htmlSanitizer = new();

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterPostViewModel model)
    {
        try
        {
            if (!ModelState.IsValid) return ValidationProblem();

            model.Username = _htmlSanitizer.Sanitize(model.Username);
            model.Password = _htmlSanitizer.Sanitize(model.Password);

            var user = new User
            {
                UserName = model.Username,
                Email = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return StatusCode(201, new { success = true, message = "User registered succesfully" });
            }
            return BadRequest(new { success = false, message = result.Errors });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }
}
