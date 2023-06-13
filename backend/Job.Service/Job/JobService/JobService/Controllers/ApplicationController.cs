using File.Package.FileService;
using JobService.Core.Dtos;
using JobService.Core.Dtos.Application;
using JobService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace JobService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApplicationController:ControllerBase
    {
        private readonly IApplication _contract;
        private readonly IFileService _fileService;
        public ApplicationController(IApplication contract, IFileService fileService) 
        {
            _contract= contract;
            _fileService= fileService;
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
            var token = Request.Headers["Authorization"].ToString().Split(' ')[1];
            string userSkills = "";
            //get user data
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var userResponse = await httpClient.GetAsync("http://localhost:5116/api/Auth/GetloggedInUser");
                string userString = await userResponse.Content.ReadAsStringAsync();
                var responseJson = JObject.Parse(userString);
                string username = responseJson["username"].ToString();
                Guid userId = (Guid)responseJson["id"];
                //userSkills = responseJson["skills"].ToString();
                Console.WriteLine(userId);
                dto.ApplicantId = userId;
                dto.ApplicantName= username;
            }

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
