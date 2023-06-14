using AutoMapper;
using File.Package.FileService;
using JobService.Core.Dtos.Application;
using JobService.Core.Models;
using JobService.Data;
using JobService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobService.Services
{
    public class ApplicationService:IApplication
    {
        private readonly JobDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public ApplicationService(JobDbContext context, IMapper mapper, IFileService fileService) 
        {
            _context= context;
            _mapper= mapper;
            _fileService= fileService;
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
                var a = _mapper.Map<Application>(dto);
                await _context.Applications.AddAsync(a);
                a.ResumeUrl = _fileService.SavePdfAsync(file).Result;
                Console.WriteLine(a.ResumeUrl);
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
