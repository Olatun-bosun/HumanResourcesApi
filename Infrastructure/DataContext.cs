using HumanResourcesApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace HumanResourcesApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions dbContextoptions) : base(dbContextoptions)
        {

        }

        public DbSet<Employee> HREmployees { get; set; }


    }
}
