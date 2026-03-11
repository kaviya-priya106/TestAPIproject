using Microsoft.EntityFrameworkCore;
using System;
using TestAPIproject.Data;
using TestAPIproject.Models;

namespace TestAPIproject.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task AddAsync(Employee emp)
        {
            await _context.Employees.AddAsync(emp);
            await _context.SaveChangesAsync();
        }



        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                await _context.SaveChangesAsync();
            }
        }
    }
}
