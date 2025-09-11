using System;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

// SQLiteConnection.CreateFile("NativeApp.sqlite");



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});



// Add services to the container.
builder.Services.AddControllers()
    // Löser problemet med "A possible object cycle was detected."
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

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
