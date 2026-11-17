using ControlPonto.Domain.Enums;

namespace ControlPonto.Application.DTOs;

public record RegisterTimeRecordRequest(
    Guid EmployeeId,
    RecordType Type,
    DateTime? Timestamp = null,
    string? Notes = null
);

public record TimeRecordItemResponse(
    Guid Id,
    DateTime Timestamp,
    RecordType Type,
    string TypeDescription,
    string? Notes
);

public record DailySummaryResponse(
    Guid EmployeeId,
    string EmployeeName,
    DateOnly Date,
    int WorkedMinutes,
    string WorkedFormatted,
    int BalanceMinutes,
    string BalanceFormatted,
    IEnumerable<TimeRecordItemResponse> Records
);
