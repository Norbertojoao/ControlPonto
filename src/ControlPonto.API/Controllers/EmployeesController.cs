using ControlPonto.Application.DTOs;
using ControlPonto.Domain.Entities;
using ControlPonto.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControlPonto.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _repository;

    public EmployeesController(IEmployeeRepository repository) => _repository = repository;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EmployeeResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetAll()
    {
        var employees = await _repository.GetAllAsync();
        var response = employees.Select(e => new EmployeeResponse(
            e.Id, e.Name, e.RegistrationCode, e.Department, e.DailyWorkloadMinutes, e.IsActive));
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployeeResponse>> Create([FromBody] CreateEmployeeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.RegistrationCode))
            return BadRequest(new { message = "Nome e matrícula são obrigatórios." });

        var existing = await _repository.GetByRegistrationCodeAsync(request.RegistrationCode.Trim());
        if (existing is not null)
            return BadRequest(new { message = "Já existe um colaborador com esta matrícula." });

        var employee = new Employee
        {
            Name = request.Name.Trim(),
            RegistrationCode = request.RegistrationCode.Trim(),
            Department = string.IsNullOrWhiteSpace(request.Department) ? "Geral" : request.Department.Trim(),
            DailyWorkloadMinutes = request.DailyWorkloadMinutes > 0 ? request.DailyWorkloadMinutes : 480
        };

        await _repository.AddAsync(employee);

        var response = new EmployeeResponse(
            employee.Id, employee.Name, employee.RegistrationCode, employee.Department, employee.DailyWorkloadMinutes, employee.IsActive);

        return CreatedAtAction(nameof(GetAll), new { id = employee.Id }, response);
    }
}
