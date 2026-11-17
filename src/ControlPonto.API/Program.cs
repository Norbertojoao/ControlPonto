using ControlPonto.Application.Services;
using ControlPonto.Domain.Entities;
using ControlPonto.Domain.Interfaces;
using ControlPonto.Infrastructure.Data;
using ControlPonto.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=controlponto.db"));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ITimeRecordRepository, TimeRecordRepository>();
builder.Services.AddScoped<TimeTrackingService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Employees.Any())
    {
        db.Employees.Add(new Employee
        {
            Name = "Norberto João Leite",
            RegistrationCode = "DEV-2026",
            Department = "Engenharia & DevOps",
            DailyWorkloadMinutes = 480,
            IsActive = true
        });
        db.SaveChanges();
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();
