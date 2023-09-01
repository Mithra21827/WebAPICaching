using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace WebAPiCaching.Models
{
    public class EmployeeDbContext: DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options): base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
