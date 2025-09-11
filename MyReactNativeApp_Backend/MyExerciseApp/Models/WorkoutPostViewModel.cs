using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyExerciseApp.Models;

public class WorkoutPostViewModel
{
    [Required]
    [StringLength(40)]
    [RegularExpression(@"^[a-öA-Ö0-9\s]+$", ErrorMessage = "Only letters and numbers allowed.")]
    public string WorkoutName { get; set; }
    public List<WorkoutItemPostViewModel> Exercises { get; set; }
}
