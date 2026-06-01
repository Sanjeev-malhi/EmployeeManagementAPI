using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.Models;

public class Employee
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Range(1000, 1000000)]
    public decimal Salary { get; set; }
}
