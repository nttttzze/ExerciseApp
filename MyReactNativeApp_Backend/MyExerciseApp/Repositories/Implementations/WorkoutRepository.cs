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
using System.IO.Compression;
// using System.Data.Entity;

namespace MyExerciseApp.Repositories.Implementations;


public class WorkoutRepository : IWorkoutRepository
{
    private readonly DataContext _context;
    public WorkoutRepository(DataContext context)
    {
        _context = context;
    }

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
                .ThenInclude(wi => wi.Exercise)
                .Where(x => x.WorkoutName == workoutName).FirstOrDefaultAsync();
    }

    public async Task<Workout> AddWorkoutAsync(Workout workout)
    {
        await _context.Workout.AddAsync(workout);
        await _context.SaveChangesAsync();
        return await _context.Workout
            .Include(w => w.WorkoutItems)
            .ThenInclude(e => e.Exercise) // La till denna fär att få ut ExerciseName i return
            .FirstOrDefaultAsync(w => w.WorkoutId == workout.WorkoutId);
    }

    public async Task<bool> DeleteWorkoutAsync(int workoutId)
    {
        var deleteWorkout = await _context.Workout.FindAsync(workoutId);
        if (deleteWorkout == null)
        {
            return false;
        }
        _context.Workout.Remove(deleteWorkout);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }





    public Task<bool> UpdateWorkoutAsync(int workoutId, UpdateWorkoutDto dto)
    {
        throw new NotImplementedException();
        // Inte Klar
    }
}
