using JobService.Core.Dtos;
using JobService.Core.Dtos.JobPosition;
using JobService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public async Task<ActionResult> AddJob(JobCreateDto dto)
        {
            var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

            //get user data
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var userResponse = await httpClient.GetAsync("http://localhost:5116/api/Auth/GetloggedInUser");
                string userString = await userResponse.Content.ReadAsStringAsync();
                var responseJson = JObject.Parse(userString);
                string username = responseJson["username"].ToString();
                dto.Username = username;
                _contract.Add(dto);
            }

            //send notification on job creation
                var newNotification = new
                {
                    description = dto.Description
                };

                var notificationContent = new StringContent(JsonConvert.SerializeObject(newNotification));
                notificationContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.PostAsync("http://localhost:8800/notifications", notificationContent);
            }
            return Ok(dto);
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
