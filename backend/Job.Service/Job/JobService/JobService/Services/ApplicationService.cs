using AutoMapper;
using File.Package.FileService;
using JobService.Client;
using JobService.Core.Dtos.Application;
using JobService.Core.Models;
using JobService.Data;
using JobService.RabbitMqConfig;
using JobService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobService.Services
{
    public class ApplicationService:IApplication
    {
        private readonly JobDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IGetJobReq _getJobReq;
        private readonly IMessageProducer _messageProducer;
        private readonly IEmail _email;
        private readonly IClient _client;
        public ApplicationService(JobDbContext context, IMapper mapper, IFileService fileService, IGetJobReq getJobReq, IMessageProducer messageProducer, IClient client, IEmail email) 
        {
            _context= context;
            _mapper= mapper;
            _fileService= fileService;
            _getJobReq= getJobReq;
            _messageProducer= messageProducer;
            _email= email;
            _client= client;
        }

        public async Task<IEnumerable<ApplicationReadDto>> GetAll()
        {
            var applications = await _context.Applications.Include("JobPosition").ToListAsync();
            var convertedapplications = _mapper.Map<IEnumerable<ApplicationReadDto>>(applications);

            return convertedapplications;
        }

        public async Task<IEnumerable<ApplicationReadDto>> GetApplicationsByApplicantId(string authorizationHeader)
        {
            try
            {
                var user = _client.GetUserAsync(authorizationHeader);
                if (user == null)
                {
                    throw new Exception("Not authorized!");
                }
                var applications = await _context.Applications.Where(a => a.ApplicantId.Equals(user.Result.Id)).Include("JobPosition").ToListAsync();
                var convertedA = _mapper.Map<IEnumerable<ApplicationReadDto>>(applications);
                return convertedA;

            }
            catch(Exception)
            {
                return null;
            }           
        }

        public async Task<IEnumerable<ApplicationReadDto>> GetApplicationsByJobId(int id)
        {
            var applications = await _context.Applications.Where(a => a.JobId == id).ToListAsync();
            var convertedA = _mapper.Map<IEnumerable<ApplicationReadDto>>(applications);

            return convertedA;
        }

        public async Task<ApplicationReadDto> GetById(int id)
        {
            try
            {
                var a = await _context.Applications.Include(ap => ap.JobPosition).Where(x => x.Id==id).FirstOrDefaultAsync();
                var returnedRes = _mapper.Map<ApplicationReadDto>(a);
                return returnedRes;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Add(ApplicationCreateDto dto, IFormFile file, string authorizationHeader)
        {            
            try
            {
                var a = _mapper.Map<Application>(dto);

                var user = _client.GetUserAsync(authorizationHeader);
                if( user == null ) { return false; }

                string userSkills = user.Result.skills;
                string email = user.Result.email;
                a.ApplicantId = user.Result.Id;
                a.ApplicantName = user.Result.username;

                if (await ifExists(a.JobId, a.ApplicantId))
                {
                    throw new Exception("You have already applied!");
                }

                var jobReq = _getJobReq.GetJobReqById(dto.JobId);                
                a.ResumeUrl = _fileService.SavePdfAsync(file).Result;
                await _context.Applications.AddAsync(a);
                await _context.SaveChangesAsync();

                var profileMatchingResult = new DTO
                {
                    ApplicantSkills = userSkills,
                    JobRequirements = jobReq,
                    ApplicationId = a.Id
                };

                _email.SendEmail(email, a.ApplicantName, a.JobPosition.Title);
                _messageProducer.SendMessage<DTO>(profileMatchingResult, "profile_match_service");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var application = await _context.Applications.FindAsync(id);
                if (application == null)
                {
                    throw new Exception("The given application does not exist!");
                }
                _context.Remove(application);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task<bool> ifExists(int jobId, Guid applicantId)
        {
            return await _context.Applications.Where(x => x.ApplicantId.Equals(applicantId) && x.JobId == jobId).AnyAsync();
        }

        public async Task<int> GetApplicationCount()
        {
            int appCount = await _context.Applications.CountAsync();
            return appCount;
        }
    }
}
