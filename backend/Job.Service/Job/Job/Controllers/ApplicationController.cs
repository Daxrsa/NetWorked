using Job.DTOs;
using Job.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Job.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplication _contract;
        public ApplicationController(IApplication contract)
        {
            _contract = contract;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApplications()
        {
            return Ok(await _contract.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult> AddApplication(ApplicationDTO application)
        {
            return Ok(await _contract.Add(application));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationById(int id)
        {
            return Ok(await _contract.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            return Ok(await _contract.Delete(id));
        }

        [HttpPut("{id}/edit")]
        public async Task<IActionResult> UpdateApplication(int id)
        {
            return Ok(await _contract.Update(id));
        }
    }
}
