using JobService.Core.Dtos.Application;
using JobService.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Services.Interfaces
{
    public interface IApplication
    {
        Task<IEnumerable<ApplicationReadDto>> GetAll();
        Task<ApplicationReadDto> GetById(string id);
        Task<IEnumerable<ApplicationReadDto>> GetApplicationsByApplicantId(Guid id);
        Task<IEnumerable<ApplicationReadDto>> GetApplicationsByJobId(int id);
        Task<bool> Add([FromForm] ApplicationCreateDto dto, IFormFile file, string authorizationHeader);
        Task<bool> Delete(string id);
        Application Update(string id, Application company);
        Task<int> GetApplicationCount();
    }
}
