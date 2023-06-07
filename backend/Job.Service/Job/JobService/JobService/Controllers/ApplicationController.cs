using JobService.Core.Dtos;
using JobService.Core.Dtos.Application;
using JobService.Services.Interfaces;
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

        [HttpGet]
        [Route("download/{url}")]
        public IActionResult DownloadPdfFile(string url)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", "pdfs", url);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File Not Found");
            }

            var pdfBytes = System.IO.File.ReadAllBytes(filePath);
            var file = File(pdfBytes, "application/pdf", url);
            return file;
        }

        [HttpPost]
        public async Task<ActionResult> Apply([FromForm] ApplicationCreateDto dto, IFormFile file)
        {
            var result = await _contract.Add(dto, file);
            var status = new Status()
            {
                StatusCode = result ? 1 : 0,
                StatusMessage = result ? "Applied successfully" : "Error has occured"
            };
            return Ok(status);
        }
    }
}
