namespace ControlPonto.Domain.Entities;

public class Employee
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string RegistrationCode { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public int DailyWorkloadMinutes { get; set; } = 480;
    public bool IsActive { get; set; } = true;
    public ICollection<TimeRecord> TimeRecords { get; set; } = new List<TimeRecord>();
}
