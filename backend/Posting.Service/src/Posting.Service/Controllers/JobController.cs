using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Posting.Service.DTOs;
using Posting.Service.Repos;

namespace Posting.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobRepo _jobRepo;
        private readonly IMapper _mapper;
        public JobController(IJobRepo jobRepo, IMapper mapper)
        {
            _jobRepo = jobRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<JobDTO>> GetAsync()
        {
            var jobs = await _jobRepo.GetAllAsync();
            var jobDTOs = _mapper.Map<IEnumerable<JobDTO>>(jobs);
            return jobDTOs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobDTO>> GetById(Guid id)
        {
            var job = await _jobRepo.GetAsync(id);
            var jobDTO = _mapper.Map<ActionResult<JobDTO>>(job);
            return jobDTO;
        }
    }
}