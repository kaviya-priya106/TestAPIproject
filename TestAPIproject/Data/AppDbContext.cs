using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TestAPIproject.Models;

namespace TestAPIproject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
