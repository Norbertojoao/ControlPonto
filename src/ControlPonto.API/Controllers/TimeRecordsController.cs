using ControlPonto.Application.DTOs;
using ControlPonto.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControlPonto.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeRecordsController : ControllerBase
{
    private readonly TimeTrackingService _service;

    public TimeRecordsController(TimeTrackingService service) => _service = service;

    [HttpPost]
    [ProducesResponseType(typeof(TimeRecordItemResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TimeRecordItemResponse>> Register([FromBody] RegisterTimeRecordRequest request)
    {
        var result = await _service.RegisterAsync(request);
        if (!result.Success)
            return BadRequest(new { message = result.Error });

        return Created(string.Empty, result.Data);
    }

    [HttpGet("daily/{employeeId:guid}")]
    [ProducesResponseType(typeof(DailySummaryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DailySummaryResponse>> GetDailySummary(Guid employeeId, [FromQuery] DateOnly? date)
    {
        var targetDate = date ?? DateOnly.FromDateTime(DateTime.Now);
        var summary = await _service.GetDailySummaryAsync(employeeId, targetDate);

        if (summary is null)
            return NotFound(new { message = "Colaborador não encontrado." });

        return Ok(summary);
    }
}
