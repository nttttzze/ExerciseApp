using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using MyExerciseApp.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class DataContext : IdentityDbContext<User>
{
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Workout> Workout { get; set; }
    public DbSet<Schedule> Schedules { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Sätter en primary key för båda.
        modelBuilder.Entity<WorkoutItem>().HasKey(wi => new { wi.WorkoutId, wi.ExerciseId });

        modelBuilder.Entity<WorkoutItem>()
            .HasOne(wi => wi.Workout)
            .WithMany(w => w.WorkoutItems)
            .HasForeignKey(wi => wi.WorkoutId);

        modelBuilder.Entity<WorkoutItem>()
            .HasOne(wi => wi.Exercise)
            .WithMany(e => e.WorkoutItems)
            .HasForeignKey(wi => wi.ExerciseId);

    }

}