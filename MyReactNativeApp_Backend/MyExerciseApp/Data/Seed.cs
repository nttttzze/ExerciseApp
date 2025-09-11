using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MyExerciseApp.Entities;

public static class Seed
{
    public static async Task LoadExercises(DataContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (context.Exercises.Any()) return;

        // Seedar med färdiga övningar
        var json = File.ReadAllText("Data/json/exercises.json");
        var exercises = JsonSerializer.Deserialize<List<Exercise>>(json, options);

        if (exercises is not null && exercises.Count > 0)
        {
            await context.Exercises.AddRangeAsync(exercises);
            await context.SaveChangesAsync();
        }
    }
}