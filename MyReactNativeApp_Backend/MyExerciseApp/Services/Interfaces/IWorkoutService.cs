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
    Task<WorkoutDto> GetWorkoutByNameAsync(string workoutName);
    Task<WorkoutDto> AddWorkoutAsync(WorkoutPostViewModel model);
    Task<bool> DeleteWorkoutAsync(int workoutId);
    Task<bool> UpdateWorkoutAsync(int workoutId, UpdateWorkoutDto dto);

}
