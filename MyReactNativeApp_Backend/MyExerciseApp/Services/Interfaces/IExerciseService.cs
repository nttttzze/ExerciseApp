using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Data.DTOs;
using MyExerciseApp.Entities;
using MyExerciseApp.Models;

namespace MyExerciseApp.Services.Interfaces;

public interface IExerciseService
{
    Task<IEnumerable<ExerciseDto>> GetExercisesAsync();
    Task<Exercise> AddExerciseAsync(ExercisePostViewModel model);
}
