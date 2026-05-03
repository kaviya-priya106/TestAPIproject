using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using EmployeeOrderManagementAPI.Application.Dto;
using EmployeeOrderManagementAPI.Domain;
using EmployeeOrderManagementAPI.Infrastructure.Data;

namespace EmployeeOrderManagementAPI.Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Employees.AsNoTracking().Skip((pageNumber - 1) * pageSize)
        .Take(pageSize).ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            var param = new SqlParameter("@Id", id);

            var result = await _context.Employees
     .FromSqlRaw("EXEC GetEmployeeById @Id", param)
     .ToListAsync();

            return result.FirstOrDefault();
            //return await _context.Employees.FindAsync(id);
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

        /*public async Task GetOrderByUserId(int id)
        {
            return await _context.Orders.FindAsync(id);
        }*/


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
