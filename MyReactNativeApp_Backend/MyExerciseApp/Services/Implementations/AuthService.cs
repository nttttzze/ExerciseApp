using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyExerciseApp.Entities;
using MyExerciseApp.Models;
using MyExerciseApp.Repositories.Implementations;
using MyExerciseApp.Repositories.Interfaces;
using MyExerciseApp.Services.Interfaces;

namespace MyExerciseApp.Services.Implementations;

public class AuthService : IAuthService
{

    private readonly UserManager<User> _userManager;


    public AuthService(UserManager<User> userManager)
    {

        _userManager = userManager;
    }
    public async Task<User> RegisterUserAsync(RegisterPostViewModel model)
    {
        var user = new User
        {
            UserName = model.Username,
            Email = model.Username
        };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return user;
        }
        return null;
    }
}
