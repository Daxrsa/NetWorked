using Job.Configuration;
using Job.DTOs;
using Job.Interfaces;
using Job.Models;
using Microsoft.EntityFrameworkCore;

namespace Job.Services
{
    public class JobPositionService : IJobPositon
    {
        private readonly JobDbContext _context;
        public JobPositionService(JobDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(JobPositonDTO entity)
        {
            JobPositon job = new()
            {
                Id= entity.Id,
                Name= entity.Name,
                SkillSet= entity.SkillSet,
                Description= entity.Description,
                Created = DateTime.UtcNow,
                Updated= DateTime.Now
            };
            await _context.JobPositions.AddAsync(job);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var job = await _context.JobPositions.Where(c => c.Id == id).FirstOrDefaultAsync();
            _context.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }

        //Duhet me ndryshu
        public async Task<bool> Update(Guid id)
        {
            var job = await _context.JobPositions.FindAsync(id);
            _context.Update(job);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<JobPositon>> GetAll()
        {
            List<JobPositon> jobs = await _context.JobPositions.ToListAsync();
            return jobs;
        }

        public async Task<JobPositonDTO> GetById(Guid id)
        {
            JobPositon job = await _context.JobPositions.Where(c => c.Id == id).FirstOrDefaultAsync();
            JobPositonDTO jobPositonDTO = new()
            {
                Id = id,
                Name = job.Name,
                SkillSet = job.SkillSet,
                Description= job.Description,
                Created= job.Created,
                Updated = job.Updated
            };
            return jobPositonDTO;
        }
    }
}
