using Job.Configuration;
using Job.DTOs;
using Job.Interfaces;
using Job.Models;
using Microsoft.EntityFrameworkCore;

namespace Job.Services
{
    public class CompanyService : ICompany
    {
        private readonly JobDbContext _context;
        public CompanyService(JobDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(CompanyDTO entity)
        {
            Company company = new()
            {
                Location = entity.Location,
                Name = entity.Name
            };
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var company = await _context.Companies.Where(c => c.Id == id).FirstOrDefaultAsync();
            _context.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }

        //Duhet me ndryshu
        public async Task<bool> Update(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);
            _context.Update(company);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Company>> GetAll()
        {
            List<Company> companies = await _context.Companies.ToListAsync();
            return companies;
        }

        public async Task<CompanyDTO> GetById(Guid id)
        {
            Company c = await _context.Companies.Where(c => c.Id == id).FirstOrDefaultAsync();
            CompanyDTO companyDTO = new()
            {
                Id = id,
                Name = c.Name,
                Location = c.Location
            };
            return companyDTO;
        }
    }
}
