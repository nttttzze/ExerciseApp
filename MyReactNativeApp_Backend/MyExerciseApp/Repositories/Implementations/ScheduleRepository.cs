using System;
using System.Collections.Generic;
// using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using MyExerciseApp.Entities;
using MyExerciseApp.Repositories.Interfaces;
using SQLitePCL;

namespace MyExerciseApp.Repositories.Implementations;

public class ScheduleRepository : IScheduleRepository
{
    private readonly DataContext _context;
    public ScheduleRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Schedule> CreateWorkoutSchedule(Schedule schedule)
    {
        // var workout = await _context.Workout.FirstOrDefaultAsync(w => w.WorkoutName == workoutName);

        await _context.Schedules.AddAsync(schedule);
        await _context.SaveChangesAsync();
        return schedule;
    }

    public async Task<Workout> GetWorkoutByNameAsync(string workoutName)
    {
        return await _context.Workout.FirstOrDefaultAsync(w => w.WorkoutName == workoutName);
    }
    public async Task<bool> DeleteScheduleAync(int scheduleId)
    {
        var deleteSchedule = await _context.Schedules.FindAsync(scheduleId);
        if (deleteSchedule == null)
        {
            return false;
        }
        _context.Schedules.Remove(deleteSchedule);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}
