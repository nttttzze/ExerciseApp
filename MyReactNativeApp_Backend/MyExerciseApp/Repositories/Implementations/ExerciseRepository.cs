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

    public Task<Exercise> AddExerciseAsync(Exercise exercise)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ExerciseDto>> ListExercisesAsync()
    {
        throw new NotImplementedException();
    }
}
