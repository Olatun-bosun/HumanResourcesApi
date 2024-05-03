using HumanResourcesApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HumanResourcesApi.Data.Repositories
{
    public partial class Repository
    {

        public  Task<List<Employee>> GetAllAsync()
        {
            return  _db.HREmployees.ToListAsync();
        }
    }
}
