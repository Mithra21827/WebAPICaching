using Microsoft.EntityFrameworkCore;
using WebAPiCaching.Models;
using WebAPiCaching.Services;

namespace WebAPiCaching.Infrastructure
{
    public class EmployeeService : IEmployeeService<Employee, int>
    {
        readonly EmployeeDbContext _context;

        public EmployeeService(EmployeeDbContext context) { 
            _context = context; 
        }

        public async Task<bool> AddEmployee(Employee entity)
        {
            var model = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == entity.EmployeeId);

            if (model == null)
            {
                _context.Employees.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var model = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (model != null) {
                _context.Employees.Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return  await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var model = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (model != null) {
                return model;
            }
            return model; 
        }

        public async Task<Employee> UpdateEmployee(Employee entity)
        {
            var model = await _context.Employees.FirstOrDefaultAsync(e=> e.EmployeeId ==  entity.EmployeeId);
            if (model != null)
            {
                _context.Employees.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return model;
        }
    }
}
