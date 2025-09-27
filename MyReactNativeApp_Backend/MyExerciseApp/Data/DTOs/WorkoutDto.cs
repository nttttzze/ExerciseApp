using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyExerciseApp.Data.DTOs;

public class WorkoutDto
{
    public int WorkoutId { get; set; }
    public string WorkoutName { get; set; } = string.Empty;
    public List<WorkoutItemDto> WorkoutItems { get; set; } = new();
}
// Ta bort?