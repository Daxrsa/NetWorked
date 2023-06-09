using AutoMapper;
using Azure.Core;
using JobService.Core.Dtos.JobPosition;
using JobService.Core.Models;
using JobService.Data;
using JobService.Services.Interfaces;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace JobService.Services
{
    public class JobPositionService : IJobPosition
    {
        private readonly JobDbContext _context;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public JobPositionService(JobDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<JobReadDto>> GetAll()
        {
            var jobs = await _context.JobPositions.Include("Company").ToListAsync();
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

        public async Task<bool> Add(JobCreateDto dto)
        {
            try
            {
                //string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
                
                //Claims.FirstOrDefault(c => c.Type == "username");

               // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //HttpResponseMessage usernameResponse = await _httpClient.GetAsync("http://localhost:5116/api/Auth/GetloggedInUser");
                //usernameResponse.EnsureSuccessStatusCode();

                //string username = await usernameResponse.Content.ReadAsStringAsync();
                var job = _mapper.Map<JobPosition>(dto);
               // job.Username = username;
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
    }
}
