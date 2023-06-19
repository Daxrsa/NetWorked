using AutoMapper;
using JobService.Core.Dtos.JobPosition;
using JobService.Core.Models;
using JobService.Data;
using JobService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using JobService.RabbitMqConfig;
using JobService.Core.Dtos;
using JobService.Client;
using JobService.Core.Dtos.Company;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobService.Services
{
    public class JobPositionService : IJobPosition, IGetJobReq
    {
        private readonly JobDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMessageProducer _messageProducer;
        private readonly IClient _client;
        public JobPositionService(JobDbContext context, IMapper mapper, IMessageProducer messageProducer, IClient client)
        {
            _mapper = mapper;
            _context = context;
            _messageProducer = messageProducer;
            _client = client;
        }

        public async Task<IEnumerable<JobReadDto>> GetAll()
        {
            var jobs = await _context.JobPositions
                .Where(job => job.ExpireDate >= DateTime.Now)
                .Include("Company")
                .Include("JobCategory")
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            var convertedJobs = _mapper.Map<IEnumerable<JobReadDto>>(jobs);

            return convertedJobs;
        }

        public async Task<IEnumerable<JobReadDto>> GetAllDashboard()
        {
            var jobs = await _context.JobPositions
                .Include("Company")
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            var convertedJobs = _mapper.Map<IEnumerable<JobReadDto>>(jobs);

            return convertedJobs;
        }

        public async Task<JobReadDto> GetById(int id)
        {
            try
            {
                var job = await _context.JobPositions
                    .Include(job => job.Company)
                    .Where(job => job.Id == id)
                    .FirstOrDefaultAsync();
                var returnedJob = _mapper.Map<JobReadDto>(job);
                return returnedJob;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<JobReadDto>> GetByRecruiterId(string authorizationHeader)
        {
            try
            {
                var user = _client.GetUserAsync(authorizationHeader);
                if (user == null) 
                {
                    throw new Exception("Not found!"); 
                }
                var job = await _context.JobPositions
                    .Where(job => job.Username.Equals(user.Result.username))
                    .Include(job => job.Company)
                    .ToListAsync();
                var returnedJob = _mapper.Map<IEnumerable<JobReadDto>>(job);
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
                var job = _mapper.Map<JobPosition>(dto);
                var user = _client.GetUserAsync(authorizationHeader);
                if (user == null) { return false; }

                job.Username = user.Result.username;

                var newNotification = new NotificationsDTO
                {
                    Description = job.Description,
                    Username = job.Username
                };
                
                _context.JobPositions.Add(job);
                _context.SaveChanges();
                _messageProducer.SendMessage<NotificationsDTO>(newNotification, "notifications_service");
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

        public JobReadDto Update(int id, JobReadDto job)
        {
            try
            {
                if (job == null || id != job.Id)
                {
                    throw new Exception("No job found");
                }

                var existingJob = _context.JobPositions.FirstOrDefault(j => j.Id == id);
                if (existingJob == null)
                {
                    throw new Exception("Not found!");
                }

                existingJob.Title = job.Title;
                existingJob.Description = job.Description;
                existingJob.ExpireDate = job.ExpireDate;
                existingJob.JobLevel = job.JobLevel;
                _context.SaveChanges();

                var jobResult = _mapper.Map<JobReadDto>(existingJob);
                return jobResult;
            }
            catch (Exception)
            {
                return null;
            }
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
