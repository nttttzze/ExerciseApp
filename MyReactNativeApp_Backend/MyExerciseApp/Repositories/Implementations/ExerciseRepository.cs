using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyExerciseApp.Data.DTOs;
using MyExerciseApp.Entities;

namespace MyExerciseApp.Repositories;


public class ExerciseRepository : IExerciseRepository
{
    private readonly DataContext _context;
    public ExerciseRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Exercise>> GetExercisesAsync()
    {
        return await _context.Exercises.ToListAsync();
    }

    public async Task<Exercise> AddExerciseAsync(Exercise exercise)
    {

        await _context.Exercises.AddAsync(exercise);
        await _context.SaveChangesAsync();
        return exercise;
    }

    public async Task<bool> ExerciseExistsAsync(string ExerciseName)
    {
        return await _context.Exercises.AnyAsync(x => x.ExerciseName == ExerciseName);
    }
}
