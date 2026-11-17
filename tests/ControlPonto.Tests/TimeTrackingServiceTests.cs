using ControlPonto.Application.DTOs;
using ControlPonto.Application.Services;
using ControlPonto.Domain.Entities;
using ControlPonto.Domain.Enums;
using ControlPonto.Domain.Interfaces;
using Xunit;

namespace ControlPonto.Tests;

public class TimeTrackingServiceTests
{
    private class FakeEmployeeRepo : IEmployeeRepository
    {
        public List<Employee> Items { get; } = new();
        public Task AddAsync(Employee employee) { Items.Add(employee); return Task.CompletedTask; }
        public Task<IEnumerable<Employee>> GetAllAsync() => Task.FromResult<IEnumerable<Employee>>(Items);
        public Task<Employee?> GetByIdAsync(Guid id) => Task.FromResult(Items.FirstOrDefault(e => e.Id == id));
        public Task<Employee?> GetByRegistrationCodeAsync(string code) => Task.FromResult(Items.FirstOrDefault(e => e.RegistrationCode == code));
    }

    private class FakeTimeRecordRepo : ITimeRecordRepository
    {
        public List<TimeRecord> Items { get; } = new();
        public Task AddAsync(TimeRecord record) { Items.Add(record); return Task.CompletedTask; }
        public Task<IEnumerable<TimeRecord>> GetByEmployeeAndDateAsync(Guid employeeId, DateOnly date) =>
            Task.FromResult<IEnumerable<TimeRecord>>(Items.Where(r => r.EmployeeId == employeeId && DateOnly.FromDateTime(r.Timestamp) == date));
    }

    [Fact]
    public async Task RegisterAsync_ShouldRejectDuplicateConsecutiveType_RN01()
    {
        var empRepo = new FakeEmployeeRepo();
        var recRepo = new FakeTimeRecordRepo();
        var emp = new Employee { Name = "Teste", RegistrationCode = "001", IsActive = true };
        await empRepo.AddAsync(emp);

        var service = new TimeTrackingService(empRepo, recRepo);
        var baseDate = new DateTime(2026, 11, 17, 8, 0, 0);

        var first = await service.RegisterAsync(new RegisterTimeRecordRequest(emp.Id, RecordType.Entrada, baseDate));
        var second = await service.RegisterAsync(new RegisterTimeRecordRequest(emp.Id, RecordType.Entrada, baseDate.AddMinutes(10)));

        Assert.True(first.Success);
        Assert.False(second.Success);
        Assert.Contains("duas vezes consecutivas", second.Error);
    }

    [Fact]
    public async Task RegisterAsync_ShouldRejectInactiveEmployee_RN03()
    {
        var empRepo = new FakeEmployeeRepo();
        var recRepo = new FakeTimeRecordRepo();
        var emp = new Employee { Name = "Inativo", RegistrationCode = "002", IsActive = false };
        await empRepo.AddAsync(emp);

        var service = new TimeTrackingService(empRepo, recRepo);
        var result = await service.RegisterAsync(new RegisterTimeRecordRequest(emp.Id, RecordType.Entrada));

        Assert.False(result.Success);
        Assert.Contains("inativo", result.Error);
    }

    [Fact]
    public async Task GetDailySummaryAsync_ShouldCalculateWorkedHoursAndBalance_RN04_RN05()
    {
        var empRepo = new FakeEmployeeRepo();
        var recRepo = new FakeTimeRecordRepo();
        var emp = new Employee { Name = "Norberto", RegistrationCode = "003", DailyWorkloadMinutes = 480 };
        await empRepo.AddAsync(emp);

        var service = new TimeTrackingService(empRepo, recRepo);
        var day = new DateTime(2026, 11, 17, 8, 0, 0);

        await service.RegisterAsync(new RegisterTimeRecordRequest(emp.Id, RecordType.Entrada, day));
        await service.RegisterAsync(new RegisterTimeRecordRequest(emp.Id, RecordType.SaidaIntervalo, day.AddHours(4)));
        await service.RegisterAsync(new RegisterTimeRecordRequest(emp.Id, RecordType.RetornoIntervalo, day.AddHours(5)));
        await service.RegisterAsync(new RegisterTimeRecordRequest(emp.Id, RecordType.Saida, day.AddHours(9).AddMinutes(30)));

        var summary = await service.GetDailySummaryAsync(emp.Id, DateOnly.FromDateTime(day));

        Assert.NotNull(summary);
        Assert.Equal(510, summary!.WorkedMinutes);
        Assert.Equal("08:30", summary.WorkedFormatted);
        Assert.Equal(30, summary.BalanceMinutes);
        Assert.Equal("+00:30", summary.BalanceFormatted);
    }
}
