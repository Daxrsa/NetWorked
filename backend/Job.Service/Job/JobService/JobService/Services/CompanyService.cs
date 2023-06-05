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
        public CompanyService(JobDbContext context, IMapper mapper) 
        { 
            _context= context;
            _mapper= mapper;
        }
        public bool Add(CompanyCreateDto entity, IFormFile file)
        {
            try
            {
                var company = _mapper.Map<Company>(entity);
                var type1 = "image/jpeg";
                var type2 = "image/png";
                var type3 = "application/png";

                /*if (file.ContentType != type1 && file.ContentType != type2 && file.ContentType != type3)
                {
                    return false;
                }*/
                if (company.Name == "") 
                {
                    throw new Exception("Company could not be added, some properties where null!");
                }

                byte[] pic = null;

                using(var stream = file.OpenReadStream())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        pic = memoryStream.ToArray();
                    }
                }
                company.Logo = pic;
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
            /*foreach (Company comp in companies)
            {
                byte[] bytes = (byte[])comp.Logo;
                comp.Logo = bytes;

            }*/
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
