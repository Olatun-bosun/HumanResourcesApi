
using HumanResourcesApi.Data.Repositories;
using Microsoft.AspNetCore.Mvc.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using HumanResourcesApi.Domain;

namespace HumanResourcesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("AllowOrigin")]
    [Authorize(Roles = SD.Role_Customer)]
    public class EmployeeController : ControllerBase
    {
        protected readonly Repository _repository;

        public EmployeeController(Repository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var employee = await _repository.GetAllAsync();

           

            return Ok(employee);
        }

    }
}
