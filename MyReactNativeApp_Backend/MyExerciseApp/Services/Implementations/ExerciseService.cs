using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Data.DTOs;
using MyExerciseApp.Entities;
using MyExerciseApp.Models;
using MyExerciseApp.Repositories;
using MyExerciseApp.Services.Interfaces;

namespace MyExerciseApp.Services.Implementations;

public class ExerciseService : IExerciseService
{

    private readonly IExerciseRepository _repo;
    public ExerciseService(IExerciseRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<ExerciseDto>> GetExercisesAsync()
    {
        var e = await _repo.GetExercisesAsync();

        return e.Select(exercise => new ExerciseDto
        {
            ExerciseId = exercise.ExerciseId,
            ExerciseName = exercise.ExerciseName,
            WorkoutType = exercise.WorkoutType,
            MainTargetMuscle = exercise.MainTargetMuscle,
            ExerciseGroup = exercise.ExerciseGroup,
            Image = exercise.Image
        }).ToList();
    }

    public async Task<Exercise> AddExerciseAsync(ExercisePostViewModel model)
    {

        if (await _repo.ExerciseExistsAsync(model.ExerciseName))
        {
            return null;
        }

        var exercise = new Exercise
        {
            ExerciseName = model.ExerciseName,
            WorkoutType = model.WorkoutType,
            MainTargetMuscle = model.MainTargetMuscle,
            ExerciseGroup = model.ExerciseGroup,
            Image = model.Image,
        };


        return await _repo.AddExerciseAsync(exercise);
    }
}
