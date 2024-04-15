using Microsoft.EntityFrameworkCore;

namespace ReactCrudAPI.Models
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) :base(options)
        {
                
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<UserDetail> userModels { get; set; }
    }
}
