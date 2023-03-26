using Common.Library;
using Microsoft.AspNetCore.Mvc;
using Posting.Service.Models;
using static Posting.Service.DTOs;

namespace Posting.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IRepo<Job> _jobRepo;
        public JobController(IRepo<Job> jobRepo)
        {
            _jobRepo = jobRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<JobDTO>> GetAsync()
        {
            var jobs = (await _jobRepo.GetAllAsync()).Select(item => item.AsDTO());
            return jobs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobDTO>> GetSingleAsync(Guid id)
        {
            var job = await _jobRepo.GetAsync(id);
            if (job == null)
            {
                return NotFound("This job does not exist.");
            }
            return job.AsDTO();
        }

        [HttpPost("add")]
        public async Task<ActionResult<JobDTO>> PostAsync(JobDTO jobDTO)
        {
            var job = new Job
            {
            Title = jobDTO.Title,
            Description = jobDTO.Description,
            Location = jobDTO.Location,
            FullOrPartTime = jobDTO.FullOrPartTime,
            MonthlySalary = jobDTO.MonthlySalary,
            Currency = jobDTO.Currency
            };
            await _jobRepo.CreateAsync(job);
            return CreatedAtAction(nameof(GetSingleAsync), new { id = job.Id }, job);
        }

        [HttpPut("{id}/edit")]
        public async Task<IActionResult> PutAsync(Guid id, JobDTO jobDTO)
        {
            var existingJob = await _jobRepo.GetAsync(id);
            if (existingJob == null)
            {
                return NotFound("Job not found.");
            }
            existingJob.Title = jobDTO.Title;
            existingJob.Description = jobDTO.Description;
            existingJob.Location = jobDTO.Location;
            existingJob.FullOrPartTime = jobDTO.FullOrPartTime;
            existingJob.MonthlySalary = jobDTO.MonthlySalary;
            existingJob.Currency = jobDTO.Currency;
            await _jobRepo.UpdateAsync(existingJob);
            return Ok(existingJob);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var job = await _jobRepo.GetAsync(id);
            if (job == null)
            {
                return NotFound("Job not found.");
            }
            await _jobRepo.RemoveAsync(job.Id);
            return Ok(job);
        }
    }
}