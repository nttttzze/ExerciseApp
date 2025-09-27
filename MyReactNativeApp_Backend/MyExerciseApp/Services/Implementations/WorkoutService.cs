using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Data.DTOs;
using MyExerciseApp.Entities;
using MyExerciseApp.Models;
using MyExerciseApp.Repositories.Interfaces;
using MyExerciseApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyExerciseApp.Interfaces;



namespace MyExerciseApp.Services.Implementations;

public class WorkoutService : IWorkoutService
{

    private readonly IWorkoutRepository _repo;
    public WorkoutService(IWorkoutRepository repo)
    {
        _repo = repo;
    }
    public async Task<IEnumerable<WorkoutDto>> GetWorkoutsAsync()
    {
        var w = await _repo.GetWorkoutsAsync();

        return w.Select(workout => new WorkoutDto
        {
            WorkoutId = workout.WorkoutId,
            WorkoutName = workout.WorkoutName,
            WorkoutItems = workout.WorkoutItems.Select(wi => new WorkoutItemDto
            {
                ExerciseName = wi.Exercise.ExerciseName,
                ExerciseOrder = wi.ExerciseOrder,
                Sets = wi.Sets,
                Reps = wi.Reps
            }).ToList()
        }).ToList();
    }

    public async Task<bool> CreateWorkoutAsync(WorkoutPostViewModel model)
    {
        await _repo.AddWorkoutAsync(model);
        return true;
    }

    public async Task<bool> DeleteWorkoutAsync(string workoutName)
    {
        return await _repo.DeleteWorkoutAsync(workoutName);
    }

    public async Task<Workout> GetWorkoutByNameAsync(string workoutName)
    {
        return await _repo.FindWorkoutByNameAsync(workoutName);
    }

    public async Task<bool> UpdateWorkoutAsync(int workoutId, UpdateWorkoutDto dto)
    {
        return await _repo.UpdateWorkoutAsync(workoutId, dto);
    }

}
