namespace ControlPonto.Application.DTOs;

public record CreateEmployeeRequest(
    string Name,
    string RegistrationCode,
    string Department,
    int DailyWorkloadMinutes = 480
);

public record EmployeeResponse(
    Guid Id,
    string Name,
    string RegistrationCode,
    string Department,
    int DailyWorkloadMinutes,
    bool IsActive
);
