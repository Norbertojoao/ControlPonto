using ControlPonto.Domain.Entities;

namespace ControlPonto.Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(Guid id);
    Task<Employee?> GetByRegistrationCodeAsync(string registrationCode);
    Task AddAsync(Employee employee);
}
