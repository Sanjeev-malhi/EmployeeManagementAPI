using EmployeeManagementAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EmployeesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _context.Employees.ToListAsync();

        return Ok(employees);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Employee employee)
    {
        _context.Employees.Add(employee);

        await _context.SaveChangesAsync();

        return Ok(employee);
    }
}
