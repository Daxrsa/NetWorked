using JobService.Core.Dtos.JobPosition;
using JobService.Core.Models;

namespace JobService.Services.Interfaces
{
    public interface IJobPosition
    {
        Task<IEnumerable<JobReadDto>> GetAll();
        Task<JobReadDto> GetById(int id);
        bool Add(JobCreateDto dto);
        Task<bool> Delete(int id);
        JobPosition Update(int id, JobPosition job);
    }
}
