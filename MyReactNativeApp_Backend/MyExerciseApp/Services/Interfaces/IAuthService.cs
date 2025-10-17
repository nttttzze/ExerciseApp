using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Entities;
using MyExerciseApp.Models;

namespace MyExerciseApp.Services.Interfaces;

public interface IAuthService
{
    Task<User> RegisterUserAsync(RegisterPostViewModel model);
}
