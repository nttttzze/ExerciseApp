using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyExerciseApp.Models;

public class LoginViewModel
{
    [StringLength(50, ErrorMessage = "Max 50 char")]
    [Required(ErrorMessage = "Username is required")]
    [EmailAddress]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
