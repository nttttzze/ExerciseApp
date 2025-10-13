using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Data.DTOs;
using MyExerciseApp.Entities;
using Microsoft.EntityFrameworkCore;
using MyExerciseApp.Models;
using MyExerciseApp.Repositories.Interfaces;
using MyExerciseApp.Services.Interfaces;

namespace MyExerciseApp.Services.Implementations;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _repo;
    public ScheduleService(IScheduleRepository repo)
    {
        _repo = repo;
    }

    public async Task<ScheduleDto> CreateWorkoutSchedule(SchedulePostViewModel model, string workoutName)
    {

        var workout = await _repo.GetWorkoutByNameAsync(workoutName);

        if (workout == null)
        {
            throw new KeyNotFoundException("Workout not found.");
        }


        var schedule = new Schedule
        {
            ScheduledWorkout = model.ScheduledWorkout, // Medvetet stavfel pga stavfel i Entity.Schedule
            Workouts = new List<Workout> { workout }
        };

        var createdSchedule = await _repo.CreateWorkoutSchedule(schedule);

        // return createdSchedule; 
        return new ScheduleDto
        {
            ScheduledWorkout = createdSchedule.ScheduledWorkout,
            Workouts = createdSchedule.Workouts.ToList()
        };
    }

    public async Task<bool> DeleteScheduleAsync(int scheduleId)
    {
        return await _repo.DeleteScheduleAync(scheduleId);
    }
}
