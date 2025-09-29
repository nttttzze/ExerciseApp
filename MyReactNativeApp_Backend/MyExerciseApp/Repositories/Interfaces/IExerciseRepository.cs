using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Data.DTOs;
using MyExerciseApp.Entities;

namespace MyExerciseApp.Repositories;

public interface IExerciseRepository
{
    Task<IEnumerable<Exercise>> GetExercisesAsync();
    Task<Exercise> AddExerciseAsync(Exercise exercise);
    Task<bool> ExerciseExistsAsync(string ExerciseName);
}
