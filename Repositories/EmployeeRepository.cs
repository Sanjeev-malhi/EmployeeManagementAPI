using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(x => x.Department)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
