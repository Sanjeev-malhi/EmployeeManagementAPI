using EmployeeManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();
    var pending = dbContext.Database
        .GetPendingMigrations();
    if (pending.Any())
    {
        dbContext.Database.Migrate();
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () =>
{
    return "Welcome to Employee Management API";
});

app.MapGet("/environment", (IWebHostEnvironment env) =>
{
    return env.EnvironmentName;
});

app.MapControllers();

app.Run();
