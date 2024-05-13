using HumanResourcesApi.Domain;
using HumanResourcesApi.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HumanResourcesApi.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions dbContextoptions) : base(dbContextoptions)
        {

        }

        public DbSet<Employee> HREmployees { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }



    }
}
