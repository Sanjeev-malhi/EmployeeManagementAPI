using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Repositories.Interfaces;
using EmployeeManagementAPI.Services.Interfaces;

namespace EmployeeManagementAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IBlobStorageService _blobService;

        public EmployeeService(IEmployeeRepository repository, IBlobStorageService blobService)
        {
            _repository = repository;
            _blobService = blobService;
        }

        public async Task<List<EmployeeResponseDto>>
            GetEmployeesAsync()
        {
            var employees = await _repository.GetAllAsync();

            return employees.Select(x =>
                new EmployeeResponseDto
                {
                    Id = x.Id,
                    FullName =
                        $"{x.FirstName} {x.LastName}",
                    Email = x.Email,
                    Salary = x.Salary,
                    DepartmentName =
                        x.Department.Name
                }).ToList();
        }

        public async Task<EmployeeResponseDto>
            CreateEmployeeAsync(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Salary = dto.Salary,
                DepartmentId = dto.DepartmentId
            };

            await _repository.AddAsync(employee);

            return new EmployeeResponseDto
            {
                Id = employee.Id,
                FullName =
                    $"{employee.FirstName} {employee.LastName}",
                Email = employee.Email,
                Salary = employee.Salary,
                DepartmentName = ""
            };
        }

        public async Task<string> UploadPhotoAsync(int employeeId, IFormFile file)
        {
            var employee = await _repository.GetByIdAsync(employeeId);
            if (employee == null)
            {
                throw new Exception("Employee Not Found");
            }

            var photoUrl = await _blobService.UploadFileAsync(file);
            employee.PhotoUrl = photoUrl;
            await _repository.UpdateAsync(employee);
            return photoUrl;
        }
    }
}
