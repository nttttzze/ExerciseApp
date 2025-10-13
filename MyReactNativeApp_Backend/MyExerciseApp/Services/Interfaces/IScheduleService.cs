using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyExerciseApp.Data.DTOs;
using MyExerciseApp.Entities;
using Microsoft.EntityFrameworkCore;
using MyExerciseApp.Models;

namespace MyExerciseApp.Services.Interfaces;

public interface IScheduleService
{
    Task<ScheduleDto> CreateWorkoutSchedule(SchedulePostViewModel model, string workoutName);
    Task<bool> DeleteScheduleAsync(int scheduleId);

}
