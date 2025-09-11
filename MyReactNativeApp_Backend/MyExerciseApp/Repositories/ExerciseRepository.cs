using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyExerciseApp.Data.DTOs;
using MyExerciseApp.Entities;

namespace MyExerciseApp.Repositories;


/// <summary>
/// Testing new stuff
/// </summary>
public class ExerciseRepository : IExerciseRepository
{
    private readonly DataContext _context;
    public ExerciseRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExerciseDto>> GetAllExercisesAsync()
    {
        return await _context.Exercises
        .Select(e => new ExerciseDto()
        {
            ExerciseName = e.ExerciseName,
            MainTargetMuscle = e.MainTargetMuscle,
        }).ToListAsync();

    }


    public Exercise GetById(int ExerciseId)
    {
        throw new NotImplementedException();
    }

    public void Insert(Exercise exercise)
    {
        throw new NotImplementedException();
    }


}
