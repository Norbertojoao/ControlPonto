using ControlPonto.Domain.Entities;
using ControlPonto.Domain.Interfaces;
using ControlPonto.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControlPonto.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Employee>> GetAllAsync() =>
        await _context.Employees.OrderBy(e => e.Name).ToListAsync();

    public async Task<Employee?> GetByIdAsync(Guid id) =>
        await _context.Employees.FindAsync(id);

    public async Task<Employee?> GetByRegistrationCodeAsync(string registrationCode) =>
        await _context.Employees.FirstOrDefaultAsync(e => e.RegistrationCode == registrationCode);

    public async Task AddAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
    }
}

public class TimeRecordRepository : ITimeRecordRepository
{
    private readonly AppDbContext _context;

    public TimeRecordRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<TimeRecord>> GetByEmployeeAndDateAsync(Guid employeeId, DateOnly date)
    {
        var start = date.ToDateTime(TimeOnly.MinValue);
        var end = date.ToDateTime(TimeOnly.MaxValue);

        return await _context.TimeRecords
            .Where(t => t.EmployeeId == employeeId && t.Timestamp >= start && t.Timestamp <= end)
            .OrderBy(t => t.Timestamp)
            .ToListAsync();
    }

    public async Task AddAsync(TimeRecord record)
    {
        _context.TimeRecords.Add(record);
        await _context.SaveChangesAsync();
    }
}
