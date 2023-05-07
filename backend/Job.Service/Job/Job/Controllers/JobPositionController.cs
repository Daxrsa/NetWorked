using Job.DTOs;
using Job.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Job.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class JobPositionController : ControllerBase
    {
        private readonly IJobPositon _contract;
        public JobPositionController(IJobPositon contract)
        {
            _contract = contract;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPositions()
        {
            return Ok(await _contract.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult> AddJob(JobPositonDTO jobPositon)
        {
            return Ok(await _contract.Add(jobPositon));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(Guid id)
        {
            return Ok(await _contract.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            return Ok(await _contract.Delete(id));
        }

        [HttpPut("{id}/edit")]
        public async Task<IActionResult> UpdateJob(Guid id)
        {
            return Ok(await _contract.Update(id));
        }
    }
}
