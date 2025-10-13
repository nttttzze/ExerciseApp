using System;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Identity;
using MyExerciseApp.Entities;
using MyExerciseApp.Repositories.Interfaces;
using MyExerciseApp.Services.Interfaces;
using MyExerciseApp.Services.Implementations;
using MyExerciseApp.Repositories.Implementations;
using MyExerciseApp.Interfaces;
using MyExerciseApp.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
    options.AddPolicy("UserAndAdmin", policy => policy.RequireRole("User", "Admin"));
});

// Workout
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();

// Exercise
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();

// Schedule
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();

builder.Services.AddCors();

// builder.Services.AddIdentityCore<User>(options =>
// {
//     options.User.RequireUniqueEmail = true;
//     options.Password.RequiredLength = 8;
// });

builder.Services.AddIdentityApiEndpoints<User>(options =>
{
    options.User.RequireUniqueEmail = true;

})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<DataContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


// builder.Services.AddIdentityApiEndpoints<User>(options =>
// {
//     options.User.RequireUniqueEmail = true;

// })
// .AddRoles<IdentityRole>()
// .AddEntityFrameworkStores<DataContext>();


// Add services to the container.
builder.Services.AddControllers()
    // Löser problemet med "A possible object cycle was detected."
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("tokenSettings:tokenKey").Value!))
        };
    });



var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.LoadExercises(context);
}
catch (Exception e)
{
    Console.WriteLine("{0}", e.Message);
    throw;
}


app.UseHsts();


// app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

