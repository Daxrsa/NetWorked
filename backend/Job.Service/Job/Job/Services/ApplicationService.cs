using Job.Configuration;
using Job.DTOs;
using Job.Interfaces;
using Job.Models;
using Microsoft.EntityFrameworkCore;

namespace Job.Services
{
    public class ApplicationService: IApplication
    {
        private readonly JobDbContext _context;
        public ApplicationService(JobDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(ApplicationDTO entity)
        {
            Application application = new()
            {
                DateApplied= DateTime.Now
            };
            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var application = await _context.Applications.Where(c => c.Id == id).FirstOrDefaultAsync();
            _context.Remove(application);
            await _context.SaveChangesAsync();
            return true;
        }

        //Duhet me ndryshu
        public async Task<bool> Update(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            _context.Update(application);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Application>> GetAll()
        {
            List<Application> applications = await _context.Applications.ToListAsync();
            return applications;
        }

        public async Task<ApplicationDTO> GetById(int id)
        {
            ApplicationDTO c = await _context.Applications.Where(c => c.Id == id).FirstOrDefaultAsync();
            ApplicationDTO applicationDTO = new()
            {
                Id = id,
                DateApplied = DateTime.Now
            };
            return applicationDTO;
        }
    }
}
