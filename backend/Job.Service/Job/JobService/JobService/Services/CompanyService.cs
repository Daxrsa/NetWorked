using AutoMapper;
using JobService.Core.Dtos.Company;
using JobService.Core.Models;
using JobService.Data;
using JobService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobService.Services
{
    public class CompanyService : ICompany
    {
        private readonly JobDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public CompanyService(JobDbContext context, IMapper mapper, IFileService fileService) 
        { 
            _context= context;
            _mapper= mapper;
            _fileService= fileService;
        }
        public bool Add(CompanyCreateDto entity)
        {
            try
            {
                var company = _mapper.Map<Company>(entity);
                if (entity.file != null)
                {
                    var fileResult = _fileService.SaveImage(entity.file);
                    if (fileResult.Item1 == 1)
                    {
                        company.Logo = fileResult.Item2;
                    }
                }
                _context.Companies.Add(company);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex) { 
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var company = await _context.Companies.FindAsync(id);
                if (company == null)
                {
                    throw new Exception("The given company does not exist!");
                }
                _context.Remove(company);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex) 
            {
                return false;
            }
        }

        public async Task<IEnumerable<CompanyReadDto>> GetAll()
        {
            var companies = await _context.Companies.ToListAsync();
            var convertedComps = _mapper.Map<IEnumerable<CompanyReadDto>>(companies);

            return convertedComps;
        }

        public async Task<CompanyReadDto> GetById(Guid id)
        {
            try
            {
                var company = await _context.Companies.FindAsync(id);
                var returnedCompany = _mapper.Map<CompanyReadDto>(company);
                return returnedCompany;
            }
            catch(Exception ex) 
            {
                return null;
            }
        }

        public Company Update(Guid id, Company company)
        {
            throw new NotImplementedException();
        }
    }
}
