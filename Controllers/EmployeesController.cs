using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(
        IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees =
            await _service.GetEmployeesAsync();

        return Ok(employees);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateEmployeeDto dto)
    {
        var employee =
            await _service.CreateEmployeeAsync(dto);

        return CreatedAtAction(
            nameof(GetEmployees),
            new { id = employee.Id },
            employee);
    }

    [HttpPost("{id}/upload-photo")]
    public async Task<IActionResult> UploadPhoto(
    int id,
    IFormFile file)
    {
        var photoUrl =
            await _service.UploadPhotoAsync(
                id,
                file);

        return Ok(new
        {
            PhotoUrl = photoUrl
        });
    }
}