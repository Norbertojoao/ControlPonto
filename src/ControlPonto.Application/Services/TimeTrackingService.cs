using ControlPonto.Application.DTOs;
using ControlPonto.Domain.Entities;
using ControlPonto.Domain.Enums;
using ControlPonto.Domain.Interfaces;

namespace ControlPonto.Application.Services;

public class TimeTrackingService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ITimeRecordRepository _timeRecordRepository;

    public TimeTrackingService(
        IEmployeeRepository employeeRepository,
        ITimeRecordRepository timeRecordRepository)
    {
        _employeeRepository = employeeRepository;
        _timeRecordRepository = timeRecordRepository;
    }

    public async Task<(bool Success, string? Error, TimeRecordItemResponse? Data)> RegisterAsync(RegisterTimeRecordRequest request)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
        if (employee is null)
            return (false, "Colaborador não encontrado.", null);

        if (!employee.IsActive)
            return (false, "Colaborador inativo não pode registrar ponto.", null);

        var timestamp = request.Timestamp ?? DateTime.Now;
        var date = DateOnly.FromDateTime(timestamp);
        var existingRecords = (await _timeRecordRepository.GetByEmployeeAndDateAsync(employee.Id, date))
            .OrderBy(r => r.Timestamp)
            .ToList();

        if (existingRecords.Count >= 4)
            return (false, "Limite diário de 4 marcações atingido.", null);

        var validationError = ValidateSequence(existingRecords, request.Type);
        if (validationError is not null)
            return (false, validationError, null);

        var record = new TimeRecord
        {
            EmployeeId = employee.Id,
            Timestamp = timestamp,
            Type = request.Type,
            Notes = request.Notes
        };

        await _timeRecordRepository.AddAsync(record);

        return (true, null, new TimeRecordItemResponse(
            record.Id,
            record.Timestamp,
            record.Type,
            GetTypeDescription(record.Type),
            record.Notes
        ));
    }

    public async Task<DailySummaryResponse?> GetDailySummaryAsync(Guid employeeId, DateOnly date)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        if (employee is null)
            return null;

        var records = (await _timeRecordRepository.GetByEmployeeAndDateAsync(employeeId, date))
            .OrderBy(r => r.Timestamp)
            .ToList();

        int workedMinutes = CalculateWorkedMinutes(records);
        int balanceMinutes = workedMinutes - employee.DailyWorkloadMinutes;

        var items = records.Select(r => new TimeRecordItemResponse(
            r.Id,
            r.Timestamp,
            r.Type,
            GetTypeDescription(r.Type),
            r.Notes
        ));

        return new DailySummaryResponse(
            employee.Id,
            employee.Name,
            date,
            workedMinutes,
            FormatMinutes(workedMinutes),
            balanceMinutes,
            FormatBalance(balanceMinutes),
            items
        );
    }

    private static string? ValidateSequence(List<TimeRecord> existing, RecordType nextType)
    {
        if (existing.Count == 0)
        {
            return nextType == RecordType.Entrada
                ? null
                : "A primeira marcação do dia deve ser obrigatoriamente 'Entrada'.";
        }

        var lastType = existing.Last().Type;

        if (lastType == RecordType.Saida)
            return "A jornada do dia já foi encerrada com 'Saída'.";

        if (lastType == nextType)
            return $"Não é permitido registrar '{GetTypeDescription(nextType)}' duas vezes consecutivas.";

        return (lastType, nextType) switch
        {
            (RecordType.Entrada, RecordType.SaidaIntervalo) => null,
            (RecordType.Entrada, RecordType.Saida) => null,
            (RecordType.SaidaIntervalo, RecordType.RetornoIntervalo) => null,
            (RecordType.RetornoIntervalo, RecordType.Saida) => null,
            _ => $"Sequência inválida: após '{GetTypeDescription(lastType)}' não é permitido registrar '{GetTypeDescription(nextType)}'."
        };
    }

    public static int CalculateWorkedMinutes(List<TimeRecord> orderedRecords)
    {
        if (orderedRecords.Count < 2)
            return 0;

        double totalMinutes = 0;

        for (int i = 0; i + 1 < orderedRecords.Count; i += 2)
        {
            var start = orderedRecords[i].Timestamp;
            var end = orderedRecords[i + 1].Timestamp;
            if (end > start)
            {
                totalMinutes += (end - start).TotalMinutes;
            }
        }

        return (int)Math.Round(totalMinutes);
    }

    private static string FormatMinutes(int totalMinutes)
    {
        var ts = TimeSpan.FromMinutes(Math.Abs(totalMinutes));
        return $"{(int)ts.TotalHours:D2}:{ts.Minutes:D2}";
    }

    private static string FormatBalance(int balanceMinutes)
    {
        var prefix = balanceMinutes >= 0 ? "+" : "-";
        return $"{prefix}{FormatMinutes(balanceMinutes)}";
    }

    private static string GetTypeDescription(RecordType type) => type switch
    {
        RecordType.Entrada => "Entrada",
        RecordType.SaidaIntervalo => "Saída para Intervalo",
        RecordType.RetornoIntervalo => "Retorno do Intervalo",
        RecordType.Saida => "Saída",
        _ => type.ToString()
    };
}
