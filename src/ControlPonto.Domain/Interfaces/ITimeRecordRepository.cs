using ControlPonto.Domain.Entities;

namespace ControlPonto.Domain.Interfaces;

public interface ITimeRecordRepository
{
    Task<IEnumerable<TimeRecord>> GetByEmployeeAndDateAsync(Guid employeeId, DateOnly date);
    Task AddAsync(TimeRecord record);
}
