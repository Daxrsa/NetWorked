using JobService.Core.Dtos.Application;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Services.Interfaces
{
    public interface IApplication
    {
        Task<IEnumerable<ApplicationReadDto>> GetAll();
        Task<ApplicationReadDto> GetById(int id);
        Task<IEnumerable<ApplicationReadDto>> GetApplicationsByApplicantId(string authorizationHeader);
        Task<IEnumerable<ApplicationReadDto>> GetApplicationsByJobId(int id);
        Task<bool> Add([FromForm] ApplicationCreateDto dto, IFormFile file, string authorizationHeader);
        Task<bool> Delete(int id);
        Task<int> GetApplicationCount();
    }
}
