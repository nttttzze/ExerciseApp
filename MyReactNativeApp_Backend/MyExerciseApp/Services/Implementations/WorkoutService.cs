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

    public async Task<WorkoutDto> GetWorkoutByNameAsync(string workoutName)
    {
        var w = await _repo.GetWorkoutByNameAsync(workoutName);

        // Returnerar 0 på workout id och exercise id.
        // DB visar korrekt workoutId och exerciseId.
        // Fungerar nu, hade inte implementerat Workout/Exercise Id i return.
        return new WorkoutDto
        {
            WorkoutName = w.WorkoutName,
            WorkoutId = w.WorkoutId,
            WorkoutItems = w.WorkoutItems.Select(wi => new WorkoutItemDto
            {
                ExerciseName = wi.Exercise.ExerciseName,
                ExerciseId = wi.ExerciseId,
                ExerciseOrder = wi.ExerciseOrder,
                Sets = wi.Sets,
                Reps = wi.Reps
            }).ToList()
        };
    }


    public async Task<WorkoutDto> AddWorkoutAsync(WorkoutPostViewModel model)
    {
        var workout = new Workout
        {
            WorkoutName = model.WorkoutName,
            WorkoutItems = model.Exercises.Select(e => new WorkoutItem
            {
                ExerciseId = e.ExerciseId,
                ExerciseOrder = e.ExerciseOrder,
                Sets = e.Sets,
                Reps = e.Reps


            }).ToList()
        };

        var savedWorkout = await _repo.AddWorkoutAsync(workout);

        return new WorkoutDto

        {
            WorkoutName = savedWorkout.WorkoutName,
            WorkoutItems = savedWorkout.WorkoutItems.Select(wi => new WorkoutItemDto
            {
                ExerciseName = wi.Exercise.ExerciseName, // Returnerar namnet på vald övning.
                ExerciseId = wi.ExerciseId,
                ExerciseOrder = wi.ExerciseOrder,
                Sets = wi.Sets,
                Reps = wi.Reps


            }).ToList()
        };
    }

    public async Task<bool> DeleteWorkoutAsync(int workoutId)
    {
        return await _repo.DeleteWorkoutAsync(workoutId);
    }


    public async Task<bool> UpdateWorkoutAsync(int workoutId, UpdateWorkoutDto dto)
    {
        return await _repo.UpdateWorkoutAsync(workoutId, dto);
        // Inte Klar
    }

}
