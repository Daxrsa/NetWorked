using File.Package.FileService;
using JobService.Core.Dtos;
using JobService.Core.Dtos.Application;
using JobService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JobService.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ApplicationController:ControllerBase
    {
        private readonly IApplication _contract;
        private readonly IFileService _fileService;
        public ApplicationController(IApplication contract, IFileService fileService) 
        {
            _contract = contract;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _contract.GetAll());
        }

        [HttpGet("applicant")]
        [Authorize]
        public async Task<ActionResult> GetByApplicantId()
        {
            string authorizationHeader = Request.Headers["Authorization"].ToString();
            return Ok(await _contract.GetApplicationsByApplicantId(authorizationHeader));
        }

        [HttpGet("jobId/{id}")]
        public async Task<ActionResult> GetByJobId(int id)
        {
            return Ok(await _contract.GetApplicationsByJobId(id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _contract.GetById(id));
        }

        [HttpGet]
        [Route("download/{url}")]
        public IActionResult DownloadPdfFile(string url)
        {
            try
            {
                var pdfBytes = _fileService.DownloadPdfFile(url);
                var file = File(pdfBytes, "application/pdf", url);
                return file;
            }
            catch (FileNotFoundException)
            {
                return NotFound("File Not Found");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Apply([FromForm] ApplicationCreateDto dto, IFormFile file)
        {
            string authorizationHeader = Request.Headers["Authorization"].ToString();
            var result = await _contract.Add(dto, file, authorizationHeader);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest("You have already applied for this position!");
        }

        [HttpGet("countApplications")]
        public async Task<ActionResult<int>> GetApplicationCount()
        {
            return Ok(await _contract.GetApplicationCount());
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _contract.Delete(id);
            if(result.Result is false)
            {
                return NotFound("Not found");
            }
            return StatusCode(200,"Deleted successfully");
        }
    }
}
