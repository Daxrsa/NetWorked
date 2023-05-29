using JobService.Core.Dtos.Application;
using JobService.Core.Dtos.Company;
using JobService.Core.Models;

namespace JobService.Services.Interfaces
{
    public interface IApplication
    {
        Task<IEnumerable<ApplicationReadDto>> GetAll();
        Task<ApplicationReadDto> GetById(string id);
        Task<IEnumerable<ApplicationReadDto>> GetApplicationsByApplicantId(Guid id);
        Task<IEnumerable<ApplicationReadDto>> GetApplicationsByJobId(int id);
        bool Add(ApplicationCreateDto dto);
        Task<bool> Delete(string id);
        Application Update(string id, Application company);
    }
}
