using AutoMapper;
using File.Package.FileService;
using JobService.Core.Dtos;
using JobService.Core.Dtos.Application;
using JobService.Core.Models;
using JobService.Data;
using JobService.RabbitMqConfig;
using JobService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace JobService.Services
{
    public class ApplicationService:IApplication
    {
        private readonly JobDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IGetJobReq _getJobReq;
        private readonly IMessageProducer _messageProducer;
        private readonly HttpClient _httpClient;
        private readonly IEmail _email;
        public ApplicationService(JobDbContext context, IMapper mapper, IFileService fileService, IGetJobReq getJobReq, IMessageProducer messageProducer, HttpClient httpClient, IEmail email) 
        {
            _context= context;
            _mapper= mapper;
            _fileService= fileService;
            _getJobReq= getJobReq;
            _messageProducer= messageProducer;
            _httpClient= httpClient;
            _email= email;
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

        public async Task<bool> Add(ApplicationCreateDto dto, IFormFile file, string authorizationHeader)
        {            
            try
            {
                var token = authorizationHeader.Split(' ')[1];
                string userSkills = "";
                string email = "";
                var a = _mapper.Map<Application>(dto);

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var userResponse = await _httpClient.GetAsync("http://localhost:5116/api/Auth/GetloggedInUser");
                if( userResponse != null)
                {
                    string content = await userResponse.Content.ReadAsStringAsync();
                    var loggedInUser = JsonConvert.DeserializeObject<UserDto>(content);
                    a.ApplicantId = loggedInUser.Id;
                    a.ApplicantName = loggedInUser.username;
                    userSkills = loggedInUser.skills;
                    email = loggedInUser.email;
                    
                }

                if (await ifExists(a.JobId, a.ApplicantId))
                {
                    throw new Exception("You have already applied!");
                }

                var jobReq = _getJobReq.GetJobReqById(dto.JobId);
                
                await _context.Applications.AddAsync(a);
                a.ResumeUrl = _fileService.SavePdfAsync(file).Result;
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

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Application Update(string id, Application company)
        {
            throw new NotImplementedException();
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
