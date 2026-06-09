namespace EmployeeManagementAPI.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();

        Task<Employee> AddAsync(Employee employee);

        Task<Employee?> GetByIdAsync(int id);

        Task UpdateAsync(Employee employee);
    }
}
