using JobService.Clients;
using JobService.Core.Dtos;
using JobService.Core.Dtos.JobPosition;
using JobService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

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
        //[Authorize]
        public async Task<ActionResult> AddJob(JobCreateDto dto)
        {
            var result = await _contract.Add(dto);

            // Check if the job was added successfully
            if (result)
            {
                // Create a new notification object
                var newNotification = new
                {
                    description = dto.Description
                };

                // Serialize the notification object
                var notificationContent = new StringContent(JsonConvert.SerializeObject(newNotification));
                notificationContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                // Extract the token from the Authorization header or provide your own token
                var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

                // Make a POST request to the notification endpoint
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await httpClient.PostAsync("http://localhost:8800/notifications", notificationContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var status = new
                        {
                            StatusCode = 1,
                            StatusMessage = "Added successfully"
                        };
                        return Ok(status);
                    }
                    else
                    {
                        var status = new
                        {
                            StatusCode = 0,
                            StatusMessage = "Error occurred while creating the notification"
                        };
                        return StatusCode((int)response.StatusCode, status);
                    }
                }
            }
            else
            {
                var status = new
                {
                    StatusCode = 0,
                    StatusMessage = "Error occurred while adding the job"
                };
                return BadRequest(status);
            }
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
