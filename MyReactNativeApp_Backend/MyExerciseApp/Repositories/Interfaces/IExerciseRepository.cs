using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Data.DTOs;
using MyExerciseApp.Entities;

namespace MyExerciseApp.Repositories;

public interface IExerciseRepository
{
    Task<IEnumerable<ExerciseDto>> ListExercisesAsync();
    Task<Exercise> AddExerciseAsync(Exercise exercise);
}
