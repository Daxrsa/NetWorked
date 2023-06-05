using AutoMapper;
using JobService.Clients;
using JobService.Core.Dtos.Application;
using JobService.Core.Dtos.JobPosition;
using JobService.Core.Models;
using JobService.Data;
using JobService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobService.Services
{
    public class ApplicationService:IApplication
    {
        private readonly JobDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserClient userClient;
        public ApplicationService(JobDbContext context, IMapper mapper, UserClient userClient) 
        {
            _context= context;
            _mapper= mapper;
            this.userClient= userClient;
        }

        public async Task<IEnumerable<ApplicationReadDto>> GetAll()
        {
            var applications = await _context.Applications.Include("JobPosition").ToListAsync();
            var convertedapplications = _mapper.Map<IEnumerable<ApplicationReadDto>>(applications);

            return convertedapplications;
        }

        public async Task<IEnumerable<ApplicationReadDto>> GetApplicationsByApplicantId(Guid id)
        {
            var applications = await _context.Applications.Where(a => a.ApplicantId.Equals(id)).ToListAsync();
            var convertedA = _mapper.Map<IEnumerable<ApplicationReadDto>>(applications);

            return convertedA;
        }

        public async Task<IEnumerable<ApplicationReadDto>> GetApplicationsByJobId(int id)
        {
            var applications = await _context.Applications.Where(a => a.JobId == id).ToListAsync();
            var convertedA = _mapper.Map<IEnumerable<ApplicationReadDto>>(applications);

            return convertedA;
        }

        public async Task<ApplicationReadDto> GetById(string id)
        {
            try
            {
                var a = await _context.Applications.Include(ap => ap.JobPosition).Where(x => (x.ApplicantId.ToString() +x.JobId.ToString()).Equals(id)).FirstOrDefaultAsync();
                var returnedRes = _mapper.Map<ApplicationReadDto>(a);
                return returnedRes;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Add(ApplicationCreateDto dto, IFormFile file)
        {
            try
            {
                var fiveMegaBytes = 5 * 1024 * 1024;
                var pdfType = "application/pdf";

                if(file.Length > fiveMegaBytes || file.ContentType != pdfType)
                {
                    return false;
                }

                var resumeUrl = Guid.NewGuid().ToString() + ".pdf";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", "pdfs", resumeUrl);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                //var user = await userClient.GetUserAsync();

                var a = _mapper.Map<Application>(dto);
                a.ResumeUrl = resumeUrl;
                //a.ApplicantId = user.Id;
                await _context.Applications.AddAsync(a);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Application Update(string id, Application company)
        {
            throw new NotImplementedException();
        }
    }
}
