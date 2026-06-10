using EmployeeManagementAPI.Configurations;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Repositories;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;


var builder = WebApplication.CreateBuilder(args);
if (!builder.Environment.IsDevelopment())
{
    builder.Configuration.AddAzureKeyVault(
        new Uri("https://employeekv.vault.azure.net/"), new ManagedIdentityCredential());
}

builder.Services.AddControllers();
Console.WriteLine("Environment: " + builder.Environment.EnvironmentName);

Console.WriteLine(
    "Connection String = " +
    builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.Configure<AzureStorageSettings>(builder.Configuration.GetSection("AzureStorage"));
builder.Services.AddSwaggerGen();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider
//        .GetRequiredService<ApplicationDbContext>();
//    var pending = dbContext.Database
//        .GetPendingMigrations();
//    if (pending.Any())
//    {
//        dbContext.Database.Migrate();
//    }
//}

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
