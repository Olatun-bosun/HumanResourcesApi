using HumanResourcesApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourcesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =SD.Role_Customer)]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            //for testing purpose

            return new List<Customer> { new Customer { Id = 1, Name = "Test1" }, new Customer { Id = 2, Name = "Test2" } };

            //return await _service.GetAllCustomersAsync();
        }
    }
}
