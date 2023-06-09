using JobService.Core.Dtos;
using JobService.Core.Dtos.JobPosition;
using JobService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JobPositionController: ControllerBase
    {
        private readonly IJobPosition _contract;
        private readonly HttpClient _httpClient;
        public JobPositionController(IJobPosition contract, IHttpClientFactory httpClientFactory)
        {
            _contract = contract;
            _httpClient = httpClientFactory.CreateClient();
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
        //[Authorize]
        public ActionResult AddJob(JobCreateDto dto)
        {
            var result = _contract.Add(dto).Result;
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
