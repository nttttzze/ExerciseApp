using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Entities;
using MyExerciseApp.Models;

namespace MyExerciseApp.Repositories.Interfaces;

public interface IScheduleRepository
{
    Task<Workout> GetWorkoutByNameAsync(string workoutName);
    Task<Schedule> CreateWorkoutSchedule(Schedule schedule);
    Task<bool> DeleteScheduleAync(int scheduleId);
}
