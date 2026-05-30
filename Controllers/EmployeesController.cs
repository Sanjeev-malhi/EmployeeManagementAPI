using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        return Ok("This will be used to get the employees");
    }

    [HttpPost]
    public async Task<IActionResult> Create(Employee employee)
    {
        return Ok("This will be used to Add new employees");
    }
}
