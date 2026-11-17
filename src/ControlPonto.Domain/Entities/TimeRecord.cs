using ControlPonto.Domain.Enums;

namespace ControlPonto.Domain.Entities;

public class TimeRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public DateTime Timestamp { get; set; }
    public RecordType Type { get; set; }
    public string? Notes { get; set; }
}
