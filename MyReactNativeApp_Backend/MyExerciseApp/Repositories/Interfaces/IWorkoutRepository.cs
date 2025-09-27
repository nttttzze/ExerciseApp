using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Data.DTOs;
using MyExerciseApp.Entities;
using MyExerciseApp.Models;

namespace MyExerciseApp.Interfaces;

public interface IWorkoutRepository
{
    Task<IEnumerable<Workout>> GetWorkoutsAsync();
    Task<Workout> FindWorkoutByNameAsync(string workoutName);
    Task AddWorkoutAsync(WorkoutPostViewModel model);
    Task<bool> UpdateWorkoutAsync(int workoutId, UpdateWorkoutDto dto);
    Task<bool> DeleteWorkoutAsync(string workoutName);
}
