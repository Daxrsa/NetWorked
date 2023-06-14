using JobService.Core.Dtos;
using JobService.Core.Dtos.JobPosition;
using JobService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JobService.RabbitMqConfig;

namespace JobService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JobPositionController: ControllerBase
    {
        private readonly IJobPosition _contract;
        private readonly IMessageProducer _messageProducer;
        public JobPositionController(IJobPosition contract, IMessageProducer messageProducer)
        {
            _contract = contract;
            _messageProducer = messageProducer;
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

            // Get user data
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

            // Send notification on job creation
            var newNotification = new NotificationsDTO
            {
                Description = dto.Description,
                Username = dto.Username
            };

           

            _messageProducer.SendMessage<NotificationsDTO>(newNotification, "notifications_service");
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
