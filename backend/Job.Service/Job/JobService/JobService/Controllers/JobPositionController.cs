using JobService.Core.Dtos;
using JobService.Core.Dtos.JobPosition;
using JobService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    //[AllowAnonymous]
    public class JobPositionController: ControllerBase
    {
        private readonly IJobPosition _contract;
        public JobPositionController(IJobPosition contract)
        {
            _contract = contract;
        }

        [HttpGet]
        [Authorize(Roles ="Recruiter")]
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
        public async Task<ActionResult> AddJob(JobCreateDto dto)
        {
            string authorizationHeader = Request.Headers["Authorization"].ToString();
            return Ok(await _contract.Add(dto, authorizationHeader));
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

        [HttpGet("countJobPositions")]
        public async Task<ActionResult<int>> GetJobPositionCount()
        {
            return Ok(await _contract.GetJobPositionCount());
        }
    }
}
