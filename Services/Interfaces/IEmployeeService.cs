using EmployeeManagementAPI.DTOs;

namespace EmployeeManagementAPI.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponseDto>> GetEmployeesAsync();

        Task<EmployeeResponseDto> CreateEmployeeAsync(
            CreateEmployeeDto dto);

        Task<string> UploadPhotoAsync(int employeeId, IFormFile file);
    }
}
