using JobService.Core.Dtos.Application;
using JobService.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApplicationController:ControllerBase
    {
        private readonly IApplication _contract;
        public ApplicationController(IApplication contract) 
        {
            _contract= contract;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _contract.GetAll());
        }

        [HttpGet("applicantId/{id}")]
        public async Task<ActionResult> GetByApplicantId(Guid id)
        {
            return Ok(await _contract.GetApplicationsByApplicantId(id));
        }

        [HttpGet("jobId/{id}")]
        public async Task<ActionResult> GetByJobId(int id)
        {
            return Ok(await _contract.GetApplicationsByJobId(id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            return Ok(await _contract.GetById(id));
        }

        [HttpPost]
        public ActionResult Apply(ApplicationCreateDto dto)
        {
            _contract.Add(dto);
            return Ok();
        }
    }
}
