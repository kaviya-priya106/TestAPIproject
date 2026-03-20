using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TestAPIproject.Models;

namespace TestAPIproject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Users> User { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }


    }
}
