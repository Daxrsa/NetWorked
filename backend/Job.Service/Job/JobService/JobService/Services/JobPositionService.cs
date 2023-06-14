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
    public class JobPositionService : IJobPosition, IGetJobReq
    {
        private readonly JobDbContext _context;
        private readonly IMapper _mapper;
        public JobPositionService(JobDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
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
                var job = _mapper.Map<JobPosition>(dto);
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
    }
}
