using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Data.DTOs;
using MyExerciseApp.Entities;
using MyExerciseApp.Models;

namespace MyExerciseApp.Services.Interfaces;

public interface IWorkoutService
{
    Task<IEnumerable<WorkoutDto>> GetWorkoutsAsync();
    Task<Workout> GetWorkoutByNameAsync(string workoutName);
    Task<bool> CreateWorkoutAsync(WorkoutPostViewModel model);
    Task<bool> UpdateWorkoutAsync(int workoutId, UpdateWorkoutDto dto);
    Task<bool> DeleteWorkoutAsync(string workoutName);
}
