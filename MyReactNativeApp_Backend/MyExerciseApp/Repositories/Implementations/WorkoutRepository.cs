using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Data.DTOs;
using MyExerciseApp.Entities;
using MyExerciseApp.Models;
using MyExerciseApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyExerciseApp.Interfaces;
// using System.Data.Entity;

namespace MyExerciseApp.Repositories.Implementations;


public class WorkoutRepository : IWorkoutRepository
{
    private readonly DataContext _context;
    public WorkoutRepository(DataContext context)
    {
        _context = context;
    }

    // Vet inte om detta är rätt alls.
    public async Task<IEnumerable<Workout>> GetWorkoutsAsync()
    {
        return await _context.Workout
             .Include(x => x.WorkoutItems)
                 .ThenInclude(w => w.Exercise)
                    .ToListAsync();
    }

    public async Task<Workout> GetWorkoutByNameAsync(string workoutName)
    {
        return await _context.Workout
            .Include(x => x.WorkoutItems)
                .ThenInclude(w => w.Exercise)
                .Where(x => x.WorkoutName == workoutName).FirstOrDefaultAsync();
    }

    public Task AddWorkoutAsync(WorkoutPostViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteWorkoutAsync(string workoutName)
    {
        throw new NotImplementedException();
    }





    public Task<bool> UpdateWorkoutAsync(int workoutId, UpdateWorkoutDto dto)
    {
        throw new NotImplementedException();
    }
}
