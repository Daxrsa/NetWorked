using Application.Interfaces;
using Domain.CreateDTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CompanyService : ICompanyRepo
    {
        private readonly JobDbContext _context;
        public CompanyService(JobDbContext context) {
            _context= context;
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

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetById(Guid id)
        {
            return await _context.Companies.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);
            _context.Update(company);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
