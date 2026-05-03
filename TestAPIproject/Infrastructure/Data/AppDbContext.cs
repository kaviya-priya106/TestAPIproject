using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EmployeeOrderManagementAPI.Domain;

namespace EmployeeOrderManagementAPI.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Users> User { get; set; }

        public DbSet<Order> Orders { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }


    }
}
