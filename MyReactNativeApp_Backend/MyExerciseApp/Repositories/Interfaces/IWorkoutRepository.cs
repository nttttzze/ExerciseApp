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
    Task<Workout> GetWorkoutByNameAsync(string workoutName);
    Task<Workout> AddWorkoutAsync(Workout workout);
    Task<bool> DeleteWorkoutAsync(int workoutId);
    Task<bool> UpdateWorkoutAsync(int workoutId, UpdateWorkoutDto dto);

}
