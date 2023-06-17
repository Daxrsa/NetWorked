using AutoMapper;
using JobService.Core.Dtos.JobPosition;
using JobService.Core.Models;
using JobService.Data;
using JobService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using JobService.RabbitMqConfig;
using JobService.Core.Dtos;
using Newtonsoft.Json;

namespace JobService.Services
{
    public class JobPositionService : IJobPosition, IGetJobReq
    {
        private readonly JobDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMessageProducer _messageProducer;
        private readonly HttpClient _httpClient;
        public JobPositionService(JobDbContext context, IMapper mapper, IMessageProducer messageProducer, HttpClient httpClient)
        {
            _mapper = mapper;
            _context = context;
            _httpClient = httpClient;
            _messageProducer = messageProducer;
        }

        public async Task<IEnumerable<JobReadDto>> GetAll()
        {
            var jobs = await _context.JobPositions.Include("Company")
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            var convertedJobs = _mapper.Map<IEnumerable<JobReadDto>>(jobs);

            return convertedJobs;
        }

        public async Task<JobReadDto> GetById(int id)
        {
            try
            {
                var job = await _context.JobPositions.Include(job => job.Company).Where(job => job.Id == id).FirstOrDefaultAsync();
                var returnedJob = _mapper.Map<JobReadDto>(job);
                return returnedJob;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Add(JobCreateDto dto, string authorizationHeader)
        {
            try
            {
                var token = authorizationHeader.Split(' ')[1];
                var job = _mapper.Map<JobPosition>(dto);

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var userResponse = await _httpClient.GetAsync("http://localhost:5116/api/Auth/GetloggedInUser");
                if (userResponse != null)
                {
                    string content = await userResponse.Content.ReadAsStringAsync();
                    var loggedInUser = JsonConvert.DeserializeObject<UserDto>(content);
                    job.Username = loggedInUser.username;

                }

                var newNotification = new NotificationsDTO
                {
                    Description = job.Description,
                    Username = job.Username
                };
                _messageProducer.SendMessage<NotificationsDTO>(newNotification, "notifications_service");
                _context.JobPositions.Add(job);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var job = await _context.JobPositions.FindAsync(id);
                if (job == null)
                {
                    throw new Exception("The given job does not exist!");
                }
                _context.Remove(job);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

            public JobPosition Update(int id, JobPosition job)
        {
            throw new NotImplementedException();
        }

        public string GetJobReqById(int jobId)
        {
            try
            {
                var job = _context.JobPositions.Find(jobId);
                return job.Requirements;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> GetJobPositionCount()
        {
            int count = await _context.JobPositions.CountAsync();
            return count;
        }
    }
}
