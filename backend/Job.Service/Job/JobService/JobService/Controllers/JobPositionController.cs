using JobService.Core.Dtos;
using JobService.Core.Dtos.JobPosition;
using JobService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JobPositionController: ControllerBase
    {
        private readonly IJobPosition _contract;
        public JobPositionController(IJobPosition contract)
        {
            _contract = contract;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllJobs()
        {
            return Ok(await _contract.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _contract.GetById(id));
        }

        [HttpPost]
        public ActionResult AddJob(JobCreateDto dto)
        {
            var result = _contract.Add(dto);
            var status = new Status()
            {
                StatusCode = result ? 1 : 0,
                StatusMessage = result ? "Added successfully": "Error has occured"
            };
            return Ok(status);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJob(int id)
        {
            var result = await _contract.Delete(id);
            var status = new Status()
            {
                StatusCode = result ? 1 : 0,
                StatusMessage = result ? "Deleted successfully" : "Error has occured"
            };
            return Ok(status);
        }
    }
}
